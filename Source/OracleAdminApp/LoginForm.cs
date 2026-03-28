using OracleAdminApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    public partial class LoginForm : Form
    {
        private Label lblHost;
        private Label lblPort;
        private Label lblUsername;
        private Label lblService;
        private TextBox txtHost;
        private TextBox txtPort;
        private TextBox txtServiceName;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnTogglePassword;
        private Button btnLogin;
        private Label lblPassword;
        private bool _isPasswordVisible;

        public LoginForm()
        {
            InitializeComponent();
            InitPlaceholders();

            txtHost.Text = "localhost";
            txtPort.Text = "11521";
            txtServiceName.Text = "xepdb1";
            txtUsername.Text = "ADMIN_ATBM";
            txtPassword.Text = "123";

            txtHost.ForeColor = Color.Black;
            txtPort.ForeColor = Color.Black;
            txtServiceName.ForeColor = Color.Black;
            txtUsername.ForeColor = Color.Black;
            txtPassword.ForeColor = Color.Black;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string host = GetInputText(txtHost);
            string port = GetInputText(txtPort);
            string serviceName = GetInputText(txtServiceName);
            string username = GetInputText(txtUsername);
            string password = GetInputText(txtPassword);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(host))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin kết nối!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper db = new DatabaseHelper();
            db.BuildConnectionString(host, port, serviceName, username, password, false);

            if (db.Connect())
            {
                db.Disconnect();

                Form1 mainForm = new Form1(host, port, serviceName, username, password);
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
        }

        private void InitializeComponent()
        {
            this.lblHost = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(289, 112);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 13);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(289, 151);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(289, 227);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username:";
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Location = new System.Drawing.Point(289, 188);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(77, 13);
            this.lblService.TabIndex = 2;
            this.lblService.Text = "Service Name:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(289, 266);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(403, 109);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 20);
            this.txtHost.TabIndex = 5;
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(403, 148);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 6;
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(403, 185);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(100, 20);
            this.txtServiceName.TabIndex = 7;
            this.txtServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(403, 224);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 8;
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(403, 263);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            //
            // btnTogglePassword
            //
            this.btnTogglePassword.Location = new System.Drawing.Point(509, 261);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(40, 23);
            this.btnTogglePassword.TabIndex = 11;
            this.btnTogglePassword.Text = "👁";
            this.btnTogglePassword.UseVisualStyleBackColor = true;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            this.btnTogglePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(348, 326);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnTogglePassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblService);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblHost);
            this.Name = "LoginForm";
            this.AcceptButton = this.btnLogin;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            var info = txtPassword.Tag as PlaceholderInfo;
            bool isPlaceholder = info != null && IsPlaceholder(txtPassword, info);
            _isPasswordVisible = !_isPasswordVisible;
            if (!isPlaceholder)
            {
                txtPassword.UseSystemPasswordChar = !_isPasswordVisible;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void InitPlaceholders()
        {
            SetupPlaceholder(txtHost, "Enter host", false);
            SetupPlaceholder(txtPort, "Enter port", false);
            SetupPlaceholder(txtServiceName, "Enter service name", false);
            SetupPlaceholder(txtUsername, "Enter username", false);
            SetupPlaceholder(txtPassword, "Enter password", true);
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

        private class PlaceholderInfo
        {
            public string Text { get; set; }
            public bool IsPassword { get; set; }
        }
    }
}