

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

//namespace OracleSecurityAdmin
//{
//    public partial class uc_Grant : UserControl
//    {
//        private string _host, _port, _serviceName, _username, _password;

//        // KHO LƯU TRỮ: Quyền nào đi với danh sách cột đó
//        private Dictionary<string, List<string>> _privilegeColumns = new Dictionary<string, List<string>>();

//        public uc_Grant() { InitializeComponent(); }

//        public void SetConfig(string h, string p, string s, string u, string pass)
//        {
//            _host = h; _port = p; _serviceName = s; _username = u; _password = pass;
//        }

//        private void uc_Grant_Load(object sender, EventArgs e)
//        {
//            cboGranteeType.SelectedIndex = 0;
//            cboObjectType.SelectedIndex = 0;
//        }

//        // --- MỖI KHI BẤM CHỌN QUYỀN: RESET VÀ HIỆN CỘT TƯƠNG ỨNG ---
//        private void clbPrivs_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string currentPriv = clbPrivs.SelectedItem?.ToString();
//            if (string.IsNullOrEmpty(currentPriv)) return;

//            // 1. Lưu lại trạng thái của quyền TRƯỚC ĐÓ trước khi chuyển sang quyền mới (nếu cần)
//            // (Phần này nâng cao, tạm thời mình làm Reset cho đơn giản theo ý Tồ)

//            // 2. Kiểm tra quyền có hỗ trợ mức cột không
//            if (currentPriv == "SELECT" || currentPriv == "UPDATE")
//            {
//                clbColumns.Enabled = true;
//                lblColumns.Text = $"3c. Chọn cột RIÊNG cho: {currentPriv}";
//                lblColumns.ForeColor = Color.Blue;

//                // RESET trạng thái cột: Bỏ chọn tất cả để Tồ chọn lại cho quyền này
//                for (int i = 0; i < clbColumns.Items.Count; i++)
//                    clbColumns.SetItemChecked(i, false);
//            }
//            else
//            {
//                clbColumns.Enabled = false;
//                lblColumns.Text = $"3c. {currentPriv} cấp trên TOÀN BẢNG";
//                lblColumns.ForeColor = Color.Red;
//                for (int i = 0; i < clbColumns.Items.Count; i++)
//                    clbColumns.SetItemChecked(i, false);
//            }
//        }

//        // --- KHI BẤM GRANT: THỰC THI THÔNG MINH ---
//        private void btnGrant_Click(object sender, EventArgs e)
//        {
//            string grantee = cboGrantee.Text;
//            string objName = cboObjectName.Text;
//            string grantOpt = chkWithGrantOption.Checked ? (cboObjectType.Text == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION") : "";

//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

//            if (clbPrivs.CheckedItems.Count == 0)
//            {
//                MessageBox.Show("Vui lòng chọn ít nhất 1 quyền!"); return;
//            }

//            foreach (string priv in clbPrivs.CheckedItems)
//            {
//                string sql = "";
//                // Nếu đang focus vào đúng quyền đó và có chọn cột
//                if ((priv == "SELECT" || priv == "UPDATE") && clbPrivs.SelectedItem?.ToString() == priv && clbColumns.CheckedItems.Count > 0)
//                {
//                    string cols = "";
//                    foreach (var c in clbColumns.CheckedItems) cols += c.ToString() + ",";
//                    sql = $"GRANT {priv}({cols.TrimEnd(',')}) ON {objName} TO {grantee}{grantOpt}";
//                }
//                else
//                {
//                    sql = $"GRANT {priv} ON {objName} TO {grantee}{grantOpt}";
//                }
//                db.ExecuteNonQuery(sql);
//            }
//            MessageBox.Show("Đã cấp quyền rạch ròi thành công!");
//        }

//        // --- CÁC HÀM LOAD DATA (Giữ nguyên) ---
//        private void cboGranteeType_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(_host)) return;
//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            string q = (cboGranteeType.Text == "User") ? "SELECT USERNAME AS NAME FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N'" : "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'";
//            DataTable dt = db.ExecuteQuery(q);
//            cboGrantee.Items.Clear();
//            if (dt != null) foreach (DataRow r in dt.Rows) cboGrantee.Items.Add(r["NAME"].ToString());
//        }

//        private void cboObjectType_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string type = cboObjectType.Text;
//            clbPrivs.Items.Clear();
//            if (type == "TABLE" || type == "VIEW") clbPrivs.Items.AddRange(new string[] { "SELECT", "INSERT", "UPDATE", "DELETE" });
//            else if (type == "PROCEDURE" || type == "FUNCTION") clbPrivs.Items.AddRange(new string[] { "EXECUTE" });

//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            string q = (type == "ROLE") ? "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'" : $"SELECT OWNER || '.' || OBJECT_NAME AS NAME FROM DBA_OBJECTS WHERE OBJECT_TYPE = '{type}' AND ORACLE_MAINTAINED = 'N'";
//            DataTable dt = db.ExecuteQuery(q);
//            cboObjectName.Items.Clear();
//            if (dt != null) foreach (DataRow r in dt.Rows) cboObjectName.Items.Add(r["NAME"].ToString());
//        }

