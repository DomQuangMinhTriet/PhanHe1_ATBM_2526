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

            // VẤN ĐỀ 1: Chặn không cho tạo khi đang để "Tất cả"
            if (loai == "Tất cả")
            {
                MessageBox.Show("Vui lòng chọn cụ thể loại muốn tạo là 'User' hoặc 'Role'!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên đối tượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string query = "";
            if (loai == "User")
            {
                if (string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("User bắt buộc phải có mật khẩu!", "Thông báo");
                    return;
                }
                // Bọc ngoặc kép để hỗ trợ C## và các tên có khoảng trắng
                query = $"CREATE USER \"{ten}\" IDENTIFIED BY \"{matKhau}\"";
            }
            else // Trường hợp là Role
            {
                query = $"CREATE ROLE \"{ten}\"";
            }

            if (db.ExecuteNonQuery(query))
            {
                // Tự động cấp quyền login nếu là User
                if (loai == "User")
                {
                    db.ExecuteNonQuery($"GRANT CREATE SESSION TO \"{ten}\"");
                }

                // VẤN ĐỀ 2: Fix lại câu thông báo cho khớp với loại đã chọn
                MessageBox.Show($"Đã tạo thành công {loai}: {ten}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnLoad_Click(null, null); // Load lại danh sách để thấy cái mới nhất ở dòng 1
            }
        }

    

        public void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn User hoặc Role cần xóa trong danh sách!", "Thông báo");
                return;
            }

            // 1. Lấy Tên và Loại thực tế từ dòng đang chọn trên DataGridView
            string ten = dgvList.CurrentRow.Cells["NAME"].Value?.ToString();

            // Mẹo: Nếu đang ở chế độ "Tất cả" thì lấy từ cột TYPE, nếu không thì lấy từ ComboBox
            string loaiThucTe = "";
            if (dgvList.Columns.Contains("TYPE"))
            {
                loaiThucTe = dgvList.CurrentRow.Cells["TYPE"].Value?.ToString().ToUpper(); // Trả về 'USER' hoặc 'ROLE'
            }
            else
            {
                loaiThucTe = cboType.SelectedItem.ToString().ToUpper();
            }

            if (string.IsNullOrEmpty(ten)) return;

            // 2. Hiện thông báo xác nhận "xịn" như đã bàn
            DialogResult result = MessageBox.Show(
                $"CẢNH BÁO: Bạn có chắc chắn muốn xóa {loaiThucTe} [{ten}] không?\nToàn bộ quyền và dữ liệu liên quan sẽ bị mất!",
                "Xác nhận xóa đối tượng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No) return;

            // 3. Thực thi SQL dựa trên loại thực tế
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            // Bọc tên trong dấu ngoặc kép để tránh lỗi nếu tên có ký tự đặc biệt (như C##)
            string query = (loaiThucTe == "USER" || loaiThucTe == "User")
                           ? $"DROP USER \"{ten}\" CASCADE"
                           : $"DROP ROLE \"{ten}\"";

            if (db.ExecuteNonQuery(query))
            {
                MessageBox.Show($"Đã xóa thành công {loaiThucTe} [{ten}].", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoad_Click(null, null); // Load lại danh sách
            }
        }

    

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn User hoặc Role cần cập nhật từ danh sách!", "Thông báo");
                return;
            }

            // 1. Lấy Tên, Mật khẩu mới và Loại thực tế từ dòng đang chọn
            string ten = dgvList.CurrentRow.Cells["NAME"].Value?.ToString();
            string matKhauMoi = GetInputText(txtMatKhau);

            // Xác định loại dựa trên cột TYPE (nếu đang ở chế độ 'Tất cả') hoặc ComboBox
            string loaiThucTe = "";
            if (dgvList.Columns.Contains("TYPE"))
            {
                loaiThucTe = dgvList.CurrentRow.Cells["TYPE"].Value?.ToString().ToUpper();
            }
            else
            {
                loaiThucTe = cboType.SelectedItem.ToString().ToUpper();
            }

            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới cần thay đổi!", "Thông báo");
                return;
            }

            // 2. Thực thi SQL
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            // Câu lệnh SQL linh hoạt: ALTER USER hoặc ALTER ROLE
            string query = (loaiThucTe == "USER" || loaiThucTe == "User")
                           ? $"ALTER USER \"{ten}\" IDENTIFIED BY \"{matKhauMoi}\""
                           : $"ALTER ROLE \"{ten}\" IDENTIFIED BY \"{matKhauMoi}\"";

            if (db.ExecuteNonQuery(query))
            {
                MessageBox.Show($"Đã cập nhật mật khẩu thành công cho {loaiThucTe} [{ten}].",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Dọn dẹp ô mật khẩu sau khi đổi xong cho sạch sẽ
                txtMatKhau.Clear();
                btnLoad_Click(null, null);
            }
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