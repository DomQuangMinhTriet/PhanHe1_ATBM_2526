//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client; // Đừng quên thư viện này

//namespace OracleSecurityAdmin
//{
//    public partial class uc_Management : UserControl
//    {
//        // Biến lưu thông tin kết nối được truyền từ Form1
//        private string _host, _port, _serviceName, _username, _password;
//        private bool _isPasswordVisible;

//        public uc_Management()
//        {
//            InitializeComponent();
//            InitPlaceholders();
//        }

//        // Hàm này để Form1 gọi và truyền dữ liệu sang
//        public void SetConfig(string host, string port, string service, string user, string pass)
//        {
//            _host = host;
//            _port = port;
//            _serviceName = service;
//            _username = user;
//            _password = pass;
//        }

//        private void uc_Management_Load(object sender, EventArgs e)
//        {
//            // Cấu hình DataGridView 
//            dgvList.AllowUserToAddRows = false;
//            dgvList.ReadOnly = true;
//            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            dgvList.MultiSelect = false;
//            dgvList.RowHeadersVisible = false;
//            dgvList.BackgroundColor = Color.White;

//            // Đăng ký sự kiện
//            dgvList.CellClick += DgvList_CellClick;
//            cboType.SelectedIndexChanged += CboType_SelectedIndexChanged;

//            // Mặc định
//            cboType.SelectedIndex = 0;
//            ApplyPlaceholderIfEmpty(txtTen);
//            ApplyPlaceholderIfEmpty(txtMatKhau);
//        }

//        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.RowIndex < dgvList.Rows.Count)
//            {
//                DataGridViewRow row = dgvList.Rows[e.RowIndex];
//                string type = cboType.SelectedItem?.ToString();

//                if (type == "User" && dgvList.Columns.Contains("USERNAME"))
//                {
//                    txtTen.Text = row.Cells["USERNAME"].Value?.ToString();
//                }
//                else if (type == "Role" && dgvList.Columns.Contains("ROLE"))
//                {
//                    txtTen.Text = row.Cells["ROLE"].Value?.ToString();
//                }
//                txtMatKhau.Clear();
//                ApplyPlaceholderIfEmpty(txtMatKhau);
//            }
//        }

//        private void CboType_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtTen.Clear();
//            txtMatKhau.Clear();
//            ApplyPlaceholderIfEmpty(txtTen);
//            ApplyPlaceholderIfEmpty(txtMatKhau);
//            btnLoad_Click(null, null);
//        }

//        private void btnLoad_Click(object sender, EventArgs e)
//        {
//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

//            string query = "";
//            string loai = cboType.SelectedItem?.ToString();

//            if (loai == "User")
//            {
//                query = "SELECT USERNAME, ACCOUNT_STATUS, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N' ORDER BY CREATED DESC";
//            }
//            else if (loai == "Role")
//            {
//                query = "SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N' ORDER BY ROLE_ID DESC";
//            }

//            DataTable dt = db.ExecuteQuery(query);
//            if (dt != null)
//            {
//                dgvList.DataSource = dt;
//                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            }
//        }

//        private void btnCreate_Click(object sender, EventArgs e)
//        {
//            string ten = GetInputText(txtTen).ToUpper();
//            string matKhau = GetInputText(txtMatKhau);
//            string loai = cboType.SelectedItem?.ToString();

//            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(loai))
//            {
//                MessageBox.Show("Vui lòng nhập Tên và chọn loại (User/Role)!", "Cảnh báo");
//                return;
//            }

//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            string query = "";

//            if (loai == "User")
//            {
//                if (string.IsNullOrEmpty(matKhau)) { MessageBox.Show("Vui lòng nhập mật khẩu!"); return; }
//                query = $@"BEGIN 
//                            EXECUTE IMMEDIATE 'CREATE USER {ten} IDENTIFIED BY ""{matKhau}""';
//                            EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO {ten}';
//                          END;";
//            }
//            else if (loai == "Role")
//            {
//                query = $"CREATE ROLE {ten}";
//            }

//            if (db.ExecuteNonQuery(query))
//            {
//                MessageBox.Show($"Đã tạo {loai} '{ten}' thành công!");
//                btnLoad_Click(sender, e);
//            }
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            string ten = GetInputText(txtTen).ToUpper();
//            string loai = cboType.SelectedItem?.ToString();

//            if (string.IsNullOrEmpty(ten)) return;

