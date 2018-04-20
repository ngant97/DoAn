namespace QLGN
{
    partial class frmThemShipper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemShipper));
            this.tabFormControl1 = new DevExpress.XtraBars.TabFormControl();
            this.tabFormPage1 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
            this.btLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btNhapLai = new DevExpress.XtraEditors.SimpleButton();
            this.luedPhuongTien = new DevExpress.XtraEditors.LookUpEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtBienSoXe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btTaoMa = new DevExpress.XtraEditors.SimpleButton();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaShiper = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).BeginInit();
            this.tabFormContentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luedPhuongTien.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabFormControl1
            // 
            this.tabFormControl1.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabFormControl1.Name = "tabFormControl1";
            this.tabFormControl1.Pages.Add(this.tabFormPage1);
            this.tabFormControl1.SelectedPage = this.tabFormPage1;
            this.tabFormControl1.ShowAddPageButton = false;
            this.tabFormControl1.ShowTabCloseButtons = false;
            this.tabFormControl1.ShowTabsInTitleBar = DevExpress.XtraBars.ShowTabsInTitleBar.True;
            this.tabFormControl1.Size = new System.Drawing.Size(746, 32);
            this.tabFormControl1.TabForm = this;
            this.tabFormControl1.TabIndex = 0;
            this.tabFormControl1.TabStop = false;
            // 
            // tabFormPage1
            // 
            this.tabFormPage1.ContentContainer = this.tabFormContentContainer1;
            this.tabFormPage1.Image = ((System.Drawing.Image)(resources.GetObject("tabFormPage1.Image")));
            this.tabFormPage1.Name = "tabFormPage1";
            this.tabFormPage1.Text = "Thêm shipper";
            // 
            // tabFormContentContainer1
            // 
            this.tabFormContentContainer1.Controls.Add(this.btLuu);
            this.tabFormContentContainer1.Controls.Add(this.btNhapLai);
            this.tabFormContentContainer1.Controls.Add(this.luedPhuongTien);
            this.tabFormContentContainer1.Controls.Add(this.label5);
            this.tabFormContentContainer1.Controls.Add(this.txtSoDienThoai);
            this.tabFormContentContainer1.Controls.Add(this.txtBienSoXe);
            this.tabFormContentContainer1.Controls.Add(this.label4);
            this.tabFormContentContainer1.Controls.Add(this.label3);
            this.tabFormContentContainer1.Controls.Add(this.btTaoMa);
            this.tabFormContentContainer1.Controls.Add(this.txtTen);
            this.tabFormContentContainer1.Controls.Add(this.label2);
            this.tabFormContentContainer1.Controls.Add(this.label1);
            this.tabFormContentContainer1.Controls.Add(this.txtMaShiper);
            this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 32);
            this.tabFormContentContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabFormContentContainer1.Name = "tabFormContentContainer1";
            this.tabFormContentContainer1.Size = new System.Drawing.Size(746, 330);
            this.tabFormContentContainer1.TabIndex = 1;
            // 
            // btLuu
            // 
            this.btLuu.Location = new System.Drawing.Point(517, 257);
            this.btLuu.Name = "btLuu";
            this.btLuu.Size = new System.Drawing.Size(98, 28);
            this.btLuu.TabIndex = 13;
            this.btLuu.Text = "Lưu";
            this.btLuu.Click += new System.EventHandler(this.btLuu_Click);
            // 
            // btNhapLai
            // 
            this.btNhapLai.Location = new System.Drawing.Point(384, 257);
            this.btNhapLai.Name = "btNhapLai";
            this.btNhapLai.Size = new System.Drawing.Size(98, 28);
            this.btNhapLai.TabIndex = 11;
            this.btNhapLai.Text = "Nhập lại";
            this.btNhapLai.Click += new System.EventHandler(this.btNhapLai_Click);
            // 
            // luedPhuongTien
            // 
            this.luedPhuongTien.Location = new System.Drawing.Point(212, 213);
            this.luedPhuongTien.Name = "luedPhuongTien";
            this.luedPhuongTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luedPhuongTien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPhuongTien", "Tên phương tiện"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PhiPhuongTien", "Phí phương tiện"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaPhuongTien", "Mã phương tiện", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.luedPhuongTien.Properties.NullText = "";
            this.luedPhuongTien.Size = new System.Drawing.Size(261, 22);
            this.luedPhuongTien.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Phương tiện";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(212, 173);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(261, 23);
            this.txtSoDienThoai.TabIndex = 8;
            // 
            // txtBienSoXe
            // 
            this.txtBienSoXe.Location = new System.Drawing.Point(212, 133);
            this.txtBienSoXe.Name = "txtBienSoXe";
            this.txtBienSoXe.Size = new System.Drawing.Size(261, 23);
            this.txtBienSoXe.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số điện thoại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Biển số xe";
            // 
            // btTaoMa
            // 
            this.btTaoMa.Location = new System.Drawing.Point(512, 53);
            this.btTaoMa.Name = "btTaoMa";
            this.btTaoMa.Size = new System.Drawing.Size(103, 26);
            this.btTaoMa.TabIndex = 4;
            this.btTaoMa.Text = "Tạo mã";
            this.btTaoMa.Click += new System.EventHandler(this.btTaoMa_Click);
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(212, 93);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(261, 23);
            this.txtTen.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ và tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã Shipper";
            // 
            // txtMaShiper
            // 
            this.txtMaShiper.Location = new System.Drawing.Point(212, 53);
            this.txtMaShiper.Name = "txtMaShiper";
            this.txtMaShiper.Size = new System.Drawing.Size(261, 23);
            this.txtMaShiper.TabIndex = 0;
            // 
            // frmThemShipper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 362);
            this.Controls.Add(this.tabFormContentContainer1);
            this.Controls.Add(this.tabFormControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmThemShipper";
            this.TabFormControl = this.tabFormControl1;
            this.Text = "frmThemShipper";
            this.Load += new System.EventHandler(this.frmThemShipper_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).EndInit();
            this.tabFormContentContainer1.ResumeLayout(false);
            this.tabFormContentContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luedPhuongTien.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.TabFormControl tabFormControl1;
        private DevExpress.XtraBars.TabFormPage tabFormPage1;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
        private DevExpress.XtraEditors.SimpleButton btTaoMa;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaShiper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btLuu;
        private DevExpress.XtraEditors.SimpleButton btNhapLai;
        private DevExpress.XtraEditors.LookUpEdit luedPhuongTien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtBienSoXe;
    }
}