namespace GD
{
    partial class QLLoaiSP
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenLoai = new System.Windows.Forms.TextBox();
            this.labelTen = new System.Windows.Forms.Label();
            this.txtMaLoai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLoaiSP = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTaoMoi = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiSP)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox3.Controls.Add(this.chkIsActive);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbType);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtMoTa);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtTenLoai);
            this.groupBox3.Controls.Add(this.labelTen);
            this.groupBox3.Controls.Add(this.txtMaLoai);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(976, 180);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin danh mục";          
            
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(600, 133);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(107, 27);
            this.chkIsActive.TabIndex = 9;
            this.chkIsActive.Text = "Kích hoạt";
            this.chkIsActive.UseVisualStyleBackColor = true;
            
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(480, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Trạng thái";
            
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] { "product", "service" });
            this.cmbType.Location = new System.Drawing.Point(600, 36);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(250, 31);
            this.cmbType.TabIndex = 7;
            
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(480, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Phân loại";
            
            this.txtMoTa.Location = new System.Drawing.Point(600, 82);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(250, 30);
            this.txtMoTa.TabIndex = 5;
            
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mô tả";
            
            this.txtTenLoai.Location = new System.Drawing.Point(120, 82);
            this.txtTenLoai.Name = "txtTenLoai";
            this.txtTenLoai.Size = new System.Drawing.Size(250, 30);
            this.txtTenLoai.TabIndex = 3;
            
            this.labelTen.AutoSize = true;
            this.labelTen.Location = new System.Drawing.Point(30, 85);
            this.labelTen.Name = "labelTen";
            this.labelTen.Size = new System.Drawing.Size(69, 23);
            this.labelTen.TabIndex = 2;
            this.labelTen.Text = "Tên loại";
            
            this.txtMaLoai.Location = new System.Drawing.Point(120, 36);
            this.txtMaLoai.Name = "txtMaLoai";
            this.txtMaLoai.ReadOnly = true;
            this.txtMaLoai.Size = new System.Drawing.Size(250, 30);
            this.txtMaLoai.TabIndex = 1;
            
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã loại";
            
            // 
            // dgvLoaiSP
            // 
            this.dgvLoaiSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoaiSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiSP.Location = new System.Drawing.Point(12, 200);
            this.dgvLoaiSP.Name = "dgvLoaiSP";
            this.dgvLoaiSP.RowHeadersWidth = 51;
            this.dgvLoaiSP.Size = new System.Drawing.Size(976, 350);
            this.dgvLoaiSP.TabIndex = 2;
            this.dgvLoaiSP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoaiSP_CellClick);
            
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtTimKiem);
            this.groupBox4.Location = new System.Drawing.Point(12, 560);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 80);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(6, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 23);
            this.label11.TabIndex = 1;
            this.label11.Text = "Tìm kiếm";
            
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTimKiem.Location = new System.Drawing.Point(20, 35);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(250, 30);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btnTaoMoi);
            this.groupBox5.Controls.Add(this.btnThem);
            this.groupBox5.Controls.Add(this.btnXoa);
            this.groupBox5.Controls.Add(this.btnSua);
            this.groupBox5.Location = new System.Drawing.Point(400, 560);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(588, 80);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 23);
            this.label9.TabIndex = 4;
            this.label9.Text = "Công cụ";
            
            this.btnTaoMoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTaoMoi.Location = new System.Drawing.Point(20, 30);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(94, 37);
            this.btnTaoMoi.TabIndex = 0;
            this.btnTaoMoi.Text = "Tạo mới";
            this.btnTaoMoi.UseVisualStyleBackColor = true;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnThem.Location = new System.Drawing.Point(140, 30);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(94, 37);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Lưu";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnXoa.Location = new System.Drawing.Point(260, 30);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(94, 37);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSua.Location = new System.Drawing.Point(380, 30);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(94, 37);
            this.btnSua.TabIndex = 3;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            
            // 
            // QLLoaiSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dgvLoaiSP);
            this.Controls.Add(this.groupBox3);
            this.Name = "QLLoaiSP";
            this.Text = "Quản lý danh mục";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiSP)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTenLoai;
        private System.Windows.Forms.Label labelTen;
        private System.Windows.Forms.TextBox txtMaLoai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvLoaiSP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label label9;
    }
}