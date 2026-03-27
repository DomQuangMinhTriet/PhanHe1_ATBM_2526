using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public partial class Form1 : Form
    {
        // Biến toàn cục lưu thông tin đăng nhập từ LoginForm truyền sang
        private string _host, _port, _serviceName, _username, _password;
        private TabControl tabControl1;
        private TabPage tabUserRole;
        private TabPage tabPage2;
        private Button btnLogout;
        private Label lblName;
        private Label lblPassword;
        private Label lblChoose;
        private Button btnLoad;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCreate;
        private TextBox txtMatKhau;
        private TextBox txtTen;
        private Button btnTogglePassword;
        private DataGridView dgvList;
        private ComboBox cboType;
        private bool _isPasswordVisible;

        // === CÁC BIẾN MỚI THÊM CHO TAB 2 (GRANT/REVOKE) ===
        private Label lblGranteeType;  // MỚI THÊM: Nhãn cho Loại Grantee
        private ComboBox cboGranteeType; // MỚI THÊM: Ô chọn Loại (User/Role)
        private Label lblGrantee;
        private ComboBox cboGrantee;
        private Button btnViewPrivs;
        private DataGridView dgvPrivs;
        private Label lblObjectType;
        private ComboBox cboObjectType;
        private Label lblObjectName;
        private ComboBox cboObjectName;
        private Label lblPrivs;
        private CheckedListBox clbPrivs;
        private Label lblColumns;
        private CheckedListBox clbColumns;
        private CheckBox chkWithGrantOption;
        private Button btnGrantPriv;
        private Button btnRevokePriv;


        public Form1()
        {
            InitializeComponent();
            InitPlaceholders();
        }
        // Constructor nhận tham số từ LoginForm
        public Form1(string host, string port, string serviceName, string username, string password)
        {
            InitializeComponent();
            _host = host;
            _port = port;
            _serviceName = serviceName;
            _username = username;
            _password = password;
            InitPlaceholders();
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUserRole = new System.Windows.Forms.TabPage();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblChoose = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblGranteeType = new System.Windows.Forms.Label();
            this.cboGranteeType = new System.Windows.Forms.ComboBox();
            this.lblGrantee = new System.Windows.Forms.Label();
            this.cboGrantee = new System.Windows.Forms.ComboBox();
            this.btnViewPrivs = new System.Windows.Forms.Button();
            this.dgvPrivs = new System.Windows.Forms.DataGridView();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.cboObjectType = new System.Windows.Forms.ComboBox();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.cboObjectName = new System.Windows.Forms.ComboBox();
            this.lblPrivs = new System.Windows.Forms.Label();
            this.clbPrivs = new System.Windows.Forms.CheckedListBox();
            this.lblColumns = new System.Windows.Forms.Label();
            this.clbColumns = new System.Windows.Forms.CheckedListBox();
            this.chkWithGrantOption = new System.Windows.Forms.CheckBox();
            this.btnGrantPriv = new System.Windows.Forms.Button();
            this.btnRevokePriv = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabUserRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabUserRole);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 451);
            this.tabControl1.TabIndex = 0;
            // 
            // tabUserRole
            // 
            this.tabUserRole.Controls.Add(this.btnLogout);
            this.tabUserRole.Controls.Add(this.lblName);
            this.tabUserRole.Controls.Add(this.lblPassword);
            this.tabUserRole.Controls.Add(this.lblChoose);
            this.tabUserRole.Controls.Add(this.btnLoad);
            this.tabUserRole.Controls.Add(this.btnUpdate);
            this.tabUserRole.Controls.Add(this.btnDelete);
            this.tabUserRole.Controls.Add(this.btnCreate);
            this.tabUserRole.Controls.Add(this.txtMatKhau);
            this.tabUserRole.Controls.Add(this.btnTogglePassword);
            this.tabUserRole.Controls.Add(this.txtTen);
            this.tabUserRole.Controls.Add(this.dgvList);
            this.tabUserRole.Controls.Add(this.cboType);
            this.tabUserRole.Location = new System.Drawing.Point(4, 22);
            this.tabUserRole.Name = "tabUserRole";
            this.tabUserRole.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserRole.Size = new System.Drawing.Size(794, 425);
            this.tabUserRole.TabIndex = 0;
            this.tabUserRole.Text = "ManagementUser/Role ";
            this.tabUserRole.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(55, 396);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 23;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 171);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(102, 13);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "Name of User/Role:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 246);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 13);
            this.lblPassword.TabIndex = 21;
            this.lblPassword.Text = "Password: ";
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.Location = new System.Drawing.Point(8, 41);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(95, 13);
            this.lblChoose.TabIndex = 20;
            this.lblChoose.Text = "Choose User/Role";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(380, 396);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 19;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(710, 396);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(602, 396);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(493, 396);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 16;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(11, 262);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(100, 20);
            this.txtMatKhau.TabIndex = 15;
            this.txtMatKhau.UseSystemPasswordChar = true;
            //
            // btnTogglePassword
            //
            this.btnTogglePassword.Location = new System.Drawing.Point(117, 260);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(40, 23);
            this.btnTogglePassword.TabIndex = 24;
            this.btnTogglePassword.Text = "\ud83d\udc41";
            this.btnTogglePassword.UseVisualStyleBackColor = true;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(12, 187);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(100, 20);
            this.txtTen.TabIndex = 14;
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(189, 6);
            this.dgvList.Name = "dgvList";
            this.dgvList.Size = new System.Drawing.Size(599, 384);
            this.dgvList.TabIndex = 13;
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "User",
            "Role"});
            this.cboType.Location = new System.Drawing.Point(11, 67);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 21);
            this.cboType.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblGranteeType);
            this.tabPage2.Controls.Add(this.cboGranteeType);
            this.tabPage2.Controls.Add(this.lblGrantee);
            this.tabPage2.Controls.Add(this.cboGrantee);
            this.tabPage2.Controls.Add(this.btnViewPrivs);
            this.tabPage2.Controls.Add(this.dgvPrivs);
            this.tabPage2.Controls.Add(this.lblObjectType);
            this.tabPage2.Controls.Add(this.cboObjectType);
            this.tabPage2.Controls.Add(this.lblObjectName);
            this.tabPage2.Controls.Add(this.cboObjectName);
            this.tabPage2.Controls.Add(this.lblPrivs);
            this.tabPage2.Controls.Add(this.clbPrivs);
            this.tabPage2.Controls.Add(this.lblColumns);
            this.tabPage2.Controls.Add(this.clbColumns);
            this.tabPage2.Controls.Add(this.chkWithGrantOption);
            this.tabPage2.Controls.Add(this.btnGrantPriv);
            this.tabPage2.Controls.Add(this.btnRevokePriv);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(794, 425);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Grant/Revoke";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblGranteeType
            // 
            this.lblGranteeType.AutoSize = true;
            this.lblGranteeType.Location = new System.Drawing.Point(10, 15);
            this.lblGranteeType.Name = "lblGranteeType";
            this.lblGranteeType.Size = new System.Drawing.Size(34, 13);
            this.lblGranteeType.TabIndex = 15;
            this.lblGranteeType.Text = "Type:";
            // 
            // cboGranteeType
            // 
            this.cboGranteeType.FormattingEnabled = true;
            this.cboGranteeType.Items.AddRange(new object[] {
            "User",
            "Role"});
            this.cboGranteeType.Location = new System.Drawing.Point(13, 35);
            this.cboGranteeType.Name = "cboGranteeType";
            this.cboGranteeType.Size = new System.Drawing.Size(70, 21);
            this.cboGranteeType.TabIndex = 16;
            this.cboGranteeType.SelectedIndexChanged += new System.EventHandler(this.cboGranteeType_SelectedIndexChanged);
            // 
            // lblGrantee
            // 
            this.lblGrantee.AutoSize = true;
            this.lblGrantee.Location = new System.Drawing.Point(90, 15);
            this.lblGrantee.Name = "lblGrantee";
            this.lblGrantee.Size = new System.Drawing.Size(139, 13);
            this.lblGrantee.TabIndex = 0;
            this.lblGrantee.Text = "Select User/Role (Grantee):";
            // 
            // cboGrantee
            // 
            this.cboGrantee.FormattingEnabled = true;
            this.cboGrantee.Location = new System.Drawing.Point(93, 35);
            this.cboGrantee.Name = "cboGrantee";
            this.cboGrantee.Size = new System.Drawing.Size(110, 21);
            this.cboGrantee.TabIndex = 1;
            // 
            // btnViewPrivs
            // 
            this.btnViewPrivs.Location = new System.Drawing.Point(210, 33);
            this.btnViewPrivs.Name = "btnViewPrivs";
            this.btnViewPrivs.Size = new System.Drawing.Size(100, 25);
            this.btnViewPrivs.TabIndex = 2;
            this.btnViewPrivs.Text = "View Privs";
            this.btnViewPrivs.UseVisualStyleBackColor = true;
            this.btnViewPrivs.Click += new System.EventHandler(this.btnViewPrivs_Click);
            // 
            // dgvPrivs
            // 
            this.dgvPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrivs.Location = new System.Drawing.Point(360, 15);
            this.dgvPrivs.Name = "dgvPrivs";
            this.dgvPrivs.Size = new System.Drawing.Size(425, 395);
            this.dgvPrivs.TabIndex = 3;
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(10, 75);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(68, 13);
            this.lblObjectType.TabIndex = 4;
            this.lblObjectType.Text = "Object Type:";
            // 
            // cboObjectType
            // 
            this.cboObjectType.FormattingEnabled = true;
            this.cboObjectType.Items.AddRange(new object[] {
            "TABLE",
            "VIEW",
            "PROCEDURE",
            "FUNCTION",
            "ROLE"});
            this.cboObjectType.Location = new System.Drawing.Point(13, 95);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(130, 21);
            this.cboObjectType.TabIndex = 5;
            this.cboObjectType.SelectedIndexChanged += new System.EventHandler(this.cboObjectType_SelectedIndexChanged);
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Location = new System.Drawing.Point(160, 75);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(72, 13);
            this.lblObjectName.TabIndex = 6;
            this.lblObjectName.Text = "Object Name:";
            // 
            // cboObjectName
            // 
            this.cboObjectName.FormattingEnabled = true;
            this.cboObjectName.Location = new System.Drawing.Point(163, 95);
            this.cboObjectName.Name = "cboObjectName";
            this.cboObjectName.Size = new System.Drawing.Size(170, 21);
            this.cboObjectName.TabIndex = 7;
            this.cboObjectName.SelectedIndexChanged += new System.EventHandler(this.cboObjectName_SelectedIndexChanged);
            // 
            // lblPrivs
            // 
            this.lblPrivs.AutoSize = true;
            this.lblPrivs.Location = new System.Drawing.Point(10, 135);
            this.lblPrivs.Name = "lblPrivs";
            this.lblPrivs.Size = new System.Drawing.Size(55, 13);
            this.lblPrivs.TabIndex = 8;
            this.lblPrivs.Text = "Privileges:";
            // 
            // clbPrivs
            // 
            this.clbPrivs.FormattingEnabled = true;
            this.clbPrivs.Location = new System.Drawing.Point(13, 155);
            this.clbPrivs.Name = "clbPrivs";
            this.clbPrivs.Size = new System.Drawing.Size(130, 94);
            this.clbPrivs.TabIndex = 9;
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(160, 135);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(144, 13);
            this.lblColumns.TabIndex = 10;
            this.lblColumns.Text = "Columns (for Select/Update):";
            // 
            // clbColumns
            // 
            this.clbColumns.FormattingEnabled = true;
            this.clbColumns.Location = new System.Drawing.Point(163, 155);
            this.clbColumns.Name = "clbColumns";
            this.clbColumns.Size = new System.Drawing.Size(170, 94);
            this.clbColumns.TabIndex = 11;
            // 
            // chkWithGrantOption
            // 
            this.chkWithGrantOption.AutoSize = true;
            this.chkWithGrantOption.Location = new System.Drawing.Point(13, 270);
            this.chkWithGrantOption.Name = "chkWithGrantOption";
            this.chkWithGrantOption.Size = new System.Drawing.Size(180, 17);
            this.chkWithGrantOption.TabIndex = 12;
            this.chkWithGrantOption.Text = "WITH GRANT/ADMIN OPTION";
            this.chkWithGrantOption.UseVisualStyleBackColor = true;
            // 
            // btnGrantPriv
            // 
            this.btnGrantPriv.BackColor = System.Drawing.Color.LightGreen;
            this.btnGrantPriv.Location = new System.Drawing.Point(13, 305);
            this.btnGrantPriv.Name = "btnGrantPriv";
            this.btnGrantPriv.Size = new System.Drawing.Size(130, 30);
            this.btnGrantPriv.TabIndex = 13;
            this.btnGrantPriv.Text = "GRANT";
            this.btnGrantPriv.UseVisualStyleBackColor = false;
            this.btnGrantPriv.Click += new System.EventHandler(this.btnGrantPriv_Click);
            // 
            // btnRevokePriv
            // 
            this.btnRevokePriv.BackColor = System.Drawing.Color.LightCoral;
            this.btnRevokePriv.Location = new System.Drawing.Point(163, 305);
            this.btnRevokePriv.Name = "btnRevokePriv";
            this.btnRevokePriv.Size = new System.Drawing.Size(130, 30);
            this.btnRevokePriv.TabIndex = 14;
            this.btnRevokePriv.Text = "REVOKE";
            this.btnRevokePriv.UseVisualStyleBackColor = false;
            this.btnRevokePriv.Click += new System.EventHandler(this.btnRevokePriv_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Oracle Security Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabUserRole.ResumeLayout(false);
            this.tabUserRole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình DataGridView 
            dgvList.AllowUserToAddRows = false; // Xoá dòng trống có dấu *
            dgvList.ReadOnly = true;            // Chỉ đọc, tránh cảnh báo lỗi khi gõ bừa vào
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.MultiSelect = false;        // Chỉ cho chọn từng dòng 1
            dgvList.RowHeadersVisible = false;  // Giấu cột mũi tên bên trái cùng cho nhỏ gọn
            dgvList.BackgroundColor = Color.White;

            // Đăng ký các sự kiện giúp tương tác UI nhạy bén
            dgvList.CellClick += DgvList_CellClick;
            cboType.SelectedIndexChanged += CboType_SelectedIndexChanged;

            // Thiết lập mặc định khi mở Form
            cboType.SelectedIndex = 0; // Tự gán User (sẽ tự động gọi sự kiện Load lưới luôn)
            LoadGranteesTab2();        // Tự động load danh sách Grantee bên Tab 2
            ApplyPlaceholderIfEmpty(txtTen);
            ApplyPlaceholderIfEmpty(txtMatKhau);
        }

        // Tự động đẩy tên User/Role lên TextBox khi nhấp vào bảng
        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvList.Rows.Count)
            {
                DataGridViewRow row = dgvList.Rows[e.RowIndex];
                string type = cboType.SelectedItem?.ToString();

                if (type == "User" && dgvList.Columns.Contains("USERNAME"))
                {
                    txtTen.Text = row.Cells["USERNAME"].Value?.ToString();
                }
                else if (type == "Role" && dgvList.Columns.Contains("ROLE"))
                {
                    txtTen.Text = row.Cells["ROLE"].Value?.ToString();
                }
                txtMatKhau.Clear(); // Luôn xoá rỗng password, buộc admin phải nhập lại khi update
                ApplyPlaceholderIfEmpty(txtMatKhau);
            }
        }

        // Tự khởi chạy chức năng LOAD khi đổi loại hiển thị (User <-> Role)
        private void CboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTen.Clear();
            txtMatKhau.Clear();
            ApplyPlaceholderIfEmpty(txtTen);
            ApplyPlaceholderIfEmpty(txtMatKhau);
            btnLoad_Click(null, null); // Tự động load dữ liệu lên bảng luôn
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất phiên làm việc này không?", "Xác nhận Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            var info = txtMatKhau.Tag as PlaceholderInfo;
            bool isPlaceholder = info != null && IsPlaceholder(txtMatKhau, info);
            _isPasswordVisible = !_isPasswordVisible;
            if (!isPlaceholder)
            {
                txtMatKhau.UseSystemPasswordChar = !_isPasswordVisible;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
        }

        // --- CÁC HÀM CŨ CỦA BẠN BÊN TAB 1 (GIỮ NGUYÊN 100%) ---
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string query = "";
            string loai = cboType.SelectedItem?.ToString();

            if (loai == "User")
            {
                query = "SELECT USERNAME, ACCOUNT_STATUS, CREATED FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N' ORDER BY CREATED DESC";
            }
            else if (loai == "Role")
            {
                query = "SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N' ORDER BY ROLE_ID DESC";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xem User hoặc Role!", "Thông báo");
                return;
            }

            DataTable dt = db.ExecuteQuery(query);
            if (dt != null)
            {
                dgvList.DataSource = dt;
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string matKhau = GetInputText(txtMatKhau);
            string loai = cboType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(loai))
            {
                MessageBox.Show("Vui lòng nhập Tên và chọn loại (User/Role)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = "";

            if (loai == "User")
            {
                if (string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cho User!", "Cảnh báo");
                    return;
                }
                query = $"CREATE USER {ten} IDENTIFIED BY {matKhau}";
            }
            else if (loai == "Role")
            {
                query = $"CREATE ROLE {ten}";
            }

            if (db.ExecuteNonQuery(query))
            {
                MessageBox.Show($"Đã tạo {loai} '{ten}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoad_Click(sender, e);
                txtTen.Clear();
                txtMatKhau.Clear();
                ApplyPlaceholderIfEmpty(txtTen);
                ApplyPlaceholderIfEmpty(txtMatKhau);
                LoadGranteesTab2(); // Cập nhật lại danh sách bên tab 2
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string loai = cboType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên User/Role cần xóa!", "Cảnh báo");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = loai == "User" ? $"DROP USER {ten} CASCADE" : $"DROP ROLE {ten}";

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa {loai} '{ten}' và toàn bộ dữ liệu liên quan không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                if (db.ExecuteNonQuery(query))
                {
                    MessageBox.Show($"Đã xóa {loai} '{ten}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoad_Click(sender, e);
                    txtTen.Clear();
                    ApplyPlaceholderIfEmpty(txtTen);
                    LoadGranteesTab2(); // Cập nhật lại danh sách bên tab 2
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ten = GetInputText(txtTen).ToUpper();
            string matKhau = GetInputText(txtMatKhau);
            string loai = cboType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập Tên User/Role và Mật khẩu mới để cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            string query = "";

            if (loai == "Users" || loai == "User")
            {
                query = $"ALTER USER {ten} IDENTIFIED BY {matKhau}";
            }
            else if (loai == "Roles" || loai == "Role")
            {
                query = $"ALTER ROLE {ten} IDENTIFIED BY {matKhau}";
            }

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn đổi mật khẩu cho {loai} '{ten}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (db.ExecuteNonQuery(query))
                {
                    MessageBox.Show($"Đã cập nhật mật khẩu cho {loai} '{ten}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoad_Click(sender, e);
                    txtMatKhau.Clear();
                    ApplyPlaceholderIfEmpty(txtMatKhau);
                }
            }
        }

        // ========================== CÁC HÀM MỚI THÊM CHO LOGIC TAB 2 ==========================

        // 1. MỚI THÊM: Sự kiện khi đổi loại Grantee (User hoặc Role)
        private void cboGranteeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboGrantee.Items.Clear();
            string type = cboGranteeType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type)) return;

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string query = type == "User" ? "SELECT USERNAME AS NAME FROM DBA_USERS WHERE ORACLE_MAINTAINED = 'N'"
                                          : "SELECT ROLE AS NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'";

            DataTable dt = db.ExecuteQuery(query);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows) cboGrantee.Items.Add(row["NAME"].ToString());
            }
            if (cboGrantee.Items.Count > 0) cboGrantee.SelectedIndex = 0;
        }

        // Nạp danh sách User/Role vào ComboBox Grantee
        private void LoadGranteesTab2()
        {
            // Tự động kích hoạt chọn "User" đầu tiên để load danh sách
            if (cboGranteeType.Items.Count > 0 && cboGranteeType.SelectedIndex == -1)
                cboGranteeType.SelectedIndex = 0;
            else
                cboGranteeType_SelectedIndexChanged(null, null);
        }

        // 2. CẬP NHẬT: Load Danh sách Quyền và Tên Đối tượng khi người dùng đổi Loại đối tượng
        private void cboObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string objType = cboObjectType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(objType)) return;

            clbPrivs.Items.Clear();
            clbColumns.Items.Clear();
            string query = "";

            if (objType == "TABLE" || objType == "VIEW")
            {
                clbPrivs.Items.AddRange(new string[] { "SELECT", "INSERT", "UPDATE", "DELETE" });
                query = $"SELECT OWNER || '.' || OBJECT_NAME AS FULL_OBJ_NAME FROM DBA_OBJECTS WHERE OBJECT_TYPE = '{objType}' AND ORACLE_MAINTAINED = 'N'";
            }
            else if (objType == "PROCEDURE" || objType == "FUNCTION")
            {
                clbPrivs.Items.AddRange(new string[] { "EXECUTE" });
                query = $"SELECT OWNER || '.' || OBJECT_NAME AS FULL_OBJ_NAME FROM DBA_OBJECTS WHERE OBJECT_TYPE = '{objType}' AND ORACLE_MAINTAINED = 'N'";
            }
            else if (objType == "ROLE") // MỚI THÊM: Nếu đối tượng là ROLE
            {
                // Không thêm đặc quyền gì vì Role gán nguyên cục
                query = $"SELECT ROLE AS FULL_OBJ_NAME FROM DBA_ROLES WHERE ORACLE_MAINTAINED = 'N'";
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);
            DataTable dt = db.ExecuteQuery(query);

            cboObjectName.Items.Clear();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows) cboObjectName.Items.Add(row["FULL_OBJ_NAME"].ToString());
            }
        }

        // 3. Load Danh sách cột khi chọn Table/View (Giữ Nguyên)
        private void cboObjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string objType = cboObjectType.SelectedItem?.ToString();
            string fullObjName = cboObjectName.SelectedItem?.ToString();

            clbColumns.Items.Clear();

            if (string.IsNullOrEmpty(fullObjName) || (objType != "TABLE" && objType != "VIEW")) return;

            // Tách Chuỗi OWNER.TABLE_NAME ra để lấy riêng TABLE_NAME truyền vào query
            string[] parts = fullObjName.Split('.');
            if (parts.Length != 2) return;
            string owner = parts[0];
            string objName = parts[1];

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string query = $"SELECT COLUMN_NAME FROM DBA_TAB_COLS WHERE TABLE_NAME = '{objName}' AND OWNER = '{owner}'";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows) clbColumns.Items.Add(row["COLUMN_NAME"].ToString());
            }
        }

        // 4. CẬP NHẬT: Xem quyền đang có của User/Role (Thêm hiển thị Role)
        private void btnViewPrivs_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(grantee))
            {
                MessageBox.Show("Vui lòng chọn Grantee ở ô trên cùng!", "Thông báo");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            string queryObjPrivs = $"SELECT OWNER || '.' || TABLE_NAME AS OBJECT_NAME, PRIVILEGE, GRANTABLE FROM DBA_TAB_PRIVS WHERE GRANTEE = '{grantee}'";
            string queryColPrivs = $"SELECT OWNER || '.' || TABLE_NAME || '.' || COLUMN_NAME AS OBJECT_NAME, PRIVILEGE, GRANTABLE FROM DBA_COL_PRIVS WHERE GRANTEE = '{grantee}'";
            // MỚI THÊM: Truy vấn các Role đã được gán cho User
            string queryRolePrivs = $"SELECT GRANTED_ROLE AS OBJECT_NAME, 'ROLE' AS PRIVILEGE, ADMIN_OPTION AS GRANTABLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = '{grantee}'";

            // Nối 3 bảng bằng UNION ALL
            DataTable dt = db.ExecuteQuery($"{queryObjPrivs} UNION ALL {queryColPrivs} UNION ALL {queryRolePrivs}");

            if (dt != null)
            {
                dgvPrivs.DataSource = dt;
                dgvPrivs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // 5. CẬP NHẬT: Cấp Quyền (Grant) và Cấp Role
        private void btnGrantPriv_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.SelectedItem?.ToString();
            string objType = cboObjectType.SelectedItem?.ToString();
            string fullObjName = cboObjectName.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(fullObjName))
            {
                MessageBox.Show("Vui lòng chọn Grantee, Object và ít nhất 1 Privilege!", "Cảnh báo");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            // Xử lý cờ tuỳ chọn cấp tiếp
            string grantOption = chkWithGrantOption.Checked ? (objType == "ROLE" ? " WITH ADMIN OPTION" : " WITH GRANT OPTION") : "";

            // MỚI THÊM: Nếu là gán Role
            if (objType == "ROLE")
            {
                if (db.ExecuteNonQuery($"GRANT {fullObjName} TO {grantee}{grantOption}"))
                {
                    MessageBox.Show("Cấp role thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnViewPrivs_Click(sender, e);
                }
                return;
            }

            if (clbPrivs.CheckedItems.Count == 0) return;
            bool success = true;

            foreach (var item in clbPrivs.CheckedItems)
            {
                string priv = item.ToString();
                string query = "";

                // Phân quyền mức cột cho SELECT và UPDATE
                if ((priv == "SELECT" || priv == "UPDATE") && clbColumns.CheckedItems.Count > 0)
                {
                    string cols = "";
                    foreach (var col in clbColumns.CheckedItems) cols += col.ToString() + ",";
                    cols = cols.TrimEnd(','); // Cắt dấu phẩy thừa ở cuối
                    query = $"GRANT {priv} ({cols}) ON {fullObjName} TO {grantee}{grantOption}";
                }
                else
                {
                    query = $"GRANT {priv} ON {fullObjName} TO {grantee}{grantOption}";
                }

                if (!db.ExecuteNonQuery(query)) success = false;
            }

            if (success)
            {
                MessageBox.Show("Cấp quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnViewPrivs_Click(sender, e); // Load lại danh sách quyền bên lưới
            }
        }

        // 6. CẬP NHẬT: Thu hồi Quyền (Revoke) và Thu hồi Role
        private void btnRevokePriv_Click(object sender, EventArgs e)
        {
            string grantee = cboGrantee.SelectedItem?.ToString();
            string objType = cboObjectType.SelectedItem?.ToString();
            string fullObjName = cboObjectName.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(fullObjName))
            {
                MessageBox.Show("Vui lòng chọn Grantee, Object và Privilege cần thu hồi!", "Cảnh báo");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(_host, _port, _serviceName, _username, _password, false);

            // MỚI THÊM: Nếu là thu hồi Role
            if (objType == "ROLE")
            {
                if (db.ExecuteNonQuery($"REVOKE {fullObjName} FROM {grantee}"))
                {
                    MessageBox.Show("Thu hồi role thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnViewPrivs_Click(sender, e);
                }
                return;
            }

            if (clbPrivs.CheckedItems.Count == 0) return;
            bool success = true;

            foreach (var item in clbPrivs.CheckedItems)
            {
                string priv = item.ToString();
                // Thu hồi quyền thì không có mức cột, thu hồi là thu hồi nguyên bảng
                string query = $"REVOKE {priv} ON {fullObjName} FROM {grantee}";

                if (!db.ExecuteNonQuery(query)) success = false;
            }

            if (success)
            {
                MessageBox.Show("Thu hồi quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnViewPrivs_Click(sender, e);
            }
        }

        private void InitPlaceholders()
        {
            SetupPlaceholder(txtTen, "Enter user/role name", false);
            SetupPlaceholder(txtMatKhau, "Enter password", true);
        }

        private void SetupPlaceholder(TextBox box, string placeholder, bool isPassword)
        {
            box.Tag = new PlaceholderInfo { Text = placeholder, IsPassword = isPassword };
            box.ForeColor = Color.Gray;
            box.Text = placeholder;
            if (isPassword) box.UseSystemPasswordChar = false;
            box.Enter += Placeholder_Enter;
            box.Leave += Placeholder_Leave;
        }

        private void Placeholder_Enter(object sender, EventArgs e)
        {
            if (!(sender is TextBox box) || !(box.Tag is PlaceholderInfo info)) return;
            if (IsPlaceholder(box, info))
            {
                box.Text = string.Empty;
                box.ForeColor = Color.Black;
                if (info.IsPassword) box.UseSystemPasswordChar = !_isPasswordVisible;
            }
        }

        private void Placeholder_Leave(object sender, EventArgs e)
        {
            if (!(sender is TextBox box) || !(box.Tag is PlaceholderInfo info)) return;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = info.Text;
                box.ForeColor = Color.Gray;
                if (info.IsPassword) box.UseSystemPasswordChar = false;
            }
        }

        private bool IsPlaceholder(TextBox box, PlaceholderInfo info)
        {
            return box.ForeColor == Color.Gray && box.Text == info.Text;
        }

        private string GetInputText(TextBox box)
        {
            var info = box.Tag as PlaceholderInfo;
            if (info != null && IsPlaceholder(box, info)) return string.Empty;
            return box.Text.Trim();
        }

        private void ApplyPlaceholderIfEmpty(TextBox box)
        {
            var info = box.Tag as PlaceholderInfo;
            if (info != null && string.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = info.Text;
                box.ForeColor = Color.Gray;
                if (info.IsPassword) box.UseSystemPasswordChar = false;
            }
        }

        private class PlaceholderInfo
        {
            public string Text { get; set; }
            public bool IsPassword { get; set; }
        }
    }
}

