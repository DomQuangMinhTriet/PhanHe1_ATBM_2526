using System;
using System.Windows.Forms;
using System.Drawing;

namespace OracleSecurityAdmin
{
    public partial class Form1 : Form
    {
        private string _host, _port, _serviceName, _username, _password;

        private TabControl tabMain;
        private TabPage tabMng;
        private TabPage tabGrant;
        private uc_Management ucMng;
        private uc_Grant ucGrant;
        private TabPage tabRevoke;
        private uc_Revoke ucRevoke;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string host, string port, string serviceName, string username, string password)
        {
            InitializeComponent();

            _host = host;
            _port = port;
            _serviceName = serviceName;
            _username = username;
            _password = password;

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

            this.tabMain.Controls.Add(this.tabMng);
            this.tabMain.Controls.Add(this.tabGrant);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            this.tabMain.Size = new System.Drawing.Size(850, 550);
            this.tabMain.TabIndex = 0;

            this.tabMng.Controls.Add(this.ucMng);
            this.tabMng.Location = new System.Drawing.Point(4, 22);
            this.tabMng.Name = "tabMng";
            this.tabMng.Padding = new System.Windows.Forms.Padding(3);
            this.tabMng.Size = new System.Drawing.Size(842, 524);
            this.tabMng.TabIndex = 0;
            this.tabMng.Text = "1. Quản lý User/Role";
            this.tabMng.UseVisualStyleBackColor = true;

            this.ucMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMng.Location = new System.Drawing.Point(3, 3);
            this.ucMng.Name = "ucMng";
            this.ucMng.TabIndex = 0;

            this.tabGrant.Controls.Add(this.ucGrant);
            this.tabGrant.Location = new System.Drawing.Point(4, 22);
            this.tabGrant.Name = "tabGrant";
            this.tabGrant.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrant.Size = new System.Drawing.Size(842, 524);
            this.tabGrant.TabIndex = 1;
            this.tabGrant.Text = "2. Cấp quyền (Grant)";
            this.tabGrant.UseVisualStyleBackColor = true;

            this.ucGrant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrant.Location = new System.Drawing.Point(3, 3);
            this.ucGrant.Name = "ucGrant";
            this.ucGrant.TabIndex = 0;

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

            this.tabRevoke = new System.Windows.Forms.TabPage();
            this.ucRevoke = new OracleSecurityAdmin.uc_Revoke();

            this.tabRevoke.Text = "3. Xem & Thu hồi quyền";
            this.tabRevoke.Controls.Add(this.ucRevoke);
            this.ucRevoke.Dock = DockStyle.Fill;
            this.tabMain.Controls.Add(this.tabRevoke);
        }


        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabRevoke)
            {
                Application.DoEvents();

                if (ucRevoke != null)
                {
                    ucRevoke.RefreshData();
                    ucRevoke.Update();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (ucMng != null)
                ucMng.SetConfig(_host, _port, _serviceName, _username, _password);

            if (ucGrant != null)
                ucGrant.SetConfig(_host, _port, _serviceName, _username, _password);

            if (ucRevoke != null)
                ucRevoke.SetConfig(_host, _port, _serviceName, _username, _password);
        }
    }
}