//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            string query = loai == "User" ? $"DROP USER {ten} CASCADE" : $"DROP ROLE {ten}";

//            if (MessageBox.Show($"Xác nhận xóa {loai} '{ten}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                if (db.ExecuteNonQuery(query))
//                {
//                    MessageBox.Show("Xóa thành công!");
//                    btnLoad_Click(sender, e);
//                }
//            }
//        }

//        private void btnUpdate_Click(object sender, EventArgs e)
//        {
//            string ten = GetInputText(txtTen).ToUpper();
//            string matKhau = GetInputText(txtMatKhau);
//            string loai = cboType.SelectedItem?.ToString();

//            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(matKhau)) return;

//            DatabaseHelper db = new DatabaseHelper();
//            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
//            string query = loai == "User" ? $"ALTER USER {ten} IDENTIFIED BY {matKhau}" : $"ALTER ROLE {ten} IDENTIFIED BY {matKhau}";

//            if (db.ExecuteNonQuery(query))
//            {
//                MessageBox.Show("Cập nhật thành công!");
//                btnLoad_Click(sender, e);
//            }
//        }

//        private void btnLogout_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                Application.Restart();
//            }
//        }

//        private void btnTogglePassword_Click(object sender, EventArgs e)
//        {
//            var info = txtMatKhau.Tag as PlaceholderInfo;
//            _isPasswordVisible = !_isPasswordVisible;
//            if (info != null && !IsPlaceholder(txtMatKhau, info))
//                txtMatKhau.UseSystemPasswordChar = !_isPasswordVisible;
//        }

//        // --- CÁC HÀM HỖ TRỢ PLACEHOLDER (GIỮ NGUYÊN TỪ CODE CŨ) ---
//        private void InitPlaceholders()
//        {
//            SetupPlaceholder(txtTen, "Enter user/role name", false);
//            SetupPlaceholder(txtMatKhau, "Enter password", true);
//        }

//        private void SetupPlaceholder(TextBox box, string placeholder, bool isPassword)
//        {
//            box.Tag = new PlaceholderInfo { Text = placeholder, IsPassword = isPassword };
//            box.ForeColor = Color.Gray;
//            box.Text = placeholder;
//            box.Enter += (s, e) => {
//                var info = box.Tag as PlaceholderInfo;
//                if (IsPlaceholder(box, info))
//                {
//                    box.Text = ""; box.ForeColor = Color.Black;
//                    if (info.IsPassword) box.UseSystemPasswordChar = !_isPasswordVisible;
//                }
//            };
//            box.Leave += (s, e) => ApplyPlaceholderIfEmpty(box);
//        }

//        private bool IsPlaceholder(TextBox box, PlaceholderInfo info) => box.ForeColor == Color.Gray && box.Text == info.Text;

//        private string GetInputText(TextBox box)
//        {
//            var info = box.Tag as PlaceholderInfo;
//            return (info != null && IsPlaceholder(box, info)) ? "" : box.Text.Trim();
//        }

//        private void ApplyPlaceholderIfEmpty(TextBox box)
//        {
//            var info = box.Tag as PlaceholderInfo;
//            if (info != null && string.IsNullOrWhiteSpace(box.Text))
//            {
//                box.Text = info.Text; box.ForeColor = Color.Gray;
//                if (info.IsPassword) box.UseSystemPasswordChar = false;
//            }
//        }

//        private class PlaceholderInfo { public string Text { get; set; } public bool IsPassword { get; set; } }
//    }
//}

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

            // Gán lại index để kích hoạt tự động load nếu cần
            cboType.SelectedIndex = 0;
            ApplyPlaceholderIfEmpty(txtTen);
            ApplyPlaceholderIfEmpty(txtMatKhau);
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_host) || string.IsNullOrEmpty(_username)) return;
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = "";
            string loai = cboType.SelectedItem?.ToString();
            if (loai == "User") query = "SELECT USERNAME, ACCOUNT_STATUS, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N' ORDER BY CREATED DESC";
            else if (loai == "Role") query = "SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N' ORDER BY ROLE_ID DESC";

            DataTable dt = db.ExecuteQuery(query);
            if (dt != null) dgvList.DataSource = dt;
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
                MessageBox.Show("Thành công!");
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

            if (result == DialogResult.No) return; // Nếu chọn No thì nghỉ, không làm gì cả
                                                   // -------------------------

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

        // --- GIỮ NGUYÊN CÁC HÀM PLACEHOLDER CŨ ---
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