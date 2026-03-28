//namespace OracleSecurityAdmin
//{
//    partial class uc_Revoke
//    {
//        private System.ComponentModel.IContainer components = null;
//        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

//        private void InitializeComponent()
//        {
//            this.dgvPrivs = new System.Windows.Forms.DataGridView();
//            this.pnlTop = new System.Windows.Forms.Panel();
//            this.btnFilter = new System.Windows.Forms.Button();
//            this.txtSearch = new System.Windows.Forms.TextBox();
//            this.lblSearch = new System.Windows.Forms.Label();
//            this.cboRevokeTarget = new System.Windows.Forms.ComboBox();
//            this.lblTarget = new System.Windows.Forms.Label();
//            this.btnRevoke = new System.Windows.Forms.Button();
//            this.lblTitle = new System.Windows.Forms.Label();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).BeginInit();
//            this.pnlTop.SuspendLayout();
//            this.SuspendLayout();

//            // pnlTop
//            this.pnlTop.Controls.Add(this.btnFilter);
//            this.pnlTop.Controls.Add(this.txtSearch);
//            this.pnlTop.Controls.Add(this.lblSearch);
//            this.pnlTop.Controls.Add(this.cboRevokeTarget);
//            this.pnlTop.Controls.Add(this.lblTarget);
//            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlTop.Height = 80;

//            // lblTarget & cboRevokeTarget (Lọc theo User/Role)
//            this.lblTarget.Text = "Lọc theo User/Role:";
//            this.lblTarget.Location = new System.Drawing.Point(15, 15);
//            this.cboRevokeTarget.Location = new System.Drawing.Point(15, 35);
//            this.cboRevokeTarget.Size = new System.Drawing.Size(200, 25);
//            this.cboRevokeTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

//            // lblSearch & txtSearch (Tìm kiếm nhanh)
//            this.lblSearch.Text = "Tìm kiếm nhanh (Tên bảng/Quyền):";
//            this.lblSearch.Location = new System.Drawing.Point(240, 15);
//            this.txtSearch.Location = new System.Drawing.Point(240, 35);
//            this.txtSearch.Size = new System.Drawing.Size(250, 25);
//            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

//            // btnFilter
//            this.btnFilter.Text = "LỌC DỮ LIỆU";
//            this.btnFilter.Location = new System.Drawing.Point(510, 32);
//            this.btnFilter.Size = new System.Drawing.Size(120, 30);
//            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

//            // dgvPrivs (Lưới hiển thị)
//            this.dgvPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvPrivs.BackgroundColor = System.Drawing.Color.WhiteSmoke;
//            this.dgvPrivs.BorderStyle = System.Windows.Forms.BorderStyle.None;

//            // btnRevoke (Nút hành động)
//            this.btnRevoke.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.btnRevoke.Height = 50;
//            this.btnRevoke.Text = "THU HỒI QUYỀN ĐANG CHỌN (REVOKE)";
//            this.btnRevoke.BackColor = System.Drawing.Color.MistyRose;
//            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
//            this.btnRevoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);

//            this.Controls.Add(this.dgvPrivs);
//            this.Controls.Add(this.pnlTop);
//            this.Controls.Add(this.btnRevoke);
//            this.Size = new System.Drawing.Size(800, 600);
//            this.Load += new System.EventHandler(this.uc_Revoke_Load);
//            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).EndInit();
//            this.pnlTop.ResumeLayout(false);
//            this.pnlTop.PerformLayout();
//            this.ResumeLayout(false);
//        }

//        private System.Windows.Forms.DataGridView dgvPrivs;
//        private System.Windows.Forms.Panel pnlTop;
//        private System.Windows.Forms.ComboBox cboRevokeTarget;
//        private System.Windows.Forms.TextBox txtSearch;
//        private System.Windows.Forms.Button btnRevoke, btnFilter;
//        private System.Windows.Forms.Label lblTarget, lblSearch, lblTitle;
//    }
//}

namespace OracleSecurityAdmin
{
    partial class uc_Revoke
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            // Cấu hình DataGridView để hiện thị nhiều cột mượt mà
            this.dgvPrivs = new System.Windows.Forms.DataGridView();
            this.dgvPrivs.AllowUserToOrderColumns = true;
            this.dgvPrivs.BackgroundColor = System.Drawing.Color.White;
            this.dgvPrivs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPrivs.ColumnHeadersHeight = 30;
            this.dgvPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrivs.GridColor = System.Drawing.Color.LightGray;
            this.dgvPrivs.Location = new System.Drawing.Point(0, 85);
            this.dgvPrivs.Name = "dgvPrivs";
            this.dgvPrivs.RowHeadersVisible = false; // Tắt cột đầu dòng cho sạch
            this.dgvPrivs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrivs.Size = new System.Drawing.Size(800, 465);

           
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cboRevokeType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboRevokeTarget = new System.Windows.Forms.ComboBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.btnRevoke = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // pnlTop (Thanh công cụ lọc)
            this.pnlTop.Controls.Add(this.cboRevokeType);
            this.pnlTop.Controls.Add(this.lblType);
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Controls.Add(this.cboRevokeTarget);
            this.pnlTop.Controls.Add(this.lblTarget);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 85;

            // Lọc Loại (User/Role)
            this.lblType.Text = "1. Chọn Loại:";
            this.lblType.Location = new System.Drawing.Point(15, 10);
            this.cboRevokeType.Items.AddRange(new object[] { "User", "Role" });
            this.cboRevokeType.Location = new System.Drawing.Point(15, 30);
            this.cboRevokeType.Size = new System.Drawing.Size(100, 25);
            this.cboRevokeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRevokeType.SelectedIndexChanged += new System.EventHandler(this.cboRevokeType_SelectedIndexChanged);

            // Lọc theo Tên cụ thể (Danh sách này sẽ đổi theo Loại)
            this.lblTarget.Text = "2. Chọn Người nhận:";
            this.lblTarget.Location = new System.Drawing.Point(135, 10);
            this.cboRevokeTarget.Location = new System.Drawing.Point(135, 30);
            this.cboRevokeTarget.Size = new System.Drawing.Size(180, 25);
            this.cboRevokeTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRevokeTarget.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);

            // Tìm kiếm nhanh (Live Search)
            this.lblSearch.Text = "3. Tìm nhanh (Bảng/Quyền):";
            this.lblSearch.Location = new System.Drawing.Point(340, 10);
            this.txtSearch.Location = new System.Drawing.Point(340, 30);
            this.txtSearch.Size = new System.Drawing.Size(250, 25);
            this.txtSearch.TextChanged += new System.EventHandler(this.Filter_Changed);

            // dgvPrivs
            this.dgvPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrivs.BackgroundColor = System.Drawing.Color.White;
            this.dgvPrivs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // btnRevoke
            this.btnRevoke.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRevoke.Height = 50;
            this.btnRevoke.BackColor = System.Drawing.Color.MistyRose;
            this.btnRevoke.Text = "THU HỒI QUYỀN (REVOKE)";
            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRevoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);

            this.Controls.Add(this.dgvPrivs);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnRevoke);
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.uc_Revoke_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivs)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvPrivs;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ComboBox cboRevokeType, cboRevokeTarget;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Label lblType, lblTarget, lblSearch;
    }
}