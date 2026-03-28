//namespace OracleSecurityAdmin
//{
//    partial class uc_Grant
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
//            this.pnlGrantee = new System.Windows.Forms.Panel();
//            this.cboGrantee = new System.Windows.Forms.ComboBox();
//            this.lblGrantee = new System.Windows.Forms.Label();
//            this.lblGranteeType = new System.Windows.Forms.Label();
//            this.cboGranteeType = new System.Windows.Forms.ComboBox();
//            this.pnlObject = new System.Windows.Forms.Panel();
//            this.lblObjectName = new System.Windows.Forms.Label();
//            this.cboObjectName = new System.Windows.Forms.ComboBox();
//            this.cboObjectType = new System.Windows.Forms.ComboBox();
//            this.lblObjectType = new System.Windows.Forms.Label();
//            this.lblPrivs = new System.Windows.Forms.Label();
//            this.clbPrivs = new System.Windows.Forms.CheckedListBox();
//            this.lblColumns = new System.Windows.Forms.Label();
//            this.clbColumns = new System.Windows.Forms.CheckedListBox();
//            this.chkWithGrantOption = new System.Windows.Forms.CheckBox();
//            this.btnGrant = new System.Windows.Forms.Button();
//            this.tableLayoutPanel1.SuspendLayout();
//            this.pnlGrantee.SuspendLayout();
//            this.pnlObject.SuspendLayout();
//            this.SuspendLayout();
//            this.clbPrivs = new System.Windows.Forms.CheckedListBox();
//            this.clbPrivs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPrivs_ItemCheck);
//            // 
//            // tableLayoutPanel1
//            // 
//            this.tableLayoutPanel1.ColumnCount = 2;
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.Controls.Add(this.pnlGrantee, 0, 0);
//            this.tableLayoutPanel1.Controls.Add(this.pnlObject, 1, 0);
//            this.tableLayoutPanel1.Controls.Add(this.lblPrivs, 0, 1);
//            this.tableLayoutPanel1.Controls.Add(this.clbPrivs, 0, 2);
//            this.tableLayoutPanel1.Controls.Add(this.lblColumns, 1, 1);
//            this.tableLayoutPanel1.Controls.Add(this.clbColumns, 1, 2);
//            this.tableLayoutPanel1.Controls.Add(this.chkWithGrantOption, 0, 3);
//            this.tableLayoutPanel1.Controls.Add(this.btnGrant, 1, 3);
//            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
//            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
//            this.tableLayoutPanel1.RowCount = 4;
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
//            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 580);
//            this.tableLayoutPanel1.TabIndex = 0;
//            // 
//            // pnlGrantee
//            // 
//            this.pnlGrantee.Controls.Add(this.cboGrantee);
//            this.pnlGrantee.Controls.Add(this.lblGrantee);
//            this.pnlGrantee.Controls.Add(this.lblGranteeType);
//            this.pnlGrantee.Controls.Add(this.cboGranteeType);
//            this.pnlGrantee.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlGrantee.Location = new System.Drawing.Point(3, 3);
//            this.pnlGrantee.Name = "pnlGrantee";
//            this.pnlGrantee.Size = new System.Drawing.Size(384, 104);
//            this.pnlGrantee.TabIndex = 0;
//            // 
//            // cboGrantee
//            // 
//            this.cboGrantee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cboGrantee.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cboGrantee.FormattingEnabled = true;
//            this.cboGrantee.Location = new System.Drawing.Point(12, 65);
//            this.cboGrantee.Name = "cboGrantee";
//            this.cboGrantee.Size = new System.Drawing.Size(300, 28);
//            this.cboGrantee.TabIndex = 3;
//            // 
//            // lblGrantee
//            // 
//            this.lblGrantee.AutoSize = true;
//            this.lblGrantee.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblGrantee.Location = new System.Drawing.Point(12, 45);
//            this.lblGrantee.Name = "lblGrantee";
//            this.lblGrantee.Size = new System.Drawing.Size(121, 17);
//            this.lblGrantee.TabIndex = 2;
//            this.lblGrantee.Text = "Người nhận quyền:";
//            // 
//            // lblGranteeType
//            // 
//            this.lblGranteeType.AutoSize = true;
//            this.lblGranteeType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblGranteeType.Location = new System.Drawing.Point(12, 0);
//            this.lblGranteeType.Name = "lblGranteeType";
//            this.lblGranteeType.Size = new System.Drawing.Size(130, 17);
//            this.lblGranteeType.TabIndex = 0;
//            this.lblGranteeType.Text = "Chọn loại (User/Role):";
//            // 
//            // cboGranteeType
//            // 
//            this.cboGranteeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cboGranteeType.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cboGranteeType.FormattingEnabled = true;
//            this.cboGranteeType.Items.AddRange(new object[] {
//            "User",
//            "Role"});
//            this.cboGranteeType.Location = new System.Drawing.Point(12, 17);
//            this.cboGranteeType.Name = "cboGranteeType";
//            this.cboGranteeType.Size = new System.Drawing.Size(121, 28);
//            this.cboGranteeType.TabIndex = 1;
//            this.cboGranteeType.SelectedIndexChanged += new System.EventHandler(this.cboGranteeType_SelectedIndexChanged);
//            // 
//            // pnlObject
//            // 
//            this.pnlObject.Controls.Add(this.lblObjectName);
//            this.pnlObject.Controls.Add(this.cboObjectName);
//            this.pnlObject.Controls.Add(this.cboObjectType);
//            this.pnlObject.Controls.Add(this.lblObjectType);
//            this.pnlObject.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlObject.Location = new System.Drawing.Point(393, 3);
//            this.pnlObject.Name = "pnlObject";
//            this.pnlObject.Size = new System.Drawing.Size(384, 104);
//            this.pnlObject.TabIndex = 1;
//            // 
//            // lblObjectName
//            // 
//            this.lblObjectName.AutoSize = true;
//            this.lblObjectName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblObjectName.Location = new System.Drawing.Point(12, 45);
//            this.lblObjectName.Name = "lblObjectName";
//            this.lblObjectName.Size = new System.Drawing.Size(138, 17);
//            this.lblObjectName.TabIndex = 2;
//            this.lblObjectName.Text = "Chọn đối tượng cụ thể:";
//            // 
//            // cboObjectName
//            // 
//            this.cboObjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cboObjectName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cboObjectName.FormattingEnabled = true;
//            this.cboObjectName.Location = new System.Drawing.Point(12, 65);
//            this.cboObjectName.Name = "cboObjectName";
//            this.cboObjectName.Size = new System.Drawing.Size(300, 28);
//            this.cboObjectName.TabIndex = 3;
//            this.cboObjectName.SelectedIndexChanged += new System.EventHandler(this.cboObjectName_SelectedIndexChanged);
//            // 
//            // cboObjectType
//            // 
//            this.cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cboObjectType.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.cboObjectType.FormattingEnabled = true;
//            this.cboObjectType.Items.AddRange(new object[] {
//            "TABLE",
//            "VIEW",
//            "PROCEDURE",
//            "FUNCTION",
//            "ROLE"});
//            this.cboObjectType.Location = new System.Drawing.Point(12, 17);
//            this.cboObjectType.Name = "cboObjectType";
//            this.cboObjectType.Size = new System.Drawing.Size(200, 28);
//            this.cboObjectType.TabIndex = 1;
//            this.cboObjectType.SelectedIndexChanged += new System.EventHandler(this.cboObjectType_SelectedIndexChanged);
//            // 
//            // lblObjectType
//            // 
//            this.lblObjectType.AutoSize = true;
//            this.lblObjectType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblObjectType.Location = new System.Drawing.Point(12, 0);
//            this.lblObjectType.Name = "lblObjectType";
//            this.lblObjectType.Size = new System.Drawing.Size(123, 17);
//            this.lblObjectType.TabIndex = 0;
//            this.lblObjectType.Text = "Chọn loại đối tượng:";
//            // 
//            // lblPrivs
//            // 
//            this.lblPrivs.AutoSize = true;
//            this.lblPrivs.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.lblPrivs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblPrivs.ForeColor = System.Drawing.Color.Blue;
//            this.lblPrivs.Location = new System.Drawing.Point(3, 123);
//            this.lblPrivs.Name = "lblPrivs";
//            this.lblPrivs.Size = new System.Drawing.Size(384, 17);
//            this.lblPrivs.TabIndex = 2;
//            this.lblPrivs.Text = "3c. Danh sách Quyền:";
//            // 
//            // clbPrivs
//            // 
//            this.clbPrivs.CheckOnClick = true;
//            this.clbPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.clbPrivs.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.clbPrivs.FormattingEnabled = true;
//            this.clbPrivs.Location = new System.Drawing.Point(10, 150);
//            this.clbPrivs.Margin = new System.Windows.Forms.Padding(10);
//            this.clbPrivs.Name = "clbPrivs";
//            this.clbPrivs.Size = new System.Drawing.Size(370, 360);
//            this.clbPrivs.TabIndex = 3;
//            // 
//            // lblColumns
//            // 
//            this.lblColumns.AutoSize = true;
//            this.lblColumns.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.lblColumns.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblColumns.ForeColor = System.Drawing.Color.Blue;
//            this.lblColumns.Location = new System.Drawing.Point(393, 123);
//            this.lblColumns.Name = "lblColumns";
//            this.lblColumns.Size = new System.Drawing.Size(384, 17);
//            this.lblColumns.TabIndex = 4;
//            this.lblColumns.Text = "3c. Danh sách Cột (Select/Update):";
//            // 
//            // clbColumns
//            // 
//            this.clbColumns.CheckOnClick = true;
//            this.clbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.clbColumns.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.clbColumns.FormattingEnabled = true;
//            this.clbColumns.Location = new System.Drawing.Point(400, 150);
//            this.clbColumns.Margin = new System.Windows.Forms.Padding(10);
//            this.clbColumns.Name = "clbColumns";
//            this.clbColumns.Size = new System.Drawing.Size(370, 360);
//            this.clbColumns.TabIndex = 5;
//            // 
//            // chkWithGrantOption
//            // 
//            this.chkWithGrantOption.AutoSize = true;
//            this.chkWithGrantOption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.chkWithGrantOption.ForeColor = System.Drawing.Color.Red;
//            this.chkWithGrantOption.Location = new System.Drawing.Point(10, 530);
//            this.chkWithGrantOption.Margin = new System.Windows.Forms.Padding(10);
//            this.chkWithGrantOption.Name = "chkWithGrantOption";
//            this.chkWithGrantOption.Size = new System.Drawing.Size(200, 24);
//            this.chkWithGrantOption.TabIndex = 6;
//            this.chkWithGrantOption.Text = "b. WITH GRANT OPTION";
//            this.chkWithGrantOption.UseVisualStyleBackColor = true;
//            // 
//            // btnGrant
//            // 
//            this.btnGrant.Dock = System.Windows.Forms.DockStyle.Right;
//            this.btnGrant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.btnGrant.Location = new System.Drawing.Point(630, 530);
//            this.btnGrant.Margin = new System.Windows.Forms.Padding(10);
//            this.btnGrant.Name = "btnGrant";
//            this.btnGrant.Size = new System.Drawing.Size(140, 40);
//            this.btnGrant.TabIndex = 7;
//            this.btnGrant.Text = "GRANT";
//            this.btnGrant.UseVisualStyleBackColor = true;
//            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
//            // 
//            // uc_Grant
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.Controls.Add(this.tableLayoutPanel1);
//            this.Name = "uc_Grant";
//            this.Padding = new System.Windows.Forms.Padding(10);
//            this.Size = new System.Drawing.Size(800, 600);
//            this.Load += new System.EventHandler(this.uc_Grant_Load);
//            this.tableLayoutPanel1.ResumeLayout(false);
//            this.tableLayoutPanel1.PerformLayout();
//            this.pnlGrantee.ResumeLayout(false);
//            this.pnlGrantee.PerformLayout();
//            this.pnlObject.ResumeLayout(false);
//            this.pnlObject.PerformLayout();
//            this.ResumeLayout(false);

