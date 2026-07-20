namespace GD
{
    partial class QLNCC
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
            this.txtTKBank = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();

            this.dgvNCC = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();

            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1159, 200);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin chi tiết";

            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(33, 39); this.label3.Text = "Mã NCC";
            this.txtMaNCC.Location = new System.Drawing.Point(158, 36); this.txtMaNCC.Size = new System.Drawing.Size(270, 30);
            this.txtMaNCC.ReadOnly = true;

            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(33, 85); this.label4.Text = "Tên NCC";
            this.txtTenNCC.Location = new System.Drawing.Point(158, 82); this.txtTenNCC.Size = new System.Drawing.Size(270, 30);

            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(33, 133); this.label5.Text = "Địa chỉ";
            this.txtDiaChi.Location = new System.Drawing.Point(158, 130); this.txtDiaChi.Size = new System.Drawing.Size(270, 30);

            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(495, 39); this.label6.Text = "Số điện thoại";
            this.txtSDT.Location = new System.Drawing.Point(662, 36); this.txtSDT.Size = new System.Drawing.Size(270, 30);

            this.label7.AutoSize = true; this.label7.Location = new System.Drawing.Point(495, 85); this.label7.Text = "Email";
            this.txtEmail.Location = new System.Drawing.Point(662, 82); this.txtEmail.Size = new System.Drawing.Size(270, 30);

            this.label8.AutoSize = true; this.label8.Location = new System.Drawing.Point(495, 133); this.label8.Text = "Ngân hàng";
            this.txtTKBank.Location = new System.Drawing.Point(662, 130); this.txtTKBank.Size = new System.Drawing.Size(270, 30);

            this.groupBox3.Controls.Add(this.label3); this.groupBox3.Controls.Add(this.txtMaNCC);
            this.groupBox3.Controls.Add(this.label4); this.groupBox3.Controls.Add(this.txtTenNCC);
            this.groupBox3.Controls.Add(this.label5); this.groupBox3.Controls.Add(this.txtDiaChi);
            this.groupBox3.Controls.Add(this.label6); this.groupBox3.Controls.Add(this.txtSDT);
            this.groupBox3.Controls.Add(this.label7); this.groupBox3.Controls.Add(this.txtEmail);
            this.groupBox3.Controls.Add(this.label8); this.groupBox3.Controls.Add(this.txtTKBank);

            // 
            // dgvNCC
            // 
            this.dgvNCC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNCC.Location = new System.Drawing.Point(12, 220);
            this.dgvNCC.Size = new System.Drawing.Size(1159, 370);
            this.dgvNCC.TabIndex = 2;
            this.dgvNCC.BackgroundColor = System.Drawing.Color.White;
            this.dgvNCC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNCC.ReadOnly = true;
            this.dgvNCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNCC.RowHeadersVisible = false;
            this.dgvNCC.EnableHeadersVisualStyles = false;
            this.dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            this.dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvNCC.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvNCC.ColumnHeadersHeight = 40;
            this.dgvNCC.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNCC_CellClick);

            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox4.Location = new System.Drawing.Point(12, 600);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 80);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;

            this.label11.AutoSize = true; this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(6, 3); this.label11.Text = "Tìm kiếm theo tên/SĐT";

            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtTimKiem.Location = new System.Drawing.Point(15, 35); this.txtTimKiem.Size = new System.Drawing.Size(250, 30);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            this.groupBox4.Controls.Add(this.label11); this.groupBox4.Controls.Add(this.txtTimKiem);

            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Location = new System.Drawing.Point(574, 600);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(597, 80);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;

            this.label9.AutoSize = true; this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(6, 0); this.label9.Text = "Công cụ";

            this.btnTaoMoi.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnTaoMoi.Location = new System.Drawing.Point(25, 30); this.btnTaoMoi.Size = new System.Drawing.Size(94, 37);
            this.btnTaoMoi.Text = "Tạo mới"; this.btnTaoMoi.UseVisualStyleBackColor = true;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);

            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnThem.Location = new System.Drawing.Point(145, 30); this.btnThem.Size = new System.Drawing.Size(94, 37);
            this.btnThem.Text = "Lưu"; this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnXoa.Location = new System.Drawing.Point(265, 30); this.btnXoa.Size = new System.Drawing.Size(94, 37);
            this.btnXoa.Text = "Xóa"; this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnSua.Location = new System.Drawing.Point(385, 30); this.btnSua.Size = new System.Drawing.Size(94, 37);
            this.btnSua.Text = "Sửa"; this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.groupBox5.Controls.Add(this.label9); this.groupBox5.Controls.Add(this.btnTaoMoi);
            this.groupBox5.Controls.Add(this.btnThem); this.groupBox5.Controls.Add(this.btnXoa);
            this.groupBox5.Controls.Add(this.btnSua);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 705);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dgvNCC);
            this.Controls.Add(this.groupBox3);
            this.Name = "QLNCC";
            this.Text = "Quản lý nhà cung cấp";

            this.groupBox3.ResumeLayout(false); this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).EndInit();
            this.groupBox4.ResumeLayout(false); this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false); this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2, label8, label7, label6, label5, label4, label3;
        private System.Windows.Forms.TextBox txtTKBank, txtEmail, txtSDT, txtDiaChi, txtTenNCC, txtMaNCC;
        private System.Windows.Forms.DataGridView dgvNCC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSua, btnXoa, btnThem, btnTaoMoi;
    }
}