using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public partial class uc_Grant : UserControl
    {
        private string _host, _port, _serviceName, _username, _password;

        private Dictionary<string, HashSet<string>> _memory = new Dictionary<string, HashSet<string>>();

        public uc_Grant() { InitializeComponent(); }

        public void SetConfig(string h, string p, string s, string u, string pass)
        {
            _host = h; _port = p; _serviceName = s; _username = u; _password = pass;
        }

        private void uc_Grant_Load(object sender, EventArgs e)
        {
            cboGranteeType.SelectedIndex = 0;
            cboObjectType.SelectedIndex = 0;
        }

        private void clbPrivs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string priv = clbPrivs.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(priv)) return;

            clbColumns.ItemCheck -= clbColumns_ItemCheck;

            if (priv == "SELECT" || priv == "UPDATE")
            {
                clbColumns.Enabled = true;
                lblColumns.Text = priv == "SELECT" ? " Chọn cột cho SELECT " : " Chọn cột cho UPDATE";
                lblColumns.ForeColor = Color.Blue;

                if (!_memory.ContainsKey(priv)) _memory[priv] = new HashSet<string>();
                var savedCols = _memory[priv];

                for (int i = 0; i < clbColumns.Items.Count; i++)
                {
                    string colName = clbColumns.Items[i].ToString();
                    clbColumns.SetItemChecked(i, savedCols.Contains(colName));
                }
            }
            else
            {
                clbColumns.Enabled = false;
                lblColumns.Text = $"3c. {priv} cấp trên TOÀN BẢNG (Không dùng cột)";
                lblColumns.ForeColor = Color.Red;

                for (int i = 0; i < clbColumns.Items.Count; i++)
                {
                    clbColumns.SetItemChecked(i, false);
                }
            }

            clbColumns.ItemCheck += clbColumns_ItemCheck;
        }

        private void clbColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string priv = clbPrivs.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(priv) || (priv != "SELECT" && priv != "UPDATE")) return;

            if (!_memory.ContainsKey(priv)) _memory[priv] = new HashSet<string>();

            string colName = clbColumns.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
                _memory[priv].Add(colName);
            else
                _memory[priv].Remove(colName);
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.Text;
            string fullObj = cboObjectName.Text;
            string objNameOnly = fullObj.Contains(".") ? fullObj.Split('.')[1] : fullObj;

            string grantOpt = chkWithGrantOption.Checked
                ? (cboObjectType.Text == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION")
                : "";

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            if (clbPrivs.CheckedItems.Count == 0) { MessageBox.Show("Chọn ít nhất 1 quyền!"); return; }

            try
            {
                foreach (string priv in clbPrivs.CheckedItems)
                {
                    bool hasColumns = _memory.ContainsKey(priv) && _memory[priv].Count > 0;
                    string sql = "";

                    if (priv == "SELECT" && hasColumns)
                    {
                        string columnList = string.Join(", ", _memory[priv]);
                        string viewName = $"V_SEC_{objNameOnly}_{grantee.Replace("#", "")}";

                        string createViewSql = $"CREATE OR REPLACE VIEW {viewName} AS SELECT {columnList} FROM {fullObj}";
                        db.ExecuteNonQuery(createViewSql);

                        sql = $"GRANT SELECT ON {viewName} TO {grantee}{grantOpt}";
                    }
                    else if (priv == "UPDATE" && hasColumns)
                    {
                        string columnList = string.Join(", ", _memory[priv]);
                        sql = $"GRANT UPDATE({columnList}) ON {fullObj} TO {grantee}{grantOpt}";
                    }
                    else
                    {
                        sql = $"GRANT {priv} ON {fullObj} TO {grantee}{grantOpt}";
                    }

                    db.ExecuteNonQuery(sql);
                }
                MessageBox.Show("Đã thực hiện cấp quyền (bao gồm tạo View bảo mật nếu có SELECT cột) thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình cấp quyền: " + ex.Message);
            }
        }

        private void cboGranteeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_host)) return;
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string q = (cboGranteeType.Text == "User") ? "SELECT USERNAME AS NAME FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N'" : "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'";
            DataTable dt = db.ExecuteQuery(q);
            cboGrantee.Items.Clear();
            if (dt != null) foreach (DataRow r in dt.Rows) cboGrantee.Items.Add(r["NAME"].ToString());
        }

        private void cboObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cboObjectType.Text;
            clbPrivs.Items.Clear();
            _memory.Clear();
            if (type == "TABLE" || type == "VIEW") clbPrivs.Items.AddRange(new string[] { "SELECT", "INSERT", "UPDATE", "DELETE" });
            else if (type == "PROCEDURE" || type == "FUNCTION") clbPrivs.Items.AddRange(new string[] { "EXECUTE" });

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string q = (type == "ROLE") ? "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'" : $"SELECT OWNER || '.' || OBJECT_NAME AS NAME FROM DBA_OBJECTS WHERE OBJECT_TYPE = '{type}' AND ORACLE_MAINTAINED = 'N'";
            DataTable dt = db.ExecuteQuery(q);
            cboObjectName.Items.Clear();
            if (dt != null) foreach (DataRow r in dt.Rows) cboObjectName.Items.Add(r["NAME"].ToString());
        }

        private void cboObjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            clbColumns.Items.Clear();
            _memory.Clear();
            string fullObj = cboObjectName.Text;
            if (!fullObj.Contains(".")) return;
            string[] p = fullObj.Split('.');
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            DataTable dt = db.ExecuteQuery($"SELECT COLUMN_NAME FROM DBA_TAB_COLS WHERE OWNER = '{p[0]}' AND TABLE_NAME = '{p[1]}'");
            if (dt != null) foreach (DataRow r in dt.Rows) clbColumns.Items.Add(r["COLUMN_NAME"].ToString());
        }
    }
}