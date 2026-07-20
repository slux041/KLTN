namespace GD
{
    partial class QLDV
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
            groupBox3 = new GroupBox();
            dgvGiaCanNang = new DataGridView();
            labelBangGia = new Label();
            cmbDanhMuc = new ComboBox();
            label9 = new Label();
            cmbThoiGian = new ComboBox();
            label8 = new Label();
            cmbTrangThai = new ComboBox();
            label7 = new Label();
            txtGia = new TextBox();
            label6 = new Label();
            txtMoTa = new TextBox();
            label5 = new Label();
            txtTenDV = new TextBox();
            label4 = new Label();
            txtMaDV = new TextBox();
            label3 = new Label();
            dgvdichvu = new DataGridView();
            groupBox4 = new GroupBox();
            txtTimKiem = new TextBox();
            groupBox5 = new GroupBox();
            label10 = new Label();
            btnTaoMoi = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGiaCanNang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvdichvu).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.BackColor = Color.Honeydew;
            groupBox3.Controls.Add(dgvGiaCanNang);
            groupBox3.Controls.Add(labelBangGia);
            groupBox3.Controls.Add(cmbDanhMuc);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(cmbThoiGian);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(cmbTrangThai);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(txtGia);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(txtMoTa);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(txtTenDV);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(txtMaDV);
            groupBox3.Controls.Add(label3);
            groupBox3.Font = new Font("Segoe UI", 10.2F);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1159, 250);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin dịch vụ";
            // 
            // dgvGiaCanNang
            // 
            dgvGiaCanNang.ColumnHeadersHeight = 29;
            dgvGiaCanNang.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dgvGiaCanNang.Location = new Point(680, 45);
            dgvGiaCanNang.Name = "dgvGiaCanNang";
            dgvGiaCanNang.RowHeadersWidth = 51;
            dgvGiaCanNang.Size = new Size(470, 195);
            dgvGiaCanNang.TabIndex = 0;
            // 
            // labelBangGia
            // 
            labelBangGia.AutoSize = true;
            labelBangGia.Location = new Point(680, 20);
            labelBangGia.Name = "labelBangGia";
            labelBangGia.Size = new Size(260, 23);
            labelBangGia.TabIndex = 1;
            labelBangGia.Text = "Bảng giá theo cân nặng (nếu có)";
            // 
            // cmbDanhMuc
            // 
            cmbDanhMuc.Location = new Point(120, 152);
            cmbDanhMuc.Name = "cmbDanhMuc";
            cmbDanhMuc.Size = new Size(200, 31);
            cmbDanhMuc.TabIndex = 2;
            // 
            // label9
            // 
            label9.Location = new Point(20, 155);
            label9.Name = "label9";
            label9.Size = new Size(100, 23);
            label9.TabIndex = 3;
            label9.Text = "Danh mục";
            // 
            // cmbThoiGian
            // 
            cmbThoiGian.Location = new Point(450, 72);
            cmbThoiGian.Name = "cmbThoiGian";
            cmbThoiGian.Size = new Size(200, 31);
            cmbThoiGian.TabIndex = 4;
            // 
            // label8
            // 
            label8.Location = new Point(350, 75);
            label8.Name = "label8";
            label8.Size = new Size(100, 23);
            label8.TabIndex = 5;
            label8.Text = "Thời gian";
            // 
            // cmbTrangThai
            // 
            cmbTrangThai.Location = new Point(450, 112);
            cmbTrangThai.Name = "cmbTrangThai";
            cmbTrangThai.Size = new Size(200, 31);
            cmbTrangThai.TabIndex = 6;
            // 
            // label7
            // 
            label7.Location = new Point(350, 115);
            label7.Name = "label7";
            label7.Size = new Size(100, 23);
            label7.TabIndex = 7;
            label7.Text = "Trạng thái";
            // 
            // txtGia
            // 
            txtGia.Location = new Point(450, 32);
            txtGia.Name = "txtGia";
            txtGia.Size = new Size(200, 30);
            txtGia.TabIndex = 8;
            // 
            // label6
            // 
            label6.Location = new Point(350, 35);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 9;
            label6.Text = "Giá gốc";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(120, 112);
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(200, 30);
            txtMoTa.TabIndex = 10;
            // 
            // label5
            // 
            label5.Location = new Point(20, 115);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 11;
            label5.Text = "Mô tả";
            // 
            // txtTenDV
            // 
            txtTenDV.Location = new Point(120, 72);
            txtTenDV.Name = "txtTenDV";
            txtTenDV.Size = new Size(200, 30);
            txtTenDV.TabIndex = 12;
            // 
            // label4
            // 
            label4.Location = new Point(20, 75);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 13;
            label4.Text = "Tên DV";
            // 
            // txtMaDV
            // 
            txtMaDV.Location = new Point(120, 32);
            txtMaDV.Name = "txtMaDV";
            txtMaDV.ReadOnly = true;
            txtMaDV.Size = new Size(200, 30);
            txtMaDV.TabIndex = 14;
            // 
            // label3
            // 
            label3.Location = new Point(20, 35);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 15;
            label3.Text = "Mã DV";
            // 
            // dgvdichvu
            // 
            dgvdichvu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvdichvu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvdichvu.Location = new Point(12, 270);
            dgvdichvu.Name = "dgvdichvu";
            dgvdichvu.RowHeadersWidth = 51;
            dgvdichvu.Size = new Size(1159, 395);
            dgvdichvu.TabIndex = 3;
            dgvdichvu.CellClick += dgvDichvu_CellClick;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.BackColor = Color.BurlyWood;
            groupBox4.Controls.Add(txtTimKiem);
            groupBox4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            groupBox4.Location = new Point(12, 675);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(268, 80);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Tìm kiếm tên dịch vụ";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(20, 35);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(226, 30);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox5.BackColor = Color.LightGray;
            groupBox5.Controls.Add(label10);
            groupBox5.Controls.Add(btnTaoMoi);
            groupBox5.Controls.Add(btnSua);
            groupBox5.Controls.Add(btnXoa);
            groupBox5.Controls.Add(btnThem);
            groupBox5.Location = new Point(702, 675);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(469, 80);
            groupBox5.TabIndex = 15;
            groupBox5.TabStop = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 0);
            label10.Name = "label10";
            label10.Size = new Size(63, 20);
            label10.TabIndex = 0;
            label10.Text = "Công cụ";
            // 
            // btnTaoMoi
            // 
            btnTaoMoi.Location = new Point(40, 30);
            btnTaoMoi.Name = "btnTaoMoi";
            btnTaoMoi.Size = new Size(90, 37);
            btnTaoMoi.TabIndex = 1;
            btnTaoMoi.Text = "Tạo mới";
            btnTaoMoi.Click += btnTaoMoi_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(340, 30);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 37);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(240, 30);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 37);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(140, 30);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 37);
            btnThem.TabIndex = 4;
            btnThem.Text = "Lưu";
            btnThem.Click += btnThem_Click;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Min";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Max";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Giá";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Loại";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;
            // 
            // QLDV
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1183, 767);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(dgvdichvu);
            Controls.Add(groupBox3);
            Name = "QLDV";
            Text = "Quản lý dịch vụ";
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGiaCanNang).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvdichvu).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvGiaCanNang;
        private System.Windows.Forms.Label labelBangGia;
        private System.Windows.Forms.ComboBox cmbDanhMuc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbThoiGian;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbTrangThai;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTenDV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvdichvu;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}