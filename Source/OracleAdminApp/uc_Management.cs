using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public partial class uc_Management : UserControl
    {
        private string _host, _port, _serviceName, _username, _password;
        private bool _isPasswordVisible;

        public uc_Management()
        {
            InitializeComponent();
            InitPlaceholders();
        }

        public void SetConfig(string host, string port, string service, string user, string pass)
        {
            _host = host; _port = port; _serviceName = service;
            _username = user; _password = pass;
        }

        private void uc_Management_Load(object sender, EventArgs e)
        {
            dgvList.AllowUserToAddRows = false;
            dgvList.ReadOnly = true;
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.BackgroundColor = Color.White;

            cboType.Items.Clear();
            cboType.Items.AddRange(new object[] { "Tất cả", "User", "Role" });

            cboType.SelectedIndex = 0;
            ApplyPlaceholderIfEmpty(txtTen);
            ApplyPlaceholderIfEmpty(txtMatKhau);
        }

        //public void btnLoad_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(_host) || string.IsNullOrEmpty(_username)) return;
        //    DatabaseHelper db = new DatabaseHelper();
        //    db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
        //    string query = "";
        //    string loai = cboType.SelectedItem?.ToString();
        //    if (loai == "User") query = "SELECT USERNAME, ACCOUNT_STATUS, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N' ORDER BY CREATED DESC";
        //    else if (loai == "Role") query = "SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N' ORDER BY ROLE_ID DESC";

        //    DataTable dt = db.ExecuteQuery(query);
        //    if (dt != null) dgvList.DataSource = dt;
        //}

        public void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_host) || string.IsNullOrEmpty(_username)) return;

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string query = "";
            string loai = cboType.SelectedItem?.ToString();

            if (loai == "User")
            {
                // CHỈ lấy User, lọc bỏ các Schema hệ thống
                query = @"
                    SELECT 
                        u.USERNAME as ""NAME"", 
                        CASE 
                            WHEN (SELECT COUNT(*) FROM DBA_SYS_PRIVS s WHERE s.GRANTEE = u.USERNAME AND s.PRIVILEGE = 'CREATE SESSION') > 0 
                            THEN 'ON' 
                            ELSE 'OFF' 
                        END as ""STATUS"", 
                        u.ACCOUNT_STATUS as ""ACCOUNT_INFO"",
                        u.CREATED 
                    FROM DBA_USERS u 
                    WHERE u.ORACLE_MAINTAINED = 'N' 
                    ORDER BY u.CREATED DESC";
                //query = "SELECT USERNAME as NAME, ACCOUNT_STATUS as STATUS, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N' ORDER BY CREATED DESC";
            }   
            else if (loai == "Role")
            {
                // CHỈ lấy Role
                query = @"
                    SELECT 
                        ROLE as ""NAME"", 
                        'VALID' as ""STATUS"", 
                        PASSWORD_REQUIRED as ""AUTH_TYPE""
                    FROM DBA_ROLES 
                    WHERE ORACLE_MAINTAINED = 'N' 
                    ORDER BY ROLE ASC";
                //query = "SELECT ROLE as NAME, 'N/A' as STATUS, PASSWORD_REQUIRED FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N' ORDER BY ROLE ASC";
            }
            else // Trường hợp "Tất cả"
            {
                // Dùng UNION để hiện cả 2 loại trên cùng một danh sách
                //    query = @"
                //SELECT USERNAME as NAME, 'USER' as TYPE, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N'
                //UNION ALL
                //SELECT ROLE as NAME, 'ROLE' as TYPE, TO_DATE(NULL) FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'
                //ORDER BY 1 ASC";

                query = @"
                    SELECT u.USERNAME as ""NAME"", 'USER' as ""TYPE"", 
                           CASE WHEN (SELECT COUNT(*) FROM DBA_SYS_PRIVS s WHERE s.GRANTEE = u.USERNAME AND s.PRIVILEGE = 'CREATE SESSION') > 0 
                           THEN 'ON' ELSE 'OFF' END as ""STATUS""
                    FROM DBA_USERS u WHERE u.ORACLE_MAINTAINED = 'N'
                    UNION ALL
                    SELECT ROLE as ""NAME"", 'ROLE' as ""TYPE"", 'VALID' as ""STATUS"" 
                    FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'
                    ORDER BY 1 ASC";
            }

            DataTable dt = db.ExecuteQuery(query);
            dgvList.DataSource = null; // Xóa sạch dữ liệu cũ để tránh lỗi cột
            if (dt != null)
            {
                dgvList.DataSource = dt;
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        public void btnCreate_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string matKhau = GetInputText(txtMatKhau);
            string loai = cboType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(ten)) return;

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = (loai == "User") ? $"CREATE USER {ten} IDENTIFIED BY {matKhau}" : $"CREATE ROLE {ten}";

            if (db.ExecuteNonQuery(query))
            {
                if (loai == "User")
                {
                    // Cấp quyền kết nối cơ bản để User có thể login được ngay
                    db.ExecuteNonQuery($"GRANT CREATE SESSION TO {ten}");
                }
                // -----------------------------

                MessageBox.Show($"Đã tạo {loai} {ten} thành công!");
                btnLoad_Click(null, null);
            }
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string loai = cboType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(ten)) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {loai} [{ten}] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No) return;

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = loai == "User" ? $"DROP USER {ten} CASCADE" : $"DROP ROLE {ten}";

            if (db.ExecuteNonQuery(query)) btnLoad_Click(null, null);
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string matKhau = GetInputText(txtMatKhau);
            string loai = cboType.SelectedItem?.ToString();
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = (loai == "User") ? $"ALTER USER {ten} IDENTIFIED BY {matKhau}" : $"ALTER ROLE {ten} IDENTIFIED BY {matKhau}";
            if (db.ExecuteNonQuery(query)) MessageBox.Show("Cập nhật thành công!");
        }

        public void btnLogout_Click(object sender, EventArgs e) { Application.Restart(); }

        public void btnTogglePassword_Click(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            txtMatKhau.UseSystemPasswordChar = !_isPasswordVisible;
        }

        public void CboType_SelectedIndexChanged(object sender, EventArgs e) { btnLoad_Click(null, null); }

        public void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtTen.Text = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void InitPlaceholders() { SetupPlaceholder(txtTen, "Enter user/role name", false); SetupPlaceholder(txtMatKhau, "Enter password", true); }
        private void SetupPlaceholder(TextBox box, string placeholder, bool isPassword)
        {
            box.Tag = new PlaceholderInfo { Text = placeholder, IsPassword = isPassword };
            box.ForeColor = Color.Gray; box.Text = placeholder;
            box.Enter += (s, ev) => { if (IsPlaceholder(box, (PlaceholderInfo)box.Tag)) { box.Text = ""; box.ForeColor = Color.Black; if (isPassword) box.UseSystemPasswordChar = !_isPasswordVisible; } };
            box.Leave += (s, ev) => ApplyPlaceholderIfEmpty(box);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private bool IsPlaceholder(TextBox box, PlaceholderInfo info) => box.ForeColor == Color.Gray && box.Text == info.Text;
        private string GetInputText(TextBox box) { var info = box.Tag as PlaceholderInfo; return IsPlaceholder(box, info) ? "" : box.Text.Trim(); }
        private void ApplyPlaceholderIfEmpty(TextBox box)
        {
            var info = box.Tag as PlaceholderInfo;
            if (string.IsNullOrWhiteSpace(box.Text)) { box.Text = info.Text; box.ForeColor = Color.Gray; box.UseSystemPasswordChar = false; }
        }
        private class PlaceholderInfo { public string Text; public bool IsPassword; }
    }
}