using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public partial class uc_Revoke : UserControl
    {
        private string _host, _port, _serviceName, _username, _password;
        private DataTable _allPrivs;

        public uc_Revoke() { InitializeComponent(); }

        public void SetConfig(string h, string p, string s, string u, string pass)
        {
            _host = h; _port = p; _serviceName = s; _username = u; _password = pass;
        }

        public void RefreshData() { LoadAllData(); }

        private void uc_Revoke_Load(object sender, EventArgs e)
        {
            dgvPrivs.ReadOnly = true;
            dgvPrivs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrivs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrivs.AllowUserToAddRows = false;
            dgvPrivs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            if (cboRevokeType.Items.Count > 0) cboRevokeType.SelectedIndex = 0;
            LoadAllData();
        }

        private void LoadAllData()
        {
            if (string.IsNullOrEmpty(_host)) return;
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string sql = @"
                SELECT p.GRANTEE, p.PRIVILEGE, p.OWNER, p.TABLE_NAME, 
                       'ALL' as COLUMN_NAME, o.CREATED, o.OBJECT_TYPE, '' as REAL_VIEW_NAME
                FROM DBA_TAB_PRIVS p 
                JOIN DBA_OBJECTS o ON p.OWNER = o.OWNER AND p.TABLE_NAME = o.OBJECT_NAME
                WHERE p.OWNER NOT IN ('SYS','SYSTEM','DBSNMP','OUTLN')
                  AND (p.GRANTEE IN (SELECT USERNAME FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N')
                       OR p.GRANTEE IN (SELECT ROLE FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'))
                UNION ALL
                SELECT cp.GRANTEE, cp.PRIVILEGE, cp.OWNER, cp.TABLE_NAME, 
                       cp.COLUMN_NAME, o.CREATED, 'COLUMN_NATIVE', ''
                FROM DBA_COL_PRIVS cp 
                JOIN DBA_OBJECTS o ON cp.OWNER = o.OWNER AND cp.TABLE_NAME = o.OBJECT_NAME
                ORDER BY 6 DESC";

            DataTable dtRaw = db.ExecuteQuery(sql);
            DataTable dtDisplay = new DataTable();
            dtDisplay.Columns.Add("Người nhận (User/Role)");
            dtDisplay.Columns.Add("Quyền");
            dtDisplay.Columns.Add("Đối tượng");
            dtDisplay.Columns.Add("Cột cụ thể");
            dtDisplay.Columns.Add("FULL_OBJ_PATH");
            dtDisplay.Columns.Add("IS_VIEW_SEC");

            foreach (DataRow row in dtRaw.Rows)
            {
                string grantee = row["GRANTEE"].ToString();
                string priv = row["PRIVILEGE"].ToString();
                string owner = row["OWNER"].ToString();
                string tableName = row["TABLE_NAME"].ToString();
                string col = row["COLUMN_NAME"].ToString();
                string objType = row["OBJECT_TYPE"].ToString();

                if (objType == "VIEW" && tableName.StartsWith("V_SEC_"))
                {
                    string[] parts = tableName.Split('_');
                    string originalTable = parts.Length >= 3 ? parts[2] : tableName;

                    DataTable dtCols = db.ExecuteQuery($"SELECT COLUMN_NAME FROM DBA_TAB_COLS WHERE OWNER = '{owner}' AND TABLE_NAME = '{tableName}'");
                    foreach (DataRow rCol in dtCols.Rows)
                    {
                        dtDisplay.Rows.Add(grantee, "SELECT (Column)", owner + "." + originalTable, rCol["COLUMN_NAME"], owner + "." + tableName, "YES");
                    }
                }
                else
                {
                    dtDisplay.Rows.Add(grantee, priv, owner + "." + tableName, col, owner + "." + tableName, "NO");
                }
            }

            _allPrivs = dtDisplay;
            dgvPrivs.DataSource = dtDisplay;

            if (dgvPrivs.Columns.Contains("FULL_OBJ_PATH")) dgvPrivs.Columns["FULL_OBJ_PATH"].Visible = false;
            if (dgvPrivs.Columns.Contains("IS_VIEW_SEC")) dgvPrivs.Columns["IS_VIEW_SEC"].Visible = false;
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            if (dgvPrivs.CurrentRow == null) return;

            var row = dgvPrivs.CurrentRow;
            string grantee = row.Cells["Người nhận (User/Role)"].Value.ToString();
            string priv = row.Cells["Quyền"].Value.ToString();
            string originalObjPath = row.Cells["Đối tượng"].Value.ToString();
            string colName = row.Cells["Cột cụ thể"].Value.ToString();
            string techPath = row.Cells["FULL_OBJ_PATH"].Value.ToString();
            string isViewSec = row.Cells["IS_VIEW_SEC"].Value.ToString();

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            if (MessageBox.Show($"Bạn có chắc muốn thu hồi quyền trên cột [{colName}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                if (isViewSec == "YES")
                {
                    DataTable dtRemain = db.ExecuteQuery($"SELECT COLUMN_NAME FROM DBA_TAB_COLS WHERE OWNER || '.' || TABLE_NAME = '{techPath.ToUpper()}' AND COLUMN_NAME <> '{colName.ToUpper()}'");

                    if (dtRemain.Rows.Count > 0)
                    {
                        string newList = "";
                        foreach (DataRow r in dtRemain.Rows) newList += r["COLUMN_NAME"].ToString() + ",";
                        db.ExecuteNonQuery($"CREATE OR REPLACE VIEW {techPath} AS SELECT {newList.TrimEnd(',')} FROM {originalObjPath}");
                        MessageBox.Show("Đã xóa cột khỏi quyền SELECT.");
                    }
                    else
                    {
                        db.ExecuteNonQuery($"DROP VIEW {techPath}");
                        MessageBox.Show("Đã thu hồi toàn bộ quyền SELECT trên đối tượng.");
                    }
                }
                else
                {
                    string sql = (priv == "UPDATE" && colName != "ALL")
                        ? $"REVOKE UPDATE ON {techPath} FROM {grantee}"
                        : $"REVOKE {priv} ON {techPath} FROM {grantee}";
                    db.ExecuteNonQuery(sql);
                    MessageBox.Show("Thu hồi thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thu hồi: " + ex.Message);
            }

            LoadAllData();
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (_allPrivs == null) return;
            string type = cboRevokeType.Text;
            string target = cboRevokeTarget.Text;
            string search = txtSearch.Text.ToUpper();

            string filter = "";
            if (target != "-- Tất cả --" && !string.IsNullOrEmpty(target))
                filter = $"[Người nhận (User/Role)] = '{target}'";

            if (!string.IsNullOrEmpty(search))
            {
                string searchPart = $"([Đối tượng] LIKE '%{search}%' OR [Quyền] LIKE '%{search}%')";
                filter = string.IsNullOrEmpty(filter) ? searchPart : $"{filter} AND {searchPart}";
            }

            _allPrivs.DefaultView.RowFilter = filter;
        }

        private void cboRevokeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_host)) return;
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string q = (cboRevokeType.Text == "User")
                ? "SELECT USERNAME AS NAME FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N'"
                : "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'";

            DataTable dt = db.ExecuteQuery(q);
            cboRevokeTarget.Items.Clear();
            cboRevokeTarget.Items.Add("-- Tất cả --");
            if (dt != null) foreach (DataRow r in dt.Rows) cboRevokeTarget.Items.Add(r["NAME"].ToString());
            cboRevokeTarget.SelectedIndex = 0;
            Filter_Changed(null, null);
        }
    }
}