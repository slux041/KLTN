namespace GD
{
    partial class QLNV
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbChucVu = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpNgaysinh = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbGioiTinh = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTrangThai = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnTaoMoi = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1159, 200);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin nhân viên";

            this.label3.Location = new System.Drawing.Point(20, 35); this.label3.Text = "Mã NV";
            this.txtMaNV.Location = new System.Drawing.Point(120, 32); this.txtMaNV.Size = new System.Drawing.Size(200, 30);

            this.label4.Location = new System.Drawing.Point(20, 75); this.label4.Text = "Họ tên";
            this.txtHoTen.Location = new System.Drawing.Point(120, 72); this.txtHoTen.Size = new System.Drawing.Size(200, 30);

            this.label5.Location = new System.Drawing.Point(20, 115); this.label5.Text = "SĐT";
            this.txtSDT.Location = new System.Drawing.Point(120, 112); this.txtSDT.Size = new System.Drawing.Size(200, 30);

            this.label6.Location = new System.Drawing.Point(20, 155); this.label6.Text = "Email";
            this.txtEmail.Location = new System.Drawing.Point(120, 152); this.txtEmail.Size = new System.Drawing.Size(200, 30);

            this.label10.Location = new System.Drawing.Point(350, 35); this.label10.Text = "Ngày sinh";
            this.dtpNgaysinh.Location = new System.Drawing.Point(450, 32); this.dtpNgaysinh.Size = new System.Drawing.Size(200, 30);

            this.label14.Location = new System.Drawing.Point(350, 75); this.label14.Text = "Giới tính";
            this.cmbGioiTinh.Location = new System.Drawing.Point(450, 72); this.cmbGioiTinh.Size = new System.Drawing.Size(200, 31); this.cmbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });

            this.label9.Location = new System.Drawing.Point(350, 115); this.label9.Text = "Chức vụ";
            this.cmbChucVu.Location = new System.Drawing.Point(450, 112); this.cmbChucVu.Size = new System.Drawing.Size(200, 31); this.cmbChucVu.Items.AddRange(new object[] { "staff", "admin" });

            this.label7.Location = new System.Drawing.Point(350, 155); this.label7.Text = "Trạng thái";
            this.cmbTrangThai.Location = new System.Drawing.Point(450, 152); this.cmbTrangThai.Size = new System.Drawing.Size(200, 31); this.cmbTrangThai.Items.AddRange(new object[] { "active", "inactive" });

            this.groupBox3.Controls.Add(this.label3); this.groupBox3.Controls.Add(this.txtMaNV);
            this.groupBox3.Controls.Add(this.label4); this.groupBox3.Controls.Add(this.txtHoTen);
            this.groupBox3.Controls.Add(this.label5); this.groupBox3.Controls.Add(this.txtSDT);
            this.groupBox3.Controls.Add(this.label6); this.groupBox3.Controls.Add(this.txtEmail);
            this.groupBox3.Controls.Add(this.label9); this.groupBox3.Controls.Add(this.cmbChucVu);
            this.groupBox3.Controls.Add(this.label10); this.groupBox3.Controls.Add(this.dtpNgaysinh);
            this.groupBox3.Controls.Add(this.label14); this.groupBox3.Controls.Add(this.cmbGioiTinh);
            this.groupBox3.Controls.Add(this.label7); this.groupBox3.Controls.Add(this.cmbTrangThai);

            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Location = new System.Drawing.Point(12, 220);
            this.dataGridView1.Size = new System.Drawing.Size(1159, 460);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox4.Location = new System.Drawing.Point(12, 690);
            this.groupBox4.Size = new System.Drawing.Size(400, 80);
            this.groupBox4.Controls.Add(this.label12); this.groupBox4.Controls.Add(this.txtTimKiem);

            this.label12.Text = "Tìm kiếm theo tên/email"; this.label12.Location = new System.Drawing.Point(10, 5);
            this.txtTimKiem.Location = new System.Drawing.Point(15, 35); this.txtTimKiem.Size = new System.Drawing.Size(250, 30);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Location = new System.Drawing.Point(420, 690);
            this.groupBox5.Size = new System.Drawing.Size(751, 80);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.btnTaoMoi);
            this.groupBox5.Controls.Add(this.btnThem);
            this.groupBox5.Controls.Add(this.btnXoa);
            this.groupBox5.Controls.Add(this.btnSua);
            this.groupBox5.Controls.Add(this.btnResetPass);

            this.label13.Text = "Công cụ"; this.label13.Location = new System.Drawing.Point(6, 0);

            this.btnTaoMoi.Text = "Tạo mới"; this.btnTaoMoi.Location = new System.Drawing.Point(20, 30); this.btnTaoMoi.Size = new System.Drawing.Size(90, 37);
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);

            this.btnThem.Text = "Lưu"; this.btnThem.Location = new System.Drawing.Point(120, 30); this.btnThem.Size = new System.Drawing.Size(90, 37);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnXoa.Text = "Xóa"; this.btnXoa.Location = new System.Drawing.Point(220, 30); this.btnXoa.Size = new System.Drawing.Size(90, 37);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnSua.Text = "Sửa"; this.btnSua.Location = new System.Drawing.Point(320, 30); this.btnSua.Size = new System.Drawing.Size(90, 37);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnResetPass.Text = "Đổi Mật Khẩu"; this.btnResetPass.Location = new System.Drawing.Point(420, 30); this.btnResetPass.Size = new System.Drawing.Size(150, 37);
            this.btnResetPass.Click += new System.EventHandler(this.btnResetPass_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 780);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Name = "QLNV";
            this.Text = "Quản lý nhân viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.groupBox3.ResumeLayout(false); this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false); this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false); this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3, groupBox4, groupBox5;
        private System.Windows.Forms.Label label2, label3, label4, label5, label6, label7, label9, label10, label12, label13, label14;
        private System.Windows.Forms.TextBox txtMaNV, txtHoTen, txtSDT, txtEmail, txtTimKiem;
        private System.Windows.Forms.ComboBox cmbChucVu, cmbGioiTinh, cmbTrangThai;
        private System.Windows.Forms.DateTimePicker dtpNgaysinh;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTaoMoi, btnThem, btnXoa, btnSua, btnResetPass;
    }
}