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
            cboGrantee.DropDownStyle = ComboBoxStyle.DropDown;
            cboObjectName.DropDownStyle = ComboBoxStyle.DropDown;

           
            cboGrantee.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboGrantee.AutoCompleteSource = AutoCompleteSource.ListItems;

            cboObjectName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboObjectName.AutoCompleteSource = AutoCompleteSource.ListItems;

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

        //private void btnGrant_Click(object sender, EventArgs e)
        //{
        //    string grantee = cboGrantee.Text;
        //    string fullObj = cboObjectName.Text;
        //    string objNameOnly = fullObj.Contains(".") ? fullObj.Split('.')[1] : fullObj;

        //    string grantOpt = chkWithGrantOption.Checked
        //        ? (cboObjectType.Text == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION")
        //        : "";

        //    DatabaseHelper db = new DatabaseHelper();
        //    db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

        //    if (clbPrivs.CheckedItems.Count == 0) { MessageBox.Show("Chọn ít nhất 1 quyền!"); return; }

        //    try
        //    {
        //        foreach (string priv in clbPrivs.CheckedItems)
        //        {
        //            bool hasColumns = _memory.ContainsKey(priv) && _memory[priv].Count > 0;
        //            string sql = "";

        //            if (priv == "SELECT" && hasColumns)
        //            {
        //                string columnList = string.Join(", ", _memory[priv]);
        //                string viewName = $"V_SEC_{objNameOnly}_{grantee.Replace("#", "")}";

        //                string createViewSql = $"CREATE OR REPLACE VIEW {viewName} AS SELECT {columnList} FROM {fullObj}";
        //                db.ExecuteNonQuery(createViewSql);

        //                sql = $"GRANT SELECT ON {viewName} TO {grantee}{grantOpt}";
        //            }
        //            else if (priv == "UPDATE" && hasColumns)
        //            {
        //                string columnList = string.Join(", ", _memory[priv]);
        //                sql = $"GRANT UPDATE({columnList}) ON {fullObj} TO {grantee}{grantOpt}";
        //            }
        //            else
        //            {
        //                sql = $"GRANT {priv} ON {fullObj} TO {grantee}{grantOpt}";
        //            }

        //            db.ExecuteNonQuery(sql);
        //        }
        //        MessageBox.Show("Cấp quyền thành công! Đã áp dụng chính sách bảo mật cho các cột được chọn.",
        //        "Thông báo",
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi trong quá trình cấp quyền: " + ex.Message);
        //    }
        //}

        private void btnGrant_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.Text;
            string fullObj = cboObjectName.Text;
            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(fullObj)) return; // Tránh lỗi rỗng

            string objNameOnly = fullObj.Contains(".") ? fullObj.Split('.')[1] : fullObj;

            string grantOpt = chkWithGrantOption.Checked
                ? (cboObjectType.Text == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION")
                : "";

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            if (clbPrivs.CheckedItems.Count == 0) { MessageBox.Show("Vui lòng chọn ít nhất 1 quyền!"); return; }

            // --- 2 DANH SÁCH ĐỂ THEO DÕI KẾT QUẢ THỰC TẾ ---
            List<string> successList = new List<string>();
            List<string> failList = new List<string>();

            try
            {
                foreach (string priv in clbPrivs.CheckedItems)
                {
                    bool hasColumns = _memory.ContainsKey(priv) && _memory[priv].Count > 0;
                    string sql = "";
                    bool isSuccess = false;

                    if (priv == "SELECT" && hasColumns)
                    {
                        string columnList = string.Join(", ", _memory[priv]);
                        string viewName = $"V_SEC_{objNameOnly}_{grantee.Replace("#", "")}";

                        string createViewSql = $"CREATE OR REPLACE VIEW {viewName} AS SELECT {columnList} FROM {fullObj}";

                        // Phải tạo View thành công thì mới đi Grant
                        if (db.ExecuteNonQuery(createViewSql))
                        {
                            sql = $"GRANT SELECT ON {viewName} TO \"{grantee}\"{grantOpt}";
                            isSuccess = db.ExecuteNonQuery(sql);
                        }
                    }
                    else if (priv == "UPDATE" && hasColumns)
                    {
                        string columnList = string.Join(", ", _memory[priv]);
                        sql = $"GRANT UPDATE({columnList}) ON {fullObj} TO \"{grantee}\"{grantOpt}";
                        isSuccess = db.ExecuteNonQuery(sql);
                    }
                    else
                    {
                        sql = $"GRANT {priv} ON {fullObj} TO \"{grantee}\"{grantOpt}";
                        isSuccess = db.ExecuteNonQuery(sql);
                    }

                    // KIỂM TRA KẾT QUẢ TỪNG LỆNH
                    if (isSuccess) successList.Add(priv);
                    else failList.Add(priv);
                }

                // --- HIỂN THỊ THÔNG BÁO DỰA TRÊN KẾT QUẢ THẬT ---
                string resultMsg = "";
                if (successList.Count > 0)
                    resultMsg += $"✅ Thành công: {string.Join(", ", successList)}\n";

                

                if (successList.Count > 0)
                {
                    MessageBox.Show(resultMsg, "Kết quả thực thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
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

            LoadExistingPrivs();
        }

        private void LoadExistingPrivs()
        {
            string grantee = cboGrantee.Text.ToUpper();
            string fullObj = cboObjectName.Text.ToUpper();

            if (string.IsNullOrEmpty(grantee) || !fullObj.Contains(".")) return;

            string[] parts = fullObj.Split('.');
            string owner = parts[0];
            string tableName = parts[1];

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            // 1. Reset toàn bộ CheckedListBox về trạng thái chưa chọn
            for (int i = 0; i < clbPrivs.Items.Count; i++)
            {
                clbPrivs.SetItemChecked(i, false);
            }

            // 2. Query lấy các quyền hệ thống đã cấp (Table-level)
            string sql = $@"SELECT PRIVILEGE FROM DBA_TAB_PRIVS 
                    WHERE GRANTEE = '{grantee}' 
                    AND OWNER = '{owner}' 
                    AND TABLE_NAME = '{tableName}'";

            DataTable dt = db.ExecuteQuery(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string priv = row["PRIVILEGE"].ToString();
                    // Nếu tìm thấy quyền (SELECT, INSERT...), tự động tích chọn
                    for (int i = 0; i < clbPrivs.Items.Count; i++)
                    {
                        if (clbPrivs.Items[i].ToString() == priv)
                        {
                            clbPrivs.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }
    }

}