//        }

//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
//        private System.Windows.Forms.Panel pnlGrantee;
//        private System.Windows.Forms.ComboBox cboGrantee;
//        private System.Windows.Forms.Label lblGrantee;
//        private System.Windows.Forms.Label lblGranteeType;
//        private System.Windows.Forms.ComboBox cboGranteeType;
//        private System.Windows.Forms.Panel pnlObject;
//        private System.Windows.Forms.Label lblObjectName;
//        private System.Windows.Forms.ComboBox cboObjectName;
//        private System.Windows.Forms.ComboBox cboObjectType;
//        private System.Windows.Forms.Label lblObjectType;
//        private System.Windows.Forms.Label lblPrivs;
//        private System.Windows.Forms.CheckedListBox clbPrivs;
//        private System.Windows.Forms.Label lblColumns;
//        private System.Windows.Forms.CheckedListBox clbColumns;
//        private System.Windows.Forms.CheckBox chkWithGrantOption;
//        private System.Windows.Forms.Button btnGrant;
//    }
//}


namespace OracleSecurityAdmin
{
    partial class uc_Grant
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGrantee = new System.Windows.Forms.Panel();
            this.cboGrantee = new System.Windows.Forms.ComboBox();
            this.lblGrantee = new System.Windows.Forms.Label();
            this.lblGranteeType = new System.Windows.Forms.Label();
            this.cboGranteeType = new System.Windows.Forms.ComboBox();
            this.pnlObject = new System.Windows.Forms.Panel();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.cboObjectName = new System.Windows.Forms.ComboBox();
            this.cboObjectType = new System.Windows.Forms.ComboBox();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.lblPrivs = new System.Windows.Forms.Label();
            this.clbPrivs = new System.Windows.Forms.CheckedListBox();
            this.lblColumns = new System.Windows.Forms.Label();
            this.clbColumns = new System.Windows.Forms.CheckedListBox();
            this.chkWithGrantOption = new System.Windows.Forms.CheckBox();
            this.btnGrant = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlGrantee.SuspendLayout();
            this.pnlObject.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnlGrantee, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlObject, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPrivs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.clbPrivs, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblColumns, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.clbColumns, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkWithGrantOption, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnGrant, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 580);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlGrantee
            // 
            this.pnlGrantee.Controls.Add(this.cboGrantee);
            this.pnlGrantee.Controls.Add(this.lblGrantee);
            this.pnlGrantee.Controls.Add(this.lblGranteeType);
            this.pnlGrantee.Controls.Add(this.cboGranteeType);
            this.pnlGrantee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrantee.Location = new System.Drawing.Point(3, 3);
            this.pnlGrantee.Name = "pnlGrantee";
            this.pnlGrantee.Size = new System.Drawing.Size(384, 104);
            this.pnlGrantee.TabIndex = 0;
            // 
            // cboGrantee
            // 
            this.cboGrantee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrantee.Location = new System.Drawing.Point(12, 65);
            this.cboGrantee.Name = "cboGrantee";
            this.cboGrantee.Size = new System.Drawing.Size(300, 21);
            this.cboGrantee.TabIndex = 0;
            // 
            // lblGrantee
            // 
            this.lblGrantee.Location = new System.Drawing.Point(12, 45);
            this.lblGrantee.Name = "lblGrantee";
            this.lblGrantee.Size = new System.Drawing.Size(121, 17);
            this.lblGrantee.TabIndex = 1;
            this.lblGrantee.Text = "Người nhận quyền:";
            // 
            // lblGranteeType
            // 
            this.lblGranteeType.Location = new System.Drawing.Point(12, 0);
            this.lblGranteeType.Name = "lblGranteeType";
            this.lblGranteeType.Size = new System.Drawing.Size(130, 17);
            this.lblGranteeType.TabIndex = 2;
            this.lblGranteeType.Text = "Chọn loại (User/Role):";
            // 
            // cboGranteeType
            // 
            this.cboGranteeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGranteeType.Items.AddRange(new object[] {
            "User",
            "Role"});
            this.cboGranteeType.Location = new System.Drawing.Point(12, 17);
            this.cboGranteeType.Name = "cboGranteeType";
            this.cboGranteeType.Size = new System.Drawing.Size(121, 21);
            this.cboGranteeType.TabIndex = 3;
            this.cboGranteeType.SelectedIndexChanged += new System.EventHandler(this.cboGranteeType_SelectedIndexChanged);
            // 
            // pnlObject
            // 
            this.pnlObject.Controls.Add(this.lblObjectName);
            this.pnlObject.Controls.Add(this.cboObjectName);
            this.pnlObject.Controls.Add(this.cboObjectType);
            this.pnlObject.Controls.Add(this.lblObjectType);
            this.pnlObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlObject.Location = new System.Drawing.Point(393, 3);
            this.pnlObject.Name = "pnlObject";
            this.pnlObject.Size = new System.Drawing.Size(384, 104);
            this.pnlObject.TabIndex = 1;
            // 
            // lblObjectName
            // 
            this.lblObjectName.Location = new System.Drawing.Point(12, 45);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(138, 17);
            this.lblObjectName.TabIndex = 0;
            this.lblObjectName.Text = "Chọn đối tượng cụ thể:";
            // 
            // cboObjectName
            // 
            this.cboObjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectName.Location = new System.Drawing.Point(12, 65);
            this.cboObjectName.Name = "cboObjectName";
            this.cboObjectName.Size = new System.Drawing.Size(300, 21);
            this.cboObjectName.TabIndex = 1;
            this.cboObjectName.SelectedIndexChanged += new System.EventHandler(this.cboObjectName_SelectedIndexChanged);
            // 
            // cboObjectType
            // 
            this.cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectType.Items.AddRange(new object[] {
            "TABLE",
            "VIEW",
            "PROCEDURE",
            "FUNCTION"});
            this.cboObjectType.Location = new System.Drawing.Point(12, 17);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(200, 21);
            this.cboObjectType.TabIndex = 2;
            this.cboObjectType.SelectedIndexChanged += new System.EventHandler(this.cboObjectType_SelectedIndexChanged);
            // 
            // lblObjectType
            // 
            this.lblObjectType.Location = new System.Drawing.Point(12, 0);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(123, 17);
            this.lblObjectType.TabIndex = 3;
            this.lblObjectType.Text = "Chọn loại đối tượng:";
            // 
            // lblPrivs
            // 
            this.lblPrivs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPrivs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPrivs.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPrivs.Location = new System.Drawing.Point(3, 117);
            this.lblPrivs.Name = "lblPrivs";
            this.lblPrivs.Size = new System.Drawing.Size(384, 23);
            this.lblPrivs.TabIndex = 2;
            this.lblPrivs.Text = "Danh sách Quyền:";
            // 
            // clbPrivs
            // 
            this.clbPrivs.CheckOnClick = true;
            this.clbPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbPrivs.Location = new System.Drawing.Point(10, 150);
            this.clbPrivs.Margin = new System.Windows.Forms.Padding(10);
            this.clbPrivs.Name = "clbPrivs";
            this.clbPrivs.Size = new System.Drawing.Size(370, 360);
            this.clbPrivs.TabIndex = 3;
            this.clbPrivs.SelectedIndexChanged += new System.EventHandler(this.clbPrivs_SelectedIndexChanged);
            // 
            // lblColumns
            // 
            this.lblColumns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblColumns.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblColumns.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblColumns.Location = new System.Drawing.Point(393, 117);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(384, 23);
            this.lblColumns.TabIndex = 4;
            this.lblColumns.Text = "Chọn Cột (Chỉ Select/Update):";
            // 
            // clbColumns
            // 
            this.clbColumns.CheckOnClick = true;
            this.clbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbColumns.Enabled = false;
            this.clbColumns.Location = new System.Drawing.Point(400, 150);
            this.clbColumns.Margin = new System.Windows.Forms.Padding(10);
            this.clbColumns.Name = "clbColumns";
            this.clbColumns.Size = new System.Drawing.Size(370, 360);
            this.clbColumns.TabIndex = 5;
            this.clbColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbColumns_ItemCheck);
            // 
            // chkWithGrantOption
            // 
            this.chkWithGrantOption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.chkWithGrantOption.ForeColor = System.Drawing.Color.Red;
            this.chkWithGrantOption.Location = new System.Drawing.Point(3, 523);
            this.chkWithGrantOption.Name = "chkWithGrantOption";
            this.chkWithGrantOption.Size = new System.Drawing.Size(200, 24);
            this.chkWithGrantOption.TabIndex = 6;
            this.chkWithGrantOption.Text = "b. WITH GRANT OPTION";
            // 
            // btnGrant
            // 
            this.btnGrant.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGrant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGrant.Location = new System.Drawing.Point(637, 523);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(140, 54);
            this.btnGrant.TabIndex = 7;
            this.btnGrant.Text = "GRANT";
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // uc_Grant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "uc_Grant";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.uc_Grant_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlGrantee.ResumeLayout(false);
            this.pnlObject.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlGrantee;
        private System.Windows.Forms.ComboBox cboGrantee;
        private System.Windows.Forms.Label lblGrantee;
        private System.Windows.Forms.Label lblGranteeType;
        private System.Windows.Forms.ComboBox cboGranteeType;
        private System.Windows.Forms.Panel pnlObject;
        private System.Windows.Forms.Label lblObjectName;
        private System.Windows.Forms.ComboBox cboObjectName;
        private System.Windows.Forms.ComboBox cboObjectType;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.Label lblPrivs;
        private System.Windows.Forms.CheckedListBox clbPrivs;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.CheckedListBox clbColumns;
        private System.Windows.Forms.CheckBox chkWithGrantOption;
        private System.Windows.Forms.Button btnGrant;
    }
}