//        private void cboObjectName_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            clbColumns.Items.Clear();
//            string fullObj = cboObjectName.Text;
//            if (!fullObj.Contains(".")) return;
//            string[] p = fullObj.Split('.');
//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            DataTable dt = db.ExecuteQuery($"SELECT COLUMN_NAME FROM DBA_TAB_COLS WHERE OWNER = '{p[0]}' AND TABLE_NAME = '{p[1]}'");
//            if (dt != null) foreach (DataRow r in dt.Rows) clbColumns.Items.Add(r["COLUMN_NAME"].ToString());
//        }
//    }
//}

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

        // BỘ NHỚ: Lưu danh sách các cột đã chọn cho từng quyền
        // Key: Tên quyền (SELECT, UPDATE...), Value: Danh sách tên cột
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

        // --- 1. KHI BẤM CHUỘT CHUYỂN DÒNG (SELECT <-> UPDATE <-> DELETE) ---
        //private void clbPrivs_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string priv = clbPrivs.SelectedItem?.ToString();
        //    if (string.IsNullOrEmpty(priv)) return;

        //    // Nếu là SELECT hoặc UPDATE thì mở bảng cột và NẠP LẠI BỘ NHỚ
        //    if (priv == "SELECT" || priv == "UPDATE")
        //    {
        //        clbColumns.Enabled = true;
        //        lblColumns.Text = $"3c. Đang xem cột của: {priv}";
        //        lblColumns.ForeColor = Color.Blue;

        //        // Tạm thời gỡ sự kiện ItemCheck để không bị lặp khi nạp dữ liệu
        //        clbColumns.ItemCheck -= clbColumns_ItemCheck;

        //        // Nạp lại các dấu tích từ bộ nhớ
        //        if (!_memory.ContainsKey(priv)) _memory[priv] = new HashSet<string>();
        //        var savedCols = _memory[priv];

        //        for (int i = 0; i < clbColumns.Items.Count; i++)
        //        {
        //            string colName = clbColumns.Items[i].ToString();
        //            clbColumns.SetItemChecked(i, savedCols.Contains(colName));
        //        }

        //        // Nối lại sự kiện sau khi nạp xong
        //        clbColumns.ItemCheck += clbColumns_ItemCheck;
        //    }
        //    else
        //    {
        //        // INSERT/DELETE: Khóa bảng cột và báo lỗi
        //        clbColumns.Enabled = false;
        //        lblColumns.Text = $"3c. {priv} không hỗ trợ mức cột!";
        //        lblColumns.ForeColor = Color.Red;
        //    }
        //}

        private void clbPrivs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string priv = clbPrivs.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(priv)) return;

            // Tạm gỡ sự kiện để việc xóa/tích bằng code không kích hoạt lưu đè vào bộ nhớ
            clbColumns.ItemCheck -= clbColumns_ItemCheck;

            if (priv == "SELECT" || priv == "UPDATE")
            {
                clbColumns.Enabled = true;
                lblColumns.Text = $"3c. Đang chọn cột cho: {priv}";
                lblColumns.ForeColor = Color.Blue;

                // 1. Lấy dữ liệu từ bộ nhớ
                if (!_memory.ContainsKey(priv)) _memory[priv] = new HashSet<string>();
                var savedCols = _memory[priv];

                // 2. Hiển thị lên giao diện (tích lại những gì đã lưu)
                for (int i = 0; i < clbColumns.Items.Count; i++)
                {
                    string colName = clbColumns.Items[i].ToString();
                    clbColumns.SetItemChecked(i, savedCols.Contains(colName));
                }
            }
            else
            {
                // --- XỬ LÝ TRIỆT ĐỂ CHO INSERT/DELETE/EXECUTE ---
                clbColumns.Enabled = false;
                lblColumns.Text = $"3c. {priv} cấp trên TOÀN BẢNG (Không dùng cột)";
                lblColumns.ForeColor = Color.Red;

                // XÓA TRẮNG dấu tích trên giao diện ngay lập tức
                for (int i = 0; i < clbColumns.Items.Count; i++)
                {
                    clbColumns.SetItemChecked(i, false);
                }
            }

            // Nối lại sự kiện để tiếp tục ghi nhận nếu người dùng tích mới
            clbColumns.ItemCheck += clbColumns_ItemCheck;
        }

        // --- 2. KHI TÍCH VÀO CỘT: LƯU NGAY VÀO BỘ NHỚ ---
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

        // --- 3. NÚT GRANT: QUÉT BỘ NHỚ ĐỂ XUẤT SQL (CHUẨN NHẤT) ---
        private void btnGrant_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.Text;
            string objName = cboObjectName.Text;
            string grantOpt = chkWithGrantOption.Checked ? (cboObjectType.Text == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION") : "";

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            if (clbPrivs.CheckedItems.Count == 0) { MessageBox.Show("Chọn ít nhất 1 quyền!"); return; }

            foreach (string priv in clbPrivs.CheckedItems)
            {
                string sql = "";
                // Nếu quyền có trong bộ nhớ và có chọn cột
                if (_memory.ContainsKey(priv) && _memory[priv].Count > 0)
                {
                    string cols = string.Join(",", _memory[priv]);
                    sql = $"GRANT {priv}({cols}) ON {objName} TO {grantee}{grantOpt}";
                }
                else
                {
                    sql = $"GRANT {priv} ON {objName} TO {grantee}{grantOpt}";
                }
                db.ExecuteNonQuery(sql);
            }
            MessageBox.Show("Cấp quyền thành công");
        }

        // --- CÁC HÀM LOAD DATA (Giữ nguyên) ---
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
            _memory.Clear(); // Đổi object thì xóa bộ nhớ cũ
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