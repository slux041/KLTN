namespace GD
{
    partial class QLSP
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
            tabControlKho = new TabControl();
            tabDanhSach = new TabPage();
            pnlPhanTrang = new Panel();
            lblSoTrang = new Label();
            btnSau = new Button();
            btnTruoc = new Button();
            groupBox6 = new GroupBox();
            txtTimKiemTen = new TextBox();
            label13 = new Label();
            groupBox5 = new GroupBox();
            btnTaoMoi = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnThem = new Button();
            label12 = new Label();
            groupBox4 = new GroupBox();
            txtTimkiemMa = new TextBox();
            label11 = new Label();
            dgvSanPham = new DataGridView();
            groupBox3 = new GroupBox();
            btnChonAnh = new Button();
            pictureBoxSanPham = new PictureBox();
            txtThuongHieu = new TextBox();
            label14 = new Label();
            cmbTrangThai = new ComboBox();
            label10 = new Label();
            txtGiaBan = new TextBox();
            txtSL = new TextBox();
            txtDonVi = new TextBox();
            txtTenSP = new TextBox();
            txtLoai = new ComboBox();
            txtMoTa = new TextBox();
            txtMaSP = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tabNhapHang = new TabPage();
            btnXoaSPNhap = new Button();
            lblTongTien = new Label();
            btnLuuPhieu = new Button();
            dgvNhapHang = new DataGridView();
            groupBox8 = new GroupBox();
            cmbSanPham = new ComboBox();
            btnThemSPNhap = new Button();
            label21 = new Label();
            label20 = new Label();
            numSoLuongNhap = new NumericUpDown();
            txtGiaNhap = new TextBox();
            label18 = new Label();
            groupBox7 = new GroupBox();
            cmbNhaCung = new ComboBox();
            label16 = new Label();
            dtpNgayNhap = new DateTimePicker();
            label15 = new Label();
            tabControlKho.SuspendLayout();
            tabDanhSach.SuspendLayout();
            pnlPhanTrang.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSanPham).BeginInit();
            tabNhapHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhapHang).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuongNhap).BeginInit();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlKho
            // 
            tabControlKho.Controls.Add(tabDanhSach);
            tabControlKho.Controls.Add(tabNhapHang);
            tabControlKho.Dock = DockStyle.Fill;
            tabControlKho.Font = new Font("Segoe UI", 10.2F);
            tabControlKho.Location = new Point(0, 0);
            tabControlKho.Name = "tabControlKho";
            tabControlKho.SelectedIndex = 0;
            tabControlKho.Size = new Size(1183, 705);
            tabControlKho.TabIndex = 0;
            tabControlKho.SelectedIndexChanged += tabControlKho_SelectedIndexChanged;
            // 
            // tabDanhSach
            // 
            tabDanhSach.BackColor = Color.WhiteSmoke;
            tabDanhSach.Controls.Add(pnlPhanTrang);
            tabDanhSach.Controls.Add(groupBox6);
            tabDanhSach.Controls.Add(groupBox5);
            tabDanhSach.Controls.Add(groupBox4);
            tabDanhSach.Controls.Add(dgvSanPham);
            tabDanhSach.Controls.Add(groupBox3);
            tabDanhSach.Location = new Point(4, 32);
            tabDanhSach.Name = "tabDanhSach";
            tabDanhSach.Padding = new Padding(3);
            tabDanhSach.Size = new Size(1175, 669);
            tabDanhSach.TabIndex = 0;
            tabDanhSach.Text = "Danh sách Sản phẩm";
            // 
            // pnlPhanTrang
            // 
            pnlPhanTrang.Anchor = AnchorStyles.Bottom;
            pnlPhanTrang.Controls.Add(lblSoTrang);
            pnlPhanTrang.Controls.Add(btnSau);
            pnlPhanTrang.Controls.Add(btnTruoc);
            pnlPhanTrang.Location = new Point(446, 529);
            pnlPhanTrang.Name = "pnlPhanTrang";
            pnlPhanTrang.Size = new Size(300, 40);
            pnlPhanTrang.TabIndex = 16;
            // 
            // lblSoTrang
            // 
            lblSoTrang.AutoSize = true;
            lblSoTrang.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblSoTrang.Location = new Point(125, 8);
            lblSoTrang.Name = "lblSoTrang";
            lblSoTrang.Size = new Size(38, 23);
            lblSoTrang.TabIndex = 1;
            lblSoTrang.Text = "0/0";
            // 
            // btnSau
            // 
            btnSau.Location = new Point(200, 3);
            btnSau.Name = "btnSau";
            btnSau.Size = new Size(75, 30);
            btnSau.TabIndex = 2;
            btnSau.Text = "Sau >";
            btnSau.UseVisualStyleBackColor = true;
            btnSau.Click += btnSau_Click;
            // 
            // btnTruoc
            // 
            btnTruoc.Location = new Point(25, 3);
            btnTruoc.Name = "btnTruoc";
            btnTruoc.Size = new Size(75, 30);
            btnTruoc.TabIndex = 0;
            btnTruoc.Text = "< Trước";
            btnTruoc.UseVisualStyleBackColor = true;
            btnTruoc.Click += btnTruoc_Click;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox6.BackColor = Color.BurlyWood;
            groupBox6.Controls.Add(txtTimKiemTen);
            groupBox6.Controls.Add(label13);
            groupBox6.Location = new Point(276, 573);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(264, 84);
            groupBox6.TabIndex = 15;
            groupBox6.TabStop = false;
            // 
            // txtTimKiemTen
            // 
            txtTimKiemTen.Font = new Font("Segoe UI", 10.2F);
            txtTimKiemTen.Location = new Point(15, 34);
            txtTimKiemTen.Name = "txtTimKiemTen";
            txtTimKiemTen.Size = new Size(235, 30);
            txtTimKiemTen.TabIndex = 2;
            txtTimKiemTen.TextChanged += txtTimKiemTen_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label13.Location = new Point(6, 3);
            label13.Name = "label13";
            label13.Size = new Size(113, 23);
            label13.TabIndex = 0;
            label13.Text = "Tìm theo tên";
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox5.BackColor = Color.LightGray;
            groupBox5.Controls.Add(btnTaoMoi);
            groupBox5.Controls.Add(btnSua);
            groupBox5.Controls.Add(btnXoa);
            groupBox5.Controls.Add(btnThem);
            groupBox5.Controls.Add(label12);
            groupBox5.Location = new Point(696, 573);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(471, 84);
            groupBox5.TabIndex = 14;
            groupBox5.TabStop = false;
            // 
            // btnTaoMoi
            // 
            btnTaoMoi.Font = new Font("Segoe UI", 10.2F);
            btnTaoMoi.Location = new Point(15, 34);
            btnTaoMoi.Name = "btnTaoMoi";
            btnTaoMoi.Size = new Size(94, 37);
            btnTaoMoi.TabIndex = 4;
            btnTaoMoi.Text = "Tạo mới";
            btnTaoMoi.UseVisualStyleBackColor = true;
            btnTaoMoi.Click += btnTaoMoi_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Segoe UI", 10.2F);
            btnSua.Location = new Point(345, 34);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 37);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Segoe UI", 10.2F);
            btnXoa.Location = new Point(235, 34);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 37);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 10.2F);
            btnThem.Location = new Point(125, 34);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 37);
            btnThem.TabIndex = 1;
            btnThem.Text = "Lưu";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label12.Location = new Point(6, 0);
            label12.Name = "label12";
            label12.Size = new Size(75, 23);
            label12.TabIndex = 0;
            label12.Text = "Công cụ";
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.BackColor = Color.BurlyWood;
            groupBox4.Controls.Add(txtTimkiemMa);
            groupBox4.Controls.Add(label11);
            groupBox4.Location = new Point(8, 573);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(250, 84);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            // 
            // txtTimkiemMa
            // 
            txtTimkiemMa.Font = new Font("Segoe UI", 10.2F);
            txtTimkiemMa.Location = new Point(15, 34);
            txtTimkiemMa.Name = "txtTimkiemMa";
            txtTimkiemMa.Size = new Size(218, 30);
            txtTimkiemMa.TabIndex = 1;
            txtTimkiemMa.TextChanged += txtTimkiemMa_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label11.Location = new Point(6, 3);
            label11.Name = "label11";
            label11.Size = new Size(112, 23);
            label11.TabIndex = 0;
            label11.Text = "Tìm theo mã";
            // 
            // dgvSanPham
            // 
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Location = new Point(8, 239);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.RowHeadersWidth = 51;
            dgvSanPham.Size = new Size(1159, 284);
            dgvSanPham.TabIndex = 12;
            dgvSanPham.SelectionChanged += dgvSanPham_SelectionChanged;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.BackColor = Color.Honeydew;
            groupBox3.Controls.Add(btnChonAnh);
            groupBox3.Controls.Add(pictureBoxSanPham);
            groupBox3.Controls.Add(txtThuongHieu);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(cmbTrangThai);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(txtGiaBan);
            groupBox3.Controls.Add(txtSL);
            groupBox3.Controls.Add(txtDonVi);
            groupBox3.Controls.Add(txtTenSP);
            groupBox3.Controls.Add(txtLoai);
            groupBox3.Controls.Add(txtMoTa);
            groupBox3.Controls.Add(txtMaSP);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label2);
            groupBox3.Font = new Font("Segoe UI", 10.2F);
            groupBox3.Location = new Point(8, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1159, 227);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Location = new Point(1020, 90);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(100, 40);
            btnChonAnh.TabIndex = 22;
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.UseVisualStyleBackColor = true;
            btnChonAnh.Click += btnChonAnh_Click;
            // 
            // pictureBoxSanPham
            // 
            pictureBoxSanPham.BackColor = Color.White;
            pictureBoxSanPham.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSanPham.Location = new Point(832, 36);
            pictureBoxSanPham.Name = "pictureBoxSanPham";
            pictureBoxSanPham.Size = new Size(170, 170);
            pictureBoxSanPham.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSanPham.TabIndex = 21;
            pictureBoxSanPham.TabStop = false;
            // 
            // txtThuongHieu
            // 
            txtThuongHieu.Location = new Point(662, 129);
            txtThuongHieu.Name = "txtThuongHieu";
            txtThuongHieu.Size = new Size(125, 30);
            txtThuongHieu.TabIndex = 20;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(495, 132);
            label14.Name = "label14";
            label14.Size = new Size(107, 23);
            label14.TabIndex = 19;
            label14.Text = "Thương hiệu";
            // 
            // cmbTrangThai
            // 
            cmbTrangThai.FormattingEnabled = true;
            cmbTrangThai.Location = new Point(662, 175);
            cmbTrangThai.Name = "cmbTrangThai";
            cmbTrangThai.Size = new Size(125, 31);
            cmbTrangThai.TabIndex = 18;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(495, 178);
            label10.Name = "label10";
            label10.Size = new Size(87, 23);
            label10.TabIndex = 17;
            label10.Text = "Trạng thái";
            // 
            // txtGiaBan
            // 
            txtGiaBan.Location = new Point(662, 82);
            txtGiaBan.Name = "txtGiaBan";
            txtGiaBan.Size = new Size(125, 30);
            txtGiaBan.TabIndex = 16;
            // 
            // txtSL
            // 
            txtSL.Location = new Point(495, 129);
            txtSL.Name = "txtSL";
            txtSL.Size = new Size(10, 30);
            txtSL.TabIndex = 14;
            txtSL.TextChanged += txtSL_TextChanged;
            // 
            // txtDonVi
            // 
            txtDonVi.Location = new Point(662, 36);
            txtDonVi.Name = "txtDonVi";
            txtDonVi.Size = new Size(125, 30);
            txtDonVi.TabIndex = 13;
            // 
            // txtTenSP
            // 
            txtTenSP.Location = new Point(158, 82);
            txtTenSP.Name = "txtTenSP";
            txtTenSP.Size = new Size(270, 30);
            txtTenSP.TabIndex = 12;
            // 
            // txtLoai
            // 
            txtLoai.FormattingEnabled = true;
            txtLoai.Location = new Point(158, 129);
            txtLoai.Name = "txtLoai";
            txtLoai.Size = new Size(270, 31);
            txtLoai.TabIndex = 11;
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(158, 175);
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(270, 30);
            txtMoTa.TabIndex = 10;
            // 
            // txtMaSP
            // 
            txtMaSP.Location = new Point(158, 36);
            txtMaSP.Name = "txtMaSP";
            txtMaSP.ReadOnly = true;
            txtMaSP.Size = new Size(270, 30);
            txtMaSP.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(33, 132);
            label9.Name = "label9";
            label9.Size = new Size(121, 23);
            label9.TabIndex = 7;
            label9.Text = "Loại sản phẩm";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(495, 132);
            label8.Name = "label8";
            label8.Size = new Size(109, 23);
            label8.TabIndex = 6;
            label8.Text = "Số lượng tồn";
            label8.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 178);
            label7.Name = "label7";
            label7.Size = new Size(55, 23);
            label7.TabIndex = 5;
            label7.Text = "Mô tả";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 85);
            label6.Name = "label6";
            label6.Size = new Size(116, 23);
            label6.TabIndex = 4;
            label6.Text = "Tên sản phẩm";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(495, 85);
            label5.Name = "label5";
            label5.Size = new Size(69, 23);
            label5.TabIndex = 3;
            label5.Text = "Giá bán";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(495, 39);
            label4.Name = "label4";
            label4.Size = new Size(59, 23);
            label4.TabIndex = 2;
            label4.Text = "Đơn vị";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 39);
            label3.Name = "label3";
            label3.Size = new Size(114, 23);
            label3.TabIndex = 1;
            label3.Text = "Mã sản phẩm";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(3, 2);
            label2.Name = "label2";
            label2.Size = new Size(149, 20);
            label2.TabIndex = 0;
            label2.Text = "Thông tin sản phẩm";
            // 
            // tabNhapHang
            // 
            tabNhapHang.BackColor = Color.White;
            tabNhapHang.Controls.Add(btnXoaSPNhap);
            tabNhapHang.Controls.Add(lblTongTien);
            tabNhapHang.Controls.Add(btnLuuPhieu);
            tabNhapHang.Controls.Add(dgvNhapHang);
            tabNhapHang.Controls.Add(groupBox8);
            tabNhapHang.Controls.Add(groupBox7);
            tabNhapHang.Location = new Point(4, 32);
            tabNhapHang.Name = "tabNhapHang";
            tabNhapHang.Padding = new Padding(3);
            tabNhapHang.Size = new Size(1175, 669);
            tabNhapHang.TabIndex = 1;
            tabNhapHang.Text = "Nhập hàng";
            // 
            // btnXoaSPNhap
            // 
            btnXoaSPNhap.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoaSPNhap.BackColor = Color.IndianRed;
            btnXoaSPNhap.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnXoaSPNhap.ForeColor = Color.White;
            btnXoaSPNhap.Location = new Point(833, 617);
            btnXoaSPNhap.Name = "btnXoaSPNhap";
            btnXoaSPNhap.Size = new Size(164, 46);
            btnXoaSPNhap.TabIndex = 5;
            btnXoaSPNhap.Text = "XÓA MÓN";
            btnXoaSPNhap.UseVisualStyleBackColor = false;
            btnXoaSPNhap.Click += btnXoaSPNhap_Click;
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTongTien.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Location = new Point(502, 620);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(325, 41);
            lblTongTien.TabIndex = 4;
            lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLuuPhieu
            // 
            btnLuuPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuuPhieu.BackColor = Color.SeaGreen;
            btnLuuPhieu.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnLuuPhieu.ForeColor = Color.White;
            btnLuuPhieu.Location = new Point(1003, 617);
            btnLuuPhieu.Name = "btnLuuPhieu";
            btnLuuPhieu.Size = new Size(164, 46);
            btnLuuPhieu.TabIndex = 3;
            btnLuuPhieu.Text = "LƯU PHIẾU";
            btnLuuPhieu.UseVisualStyleBackColor = false;
            btnLuuPhieu.Click += btnLuuPhieu_Click;
            // 
            // dgvNhapHang
            // 
            dgvNhapHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNhapHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhapHang.Location = new Point(8, 200);
            dgvNhapHang.Name = "dgvNhapHang";
            dgvNhapHang.RowHeadersWidth = 51;
            dgvNhapHang.Size = new Size(1159, 411);
            dgvNhapHang.TabIndex = 2;
            // 
            // groupBox8
            // 
            groupBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox8.BackColor = Color.AliceBlue;
            groupBox8.Controls.Add(cmbSanPham);
            groupBox8.Controls.Add(btnThemSPNhap);
            groupBox8.Controls.Add(label21);
            groupBox8.Controls.Add(label20);
            groupBox8.Controls.Add(numSoLuongNhap);
            groupBox8.Controls.Add(txtGiaNhap);
            groupBox8.Controls.Add(label18);
            groupBox8.Location = new Point(8, 92);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(1159, 102);
            groupBox8.TabIndex = 1;
            groupBox8.TabStop = false;
            groupBox8.Text = "Chi tiết nhập";
            // 
            // cmbSanPham
            // 
            cmbSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSanPham.FormattingEnabled = true;
            cmbSanPham.Location = new Point(120, 50);
            cmbSanPham.Name = "cmbSanPham";
            cmbSanPham.Size = new Size(350, 31);
            cmbSanPham.TabIndex = 10;
            cmbSanPham.SelectedIndexChanged += cmbSanPham_SelectedIndexChanged;
            // 
            // btnThemSPNhap
            // 
            btnThemSPNhap.Location = new Point(920, 50);
            btnThemSPNhap.Name = "btnThemSPNhap";
            btnThemSPNhap.Size = new Size(120, 35);
            btnThemSPNhap.TabIndex = 9;
            btnThemSPNhap.Text = "Thêm vào list";
            btnThemSPNhap.UseVisualStyleBackColor = true;
            btnThemSPNhap.Click += btnThemSPNhap_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(750, 25);
            label21.Name = "label21";
            label21.Size = new Size(78, 23);
            label21.TabIndex = 8;
            label21.Text = "Số lượng";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(580, 25);
            label20.Name = "label20";
            label20.Size = new Size(79, 23);
            label20.TabIndex = 7;
            label20.Text = "Giá nhập";
            // 
            // numSoLuongNhap
            // 
            numSoLuongNhap.Location = new Point(750, 50);
            numSoLuongNhap.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numSoLuongNhap.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSoLuongNhap.Name = "numSoLuongNhap";
            numSoLuongNhap.Size = new Size(120, 30);
            numSoLuongNhap.TabIndex = 6;
            numSoLuongNhap.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // txtGiaNhap
            // 
            txtGiaNhap.Location = new Point(580, 50);
            txtGiaNhap.Name = "txtGiaNhap";
            txtGiaNhap.Size = new Size(150, 30);
            txtGiaNhap.TabIndex = 5;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(20, 50);
            label18.Name = "label18";
            label18.Size = new Size(87, 23);
            label18.TabIndex = 0;
            label18.Text = "Sản phẩm";
            // 
            // groupBox7
            // 
            groupBox7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox7.Controls.Add(cmbNhaCung);
            groupBox7.Controls.Add(label16);
            groupBox7.Controls.Add(dtpNgayNhap);
            groupBox7.Controls.Add(label15);
            groupBox7.Location = new Point(8, 6);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(1159, 80);
            groupBox7.TabIndex = 0;
            groupBox7.TabStop = false;
            groupBox7.Text = "Thông tin chung";
            // 
            // cmbNhaCung
            // 
            cmbNhaCung.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNhaCung.FormattingEnabled = true;
            cmbNhaCung.Location = new Point(135, 30);
            cmbNhaCung.Name = "cmbNhaCung";
            cmbNhaCung.Size = new Size(300, 31);
            cmbNhaCung.TabIndex = 3;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(20, 33);
            label16.Name = "label16";
            label16.Size = new Size(117, 23);
            label16.TabIndex = 2;
            label16.Text = "Nhà cung cấp";
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.Format = DateTimePickerFormat.Short;
            dtpNgayNhap.Location = new Point(580, 30);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(200, 30);
            dtpNgayNhap.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(480, 33);
            label15.Name = "label15";
            label15.Size = new Size(94, 23);
            label15.TabIndex = 0;
            label15.Text = "Ngày nhập";
            // 
            // QLSP
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1183, 705);
            Controls.Add(tabControlKho);
            Name = "QLSP";
            Text = "Quản lý Kho (Sản phẩm & Nhập hàng)";
            tabControlKho.ResumeLayout(false);
            tabDanhSach.ResumeLayout(false);
            pnlPhanTrang.ResumeLayout(false);
            pnlPhanTrang.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSanPham).EndInit();
            tabNhapHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNhapHang).EndInit();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuongNhap).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlKho;
        private System.Windows.Forms.TabPage tabDanhSach;
        private System.Windows.Forms.TabPage tabNhapHang;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtSL;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.ComboBox txtLoai;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTimkiemMa;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtTimKiemTen;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbTrangThai;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtThuongHieu;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Button btnTruoc;
        private System.Windows.Forms.Button btnSau;
        private System.Windows.Forms.Label lblSoTrang;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbNhaCung;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnThemSPNhap;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numSoLuongNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dgvNhapHang;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.ComboBox cmbSanPham;
        private System.Windows.Forms.Button btnXoaSPNhap;
        private System.Windows.Forms.PictureBox pictureBoxSanPham;
        private System.Windows.Forms.Button btnChonAnh;
    }
}