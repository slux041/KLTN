namespace GD
{
    partial class QLKM
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
            this.cmbTrangThai = new System.Windows.Forms.ComboBox();
            this.txtGTGiam = new System.Windows.Forms.TextBox();
            this.dtpKetThuc = new System.Windows.Forms.DateTimePicker();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtTenKM = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtbBatDau = new System.Windows.Forms.DateTimePicker();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaKM = new System.Windows.Forms.TextBox();
            this.dgvKhuyenMai = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTKKM = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnTaoMoi = new System.Windows.Forms.Button();

            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();

            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1159, 200);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin khuyến mãi";

            this.label2.Location = new System.Drawing.Point(30, 40); this.label2.Text = "Mã Code";
            this.txtCode.Location = new System.Drawing.Point(150, 35); this.txtCode.Size = new System.Drawing.Size(250, 30);

            this.label4.Location = new System.Drawing.Point(30, 90); this.label4.Text = "Mô tả";
            this.txtMoTa.Location = new System.Drawing.Point(150, 85); this.txtMoTa.Size = new System.Drawing.Size(250, 30);

            this.label5.Location = new System.Drawing.Point(30, 140); this.label5.Text = "Giảm giá (%)";
            this.txtGTGiam.Location = new System.Drawing.Point(150, 135); this.txtGTGiam.Size = new System.Drawing.Size(250, 30);

            this.label6.Location = new System.Drawing.Point(480, 40); this.label6.Text = "Ngày bắt đầu";
            this.dtbBatDau.Location = new System.Drawing.Point(600, 35); this.dtbBatDau.Size = new System.Drawing.Size(250, 30);
            this.dtbBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.label7.Location = new System.Drawing.Point(480, 90); this.label7.Text = "Ngày kết thúc";
            this.dtpKetThuc.Location = new System.Drawing.Point(600, 85); this.dtpKetThuc.Size = new System.Drawing.Size(250, 30);
            this.dtpKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.label8.Location = new System.Drawing.Point(480, 140); this.label8.Text = "Trạng thái";
            this.cmbTrangThai.Location = new System.Drawing.Point(600, 135); this.cmbTrangThai.Size = new System.Drawing.Size(250, 30);

            this.txtMaKM.Visible = false;

            this.groupBox3.Controls.Add(this.txtCode); this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtMoTa); this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtGTGiam); this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.dtbBatDau); this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.dtpKetThuc); this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbTrangThai); this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtMaKM);

            // 
            // dgvKhuyenMai
            // 
            this.dgvKhuyenMai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKhuyenMai.Location = new System.Drawing.Point(12, 220);
            this.dgvKhuyenMai.Size = new System.Drawing.Size(1159, 366);
            this.dgvKhuyenMai.TabIndex = 2;
            this.dgvKhuyenMai.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhuyenMai_CellClick);

            // groupBox4
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.BurlyWood;
            this.groupBox4.Location = new System.Drawing.Point(12, 600);
            this.groupBox4.Size = new System.Drawing.Size(300, 80);
            this.groupBox4.Controls.Add(this.label11); this.groupBox4.Controls.Add(this.txtTKKM);

            this.label11.Text = "Tìm kiếm theo mã"; this.label11.Location = new System.Drawing.Point(10, 5);
            this.txtTKKM.Location = new System.Drawing.Point(15, 35); this.txtTKKM.Size = new System.Drawing.Size(250, 30);
            this.txtTKKM.TextChanged += new System.EventHandler(this.txtTKKM_TextChanged);

            // groupBox6
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.BackColor = System.Drawing.Color.LightGray;
            this.groupBox6.Location = new System.Drawing.Point(650, 600);
            this.groupBox6.Size = new System.Drawing.Size(520, 80);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.btnTaoMoi);
            this.groupBox6.Controls.Add(this.btnThem);
            this.groupBox6.Controls.Add(this.btnXoa);
            this.groupBox6.Controls.Add(this.btnSua);

            this.label9.Text = "Công cụ"; this.label9.Location = new System.Drawing.Point(6, 0);

            this.btnTaoMoi.Text = "Tạo mới"; this.btnTaoMoi.Location = new System.Drawing.Point(20, 30); this.btnTaoMoi.Size = new System.Drawing.Size(90, 37);
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);

            this.btnThem.Text = "Lưu"; this.btnThem.Location = new System.Drawing.Point(120, 30); this.btnThem.Size = new System.Drawing.Size(90, 37);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnXoa.Text = "Xóa"; this.btnXoa.Location = new System.Drawing.Point(220, 30); this.btnXoa.Size = new System.Drawing.Size(90, 37);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnSua.Text = "Sửa"; this.btnSua.Location = new System.Drawing.Point(320, 30); this.btnSua.Size = new System.Drawing.Size(90, 37);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(1183, 705);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dgvKhuyenMai);
            this.Controls.Add(this.groupBox3);
            this.Name = "QLKM";
            this.Text = "Quản lý khuyến mãi";
            this.groupBox3.ResumeLayout(false); this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuyenMai)).EndInit();
            this.groupBox4.ResumeLayout(false); this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false); this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCode, txtMoTa, txtGTGiam, txtMaKM, txtTenKM;
        private System.Windows.Forms.DateTimePicker dtbBatDau, dtpKetThuc;
        private System.Windows.Forms.ComboBox cmbTrangThai;
        private System.Windows.Forms.Label label2, label4, label5, label6, label7, label8, label3;
        private System.Windows.Forms.DataGridView dgvKhuyenMai;
        private System.Windows.Forms.GroupBox groupBox4, groupBox6;
        private System.Windows.Forms.TextBox txtTKKM;
        private System.Windows.Forms.Label label11, label9;
        private System.Windows.Forms.Button btnTaoMoi, btnThem, btnXoa, btnSua;
    }
}