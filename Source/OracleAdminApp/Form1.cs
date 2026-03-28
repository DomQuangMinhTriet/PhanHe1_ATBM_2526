//using System;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

//namespace OracleSecurityAdmin
//{
//    public partial class Form1 : Form
//    {
//        // Biến lưu thông tin kết nối từ LoginForm truyền sang
//        private string _host, _port, _serviceName, _username, _password;

//        private void InitializeComponent()
//        {
//            this.tabMain = new System.Windows.Forms.TabControl();
//            this.tabMng = new System.Windows.Forms.TabPage();
//            this.tabGrant = new System.Windows.Forms.TabPage();
//            this.ucMng = new OracleSecurityAdmin.uc_Management();
//            this.ucGrant = new OracleSecurityAdmin.uc_Grant();

//            this.tabMain.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // Cấu hình TabControl
//            this.tabMain.Controls.Add(this.tabMng);
//            this.tabMain.Controls.Add(this.tabGrant);
//            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tabMain.Name = "tabMain";

//            // Cấu hình Tab 1 (Management)
//            this.tabMng.Controls.Add(this.ucMng);
//            this.tabMng.Text = "Quản lý User/Role";
//            this.ucMng.Dock = System.Windows.Forms.DockStyle.Fill;

//            // Cấu hình Tab 2 (Grant - Cấp quyền)
//            this.tabGrant.Controls.Add(this.ucGrant);
//            this.tabGrant.Text = "Cấp quyền (3a, 3b, 3c)";
//            this.ucGrant.Dock = System.Windows.Forms.DockStyle.Fill;

//            // Form1
//            this.ClientSize = new System.Drawing.Size(850, 500); // Cho Form to ra tí nhìn UX cho sướng
//            this.Controls.Add(this.tabMain);
//            this.Name = "Form1";
//            this.tabMain.ResumeLayout(false);
//            this.ResumeLayout(false);
//        }

//        // Khai báo thêm 2 cái TabPage nữa ở ngoài nhé
//        private TabPage tabMng;
//        private TabPage tabGrant;

//        public Form1()
//        {
//            InitializeComponent();
//        }

//        // Constructor nhận tham số từ LoginForm
//        public Form1(string host, string port, string serviceName, string username, string password)
//        {
//            InitializeComponent();
//            _host = host;
//            _port = port;
//            _serviceName = serviceName;
//            _username = username;
//            _password = password;

//            if (ucMng != null)
//            {
//                ucMng.SetConfig(host, port, serviceName, username, password);
//            }
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {
//            // Truyền cấu hình xuống UserControl Quản lý (Tab 1)
//            if (ucMng != null)
//            {
//                ucMng.SetConfig(_host, _port, _serviceName, _username, _password);
//            }

//            //// Truyền cấu hình xuống UserControl Cấp quyền (Tab 2)
//            //if (ucGrant != null)
//            //{
//            //    ucGrant.SetConfig(_host, _port, _serviceName, _username, _password);
//            //}
//        }
//    }
//}

using System;
using System.Windows.Forms;
using System.Drawing;

namespace OracleSecurityAdmin
{
    public partial class Form1 : Form
    {
        // Biến lưu thông tin kết nối từ LoginForm truyền sang
        private string _host, _port, _serviceName, _username, _password;

        // Khai báo các thành phần giao diện
        private TabControl tabMain;
        private TabPage tabMng;
        private TabPage tabGrant;
        private uc_Management ucMng;
        private uc_Grant ucGrant;

        public Form1()
        {
            InitializeComponent();
        }

        // Constructor nhận tham số từ LoginForm và truyền ngay xuống các Tab
        public Form1(string host, string port, string serviceName, string username, string password)
        {
            InitializeComponent();

            _host = host;
            _port = port;
            _serviceName = serviceName;
            _username = username;
            _password = password;

            // Truyền cấu hình xuống các UserControl ngay khi khởi tạo
            if (ucMng != null)
                ucMng.SetConfig(host, port, serviceName, username, password);

            if (ucGrant != null)
                ucGrant.SetConfig(host, port, serviceName, username, password);
        }

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMng = new System.Windows.Forms.TabPage();
            this.tabGrant = new System.Windows.Forms.TabPage();
            this.ucMng = new OracleSecurityAdmin.uc_Management();
            this.ucGrant = new OracleSecurityAdmin.uc_Grant();

            this.tabMain.SuspendLayout();
            this.tabMng.SuspendLayout();
            this.tabGrant.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabMain (Bộ khung chứa các Tab)
            // 
            this.tabMain.Controls.Add(this.tabMng);
            this.tabMain.Controls.Add(this.tabGrant);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(850, 550);
            this.tabMain.TabIndex = 0;

            // 
            // tabMng (Tab Quản lý User/Role)
            // 
            this.tabMng.Controls.Add(this.ucMng);
            this.tabMng.Location = new System.Drawing.Point(4, 22);
            this.tabMng.Name = "tabMng";
            this.tabMng.Padding = new System.Windows.Forms.Padding(3);
            this.tabMng.Size = new System.Drawing.Size(842, 524);
            this.tabMng.TabIndex = 0;
            this.tabMng.Text = "1. Quản lý User/Role";
            this.tabMng.UseVisualStyleBackColor = true;

            // 
            // ucMng (Nội dung bên trong Tab 1)
            // 
            this.ucMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMng.Location = new System.Drawing.Point(3, 3);
            this.ucMng.Name = "ucMng";
            this.ucMng.TabIndex = 0;

            // 
            // tabGrant (Tab Cấp quyền)
            // 
            this.tabGrant.Controls.Add(this.ucGrant);
            this.tabGrant.Location = new System.Drawing.Point(4, 22);
            this.tabGrant.Name = "tabGrant";
            this.tabGrant.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrant.Size = new System.Drawing.Size(842, 524);
            this.tabGrant.TabIndex = 1;
            this.tabGrant.Text = "2. Cấp quyền (Grant)";
            this.tabGrant.UseVisualStyleBackColor = true;

            // 
            // ucGrant (Nội dung bên trong Tab 2)
            // 
            this.ucGrant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrant.Location = new System.Drawing.Point(3, 3);
            this.ucGrant.Name = "ucGrant";
            this.ucGrant.TabIndex = 0;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.tabMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản trị Bảo mật Oracle - Admin App";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.tabMain.ResumeLayout(false);
            this.tabMng.ResumeLayout(false);
            this.tabGrant.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Đảm bảo khi Form hiện lên, các Tab đều đã nhận được cấu hình
            if (ucMng != null)
                ucMng.SetConfig(_host, _port, _serviceName, _username, _password);

            if (ucGrant != null)
                ucGrant.SetConfig(_host, _port, _serviceName, _username, _password);
        }
    }
}