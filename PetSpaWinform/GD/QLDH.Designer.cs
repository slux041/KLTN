namespace GD
{
    partial class QLDH
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
            tabControlMain = new TabControl();
            tabDanhSach = new TabPage();
            groupBoxTools = new GroupBox();
            btnInLai = new Button();
            btnHuyDon = new Button();
            btnCapNhat = new Button();
            btnChiTiet = new Button();
            groupBoxSearch = new GroupBox();
            txtTimKiemDon = new TextBox();
            labelTimKiem = new Label();
            dgvDonHang = new DataGridView();
            groupBoxFilter = new GroupBox();
            btnLoc = new Button();
            dtpDenNgay = new DateTimePicker();
            labelDenNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            labelTuNgay = new Label();
            cmbTrangThaiLoc = new ComboBox();
            labelTrangThaiLoc = new Label();
            tabBanHang = new TabPage();
            splitContainerPOS = new SplitContainer();
            tabControlSanpham = new TabControl();
            tabSP = new TabPage();
            dgvSanPham = new DataGridView();
            tabDV = new TabPage();
            dgvDichVu = new DataGridView();
            panelSearchSP = new Panel();
            btnThemMon = new Button();
            nudSoLuong = new NumericUpDown();
            labelSoLuong = new Label();
            txtTimKiemSP = new TextBox();
            labelTimSP = new Label();
            panelGioHang = new Panel();
            dgvGioHang = new DataGridView();
            panelThanhToan = new Panel();
            labelTienHang = new Label();
            lblTongTienHang = new Label();
            labelGiamGia = new Label();
            txtGiamGia = new TextBox();
            labelTongCong = new Label();
            lblThanhTien = new Label();
            btnThanhToan = new Button();
            panelKhachHang = new Panel();
            lblTenKhach = new Label();
            btnThemKhach = new Button();
            txtSDTKhach = new TextBox();
            labelSDT = new Label();
            tabControlMain.SuspendLayout();
            tabDanhSach.SuspendLayout();
            groupBoxTools.SuspendLayout();
            groupBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).BeginInit();
            groupBoxFilter.SuspendLayout();
            tabBanHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPOS).BeginInit();
            splitContainerPOS.Panel1.SuspendLayout();
            splitContainerPOS.Panel2.SuspendLayout();
            splitContainerPOS.SuspendLayout();
            tabControlSanpham.SuspendLayout();
            tabSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
            tabDV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            panelSearchSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            panelGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
            panelThanhToan.SuspendLayout();
            panelKhachHang.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabDanhSach);
            tabControlMain.Controls.Add(tabBanHang);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Font = new Font("Segoe UI", 10F);
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1200, 700);
            tabControlMain.TabIndex = 0;
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;
            // 
            // tabDanhSach
            // 
            tabDanhSach.Controls.Add(groupBoxTools);
            tabDanhSach.Controls.Add(groupBoxSearch);
            tabDanhSach.Controls.Add(dgvDonHang);
            tabDanhSach.Controls.Add(groupBoxFilter);
            tabDanhSach.Location = new Point(4, 32);
            tabDanhSach.Name = "tabDanhSach";
            tabDanhSach.Padding = new Padding(3);
            tabDanhSach.Size = new Size(1192, 664);
            tabDanhSach.TabIndex = 0;
            tabDanhSach.Text = "Danh sách Đơn hàng";
            tabDanhSach.UseVisualStyleBackColor = true;
            // 
            // groupBoxTools
            // 
            groupBoxTools.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBoxTools.BackColor = Color.LightGray;
            groupBoxTools.Controls.Add(btnInLai);
            groupBoxTools.Controls.Add(btnHuyDon);
            groupBoxTools.Controls.Add(btnCapNhat);
            groupBoxTools.Controls.Add(btnChiTiet);
            groupBoxTools.Location = new Point(600, 580);
            groupBoxTools.Name = "groupBoxTools";
            groupBoxTools.Size = new Size(589, 80);
            groupBoxTools.TabIndex = 3;
            groupBoxTools.TabStop = false;
            groupBoxTools.Text = "Thao tác";
            // 
            // btnInLai
            // 
            btnInLai.Location = new Point(450, 30);
            btnInLai.Name = "btnInLai";
            btnInLai.Size = new Size(120, 35);
            btnInLai.TabIndex = 3;
            btnInLai.Text = "In Hóa Đơn";
            btnInLai.Click += btnInLai_Click;
            // 
            // btnHuyDon
            // 
            btnHuyDon.Location = new Point(310, 30);
            btnHuyDon.Name = "btnHuyDon";
            btnHuyDon.Size = new Size(120, 35);
            btnHuyDon.TabIndex = 2;
            btnHuyDon.Text = "Hủy đơn";
            btnHuyDon.Click += btnHuyDon_Click;
            // 
            // btnCapNhat
            // 
            btnCapNhat.Location = new Point(170, 30);
            btnCapNhat.Name = "btnCapNhat";
            btnCapNhat.Size = new Size(120, 35);
            btnCapNhat.TabIndex = 1;
            btnCapNhat.Text = "Cập nhật TT";
            btnCapNhat.Click += btnCapNhat_Click;
            // 
            // btnChiTiet
            // 
            btnChiTiet.Location = new Point(30, 30);
            btnChiTiet.Name = "btnChiTiet";
            btnChiTiet.Size = new Size(120, 35);
            btnChiTiet.TabIndex = 0;
            btnChiTiet.Text = "Xem Chi Tiết";
            btnChiTiet.Click += btnChiTiet_Click;
            // 
            // groupBoxSearch
            // 
            groupBoxSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxSearch.BackColor = Color.BurlyWood;
            groupBoxSearch.Controls.Add(txtTimKiemDon);
            groupBoxSearch.Controls.Add(labelTimKiem);
            groupBoxSearch.Location = new Point(3, 580);
            groupBoxSearch.Name = "groupBoxSearch";
            groupBoxSearch.Size = new Size(350, 80);
            groupBoxSearch.TabIndex = 2;
            groupBoxSearch.TabStop = false;
            groupBoxSearch.Text = "Tìm kiếm";
            // 
            // txtTimKiemDon
            // 
            txtTimKiemDon.Location = new Point(140, 32);
            txtTimKiemDon.Name = "txtTimKiemDon";
            txtTimKiemDon.Size = new Size(190, 30);
            txtTimKiemDon.TabIndex = 1;
            txtTimKiemDon.TextChanged += txtTimKiemDon_TextChanged;
            // 
            // labelTimKiem
            // 
            labelTimKiem.AutoSize = true;
            labelTimKiem.Location = new Point(10, 35);
            labelTimKiem.Name = "labelTimKiem";
            labelTimKiem.Size = new Size(133, 23);
            labelTimKiem.TabIndex = 0;
            labelTimKiem.Text = "Mã đơn/Tên KH:";
            // 
            // dgvDonHang
            // 
            dgvDonHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonHang.Location = new Point(3, 90);
            dgvDonHang.Name = "dgvDonHang";
            dgvDonHang.RowHeadersWidth = 51;
            dgvDonHang.Size = new Size(1186, 480);
            dgvDonHang.TabIndex = 1;
            dgvDonHang.CellClick += dgvDonHang_CellClick;
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.BackColor = Color.Honeydew;
            groupBoxFilter.Controls.Add(btnLoc);
            groupBoxFilter.Controls.Add(dtpDenNgay);
            groupBoxFilter.Controls.Add(labelDenNgay);
            groupBoxFilter.Controls.Add(dtpTuNgay);
            groupBoxFilter.Controls.Add(labelTuNgay);
            groupBoxFilter.Controls.Add(cmbTrangThaiLoc);
            groupBoxFilter.Controls.Add(labelTrangThaiLoc);
            groupBoxFilter.Dock = DockStyle.Top;
            groupBoxFilter.Location = new Point(3, 3);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Size = new Size(1186, 80);
            groupBoxFilter.TabIndex = 0;
            groupBoxFilter.TabStop = false;
            groupBoxFilter.Text = "Bộ lọc";
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.BurlyWood;
            btnLoc.Location = new Point(760, 30);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(100, 35);
            btnLoc.TabIndex = 6;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = false;
            btnLoc.Click += btnLoc_Click;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(610, 32);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(120, 30);
            dtpDenNgay.TabIndex = 5;
            // 
            // labelDenNgay
            // 
            labelDenNgay.AutoSize = true;
            labelDenNgay.Location = new Point(530, 35);
            labelDenNgay.Name = "labelDenNgay";
            labelDenNgay.Size = new Size(87, 23);
            labelDenNgay.TabIndex = 4;
            labelDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(380, 32);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(120, 30);
            dtpTuNgay.TabIndex = 3;
            // 
            // labelTuNgay
            // 
            labelTuNgay.AutoSize = true;
            labelTuNgay.Location = new Point(300, 35);
            labelTuNgay.Name = "labelTuNgay";
            labelTuNgay.Size = new Size(75, 23);
            labelTuNgay.TabIndex = 2;
            labelTuNgay.Text = "Từ ngày:";
            // 
            // cmbTrangThaiLoc
            // 
            cmbTrangThaiLoc.FormattingEnabled = true;
            cmbTrangThaiLoc.Location = new Point(110, 32);
            cmbTrangThaiLoc.Name = "cmbTrangThaiLoc";
            cmbTrangThaiLoc.Size = new Size(150, 31);
            cmbTrangThaiLoc.TabIndex = 1;
            // 
            // labelTrangThaiLoc
            // 
            labelTrangThaiLoc.AutoSize = true;
            labelTrangThaiLoc.Location = new Point(20, 35);
            labelTrangThaiLoc.Name = "labelTrangThaiLoc";
            labelTrangThaiLoc.Size = new Size(91, 23);
            labelTrangThaiLoc.TabIndex = 0;
            labelTrangThaiLoc.Text = "Trạng thái:";
            // 
            // tabBanHang
            // 
            tabBanHang.Controls.Add(splitContainerPOS);
            tabBanHang.Location = new Point(4, 32);
            tabBanHang.Name = "tabBanHang";
            tabBanHang.Padding = new Padding(3);
            tabBanHang.Size = new Size(1192, 664);
            tabBanHang.TabIndex = 1;
            tabBanHang.Text = "Bán hàng tại quầy (POS)";
            tabBanHang.UseVisualStyleBackColor = true;
            // 
            // splitContainerPOS
            // 
            splitContainerPOS.Dock = DockStyle.Fill;
            splitContainerPOS.Location = new Point(3, 3);
            splitContainerPOS.Name = "splitContainerPOS";
            // 
            // splitContainerPOS.Panel1
            // 
            splitContainerPOS.Panel1.Controls.Add(tabControlSanpham);
            splitContainerPOS.Panel1.Controls.Add(panelSearchSP);
            // 
            // splitContainerPOS.Panel2
            // 
            splitContainerPOS.Panel2.Controls.Add(panelGioHang);
            splitContainerPOS.Panel2.Controls.Add(panelThanhToan);
            splitContainerPOS.Panel2.Controls.Add(panelKhachHang);
            splitContainerPOS.Size = new Size(1186, 658);
            splitContainerPOS.SplitterDistance = 580;
            splitContainerPOS.TabIndex = 0;
            // 
            // tabControlSanpham
            // 
            tabControlSanpham.Controls.Add(tabSP);
            tabControlSanpham.Controls.Add(tabDV);
            tabControlSanpham.Dock = DockStyle.Fill;
            tabControlSanpham.Location = new Point(0, 60);
            tabControlSanpham.Name = "tabControlSanpham";
            tabControlSanpham.SelectedIndex = 0;
            tabControlSanpham.Size = new Size(580, 598);
            tabControlSanpham.TabIndex = 1;
            // 
            // tabSP
            // 
            tabSP.Controls.Add(dgvSanPham);
            tabSP.Location = new Point(4, 32);
            tabSP.Name = "tabSP";
            tabSP.Padding = new Padding(3);
            tabSP.Size = new Size(572, 562);
            tabSP.TabIndex = 0;
            tabSP.Text = "Sản phẩm";
            tabSP.UseVisualStyleBackColor = true;
            // 
            // dgvSanPham
            // 
            dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSanPham.Dock = DockStyle.Fill;
            dgvSanPham.Location = new Point(3, 3);
            dgvSanPham.Name = "dgvSanPham";
            dgvSanPham.RowHeadersWidth = 51;
            dgvSanPham.Size = new Size(566, 556);
            dgvSanPham.TabIndex = 0;
            // 
            // tabDV
            // 
            tabDV.Controls.Add(dgvDichVu);
            tabDV.Location = new Point(4, 29);
            tabDV.Name = "tabDV";
            tabDV.Padding = new Padding(3);
            tabDV.Size = new Size(572, 565);
            tabDV.TabIndex = 1;
            tabDV.Text = "Dịch vụ";
            tabDV.UseVisualStyleBackColor = true;
            // 
            // dgvDichVu
            // 
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Dock = DockStyle.Fill;
            dgvDichVu.Location = new Point(3, 3);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(566, 559);
            dgvDichVu.TabIndex = 0;
            // 
            // panelSearchSP
            // 
            panelSearchSP.BackColor = Color.WhiteSmoke;
            panelSearchSP.Controls.Add(btnThemMon);
            panelSearchSP.Controls.Add(nudSoLuong);
            panelSearchSP.Controls.Add(labelSoLuong);
            panelSearchSP.Controls.Add(txtTimKiemSP);
            panelSearchSP.Controls.Add(labelTimSP);
            panelSearchSP.Dock = DockStyle.Top;
            panelSearchSP.Location = new Point(0, 0);
            panelSearchSP.Name = "panelSearchSP";
            panelSearchSP.Size = new Size(580, 60);
            panelSearchSP.TabIndex = 0;
            // 
            // btnThemMon
            // 
            btnThemMon.BackColor = Color.ForestGreen;
            btnThemMon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThemMon.ForeColor = Color.White;
            btnThemMon.Location = new Point(350, 12);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new Size(120, 40);
            btnThemMon.TabIndex = 4;
            btnThemMon.Text = "Thêm";
            btnThemMon.UseVisualStyleBackColor = false;
            btnThemMon.Click += btnThemMon_Click;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Location = new Point(275, 18);
            nudSoLuong.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(60, 30);
            nudSoLuong.TabIndex = 3;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelSoLuong
            // 
            labelSoLuong.AutoSize = true;
            labelSoLuong.Location = new Point(240, 20);
            labelSoLuong.Name = "labelSoLuong";
            labelSoLuong.Size = new Size(31, 23);
            labelSoLuong.TabIndex = 2;
            labelSoLuong.Text = "SL:";
            // 
            // txtTimKiemSP
            // 
            txtTimKiemSP.Location = new Point(45, 17);
            txtTimKiemSP.Name = "txtTimKiemSP";
            txtTimKiemSP.Size = new Size(180, 30);
            txtTimKiemSP.TabIndex = 1;
            txtTimKiemSP.TextChanged += txtTimKiemSP_TextChanged;
            // 
            // labelTimSP
            // 
            labelTimSP.AutoSize = true;
            labelTimSP.Location = new Point(5, 20);
            labelTimSP.Name = "labelTimSP";
            labelTimSP.Size = new Size(42, 23);
            labelTimSP.TabIndex = 0;
            labelTimSP.Text = "Tìm:";
            // 
            // panelGioHang
            // 
            panelGioHang.Controls.Add(dgvGioHang);
            panelGioHang.Dock = DockStyle.Fill;
            panelGioHang.Location = new Point(0, 80);
            panelGioHang.Name = "panelGioHang";
            panelGioHang.Size = new Size(602, 398);
            panelGioHang.TabIndex = 2;
            // 
            // dgvGioHang
            // 
            dgvGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGioHang.Dock = DockStyle.Fill;
            dgvGioHang.Location = new Point(0, 0);
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.RowHeadersWidth = 51;
            dgvGioHang.Size = new Size(602, 398);
            dgvGioHang.TabIndex = 0;
            dgvGioHang.CellContentClick += dgvGioHang_CellContentClick;
            dgvGioHang.CellEndEdit += dgvGioHang_CellEndEdit;
            // 
            // panelThanhToan
            // 
            panelThanhToan.BackColor = Color.AliceBlue;
            panelThanhToan.Controls.Add(labelTienHang);
            panelThanhToan.Controls.Add(lblTongTienHang);
            panelThanhToan.Controls.Add(labelGiamGia);
            panelThanhToan.Controls.Add(txtGiamGia);
            panelThanhToan.Controls.Add(labelTongCong);
            panelThanhToan.Controls.Add(lblThanhTien);
            panelThanhToan.Controls.Add(btnThanhToan);
            panelThanhToan.Dock = DockStyle.Bottom;
            panelThanhToan.Location = new Point(0, 478);
            panelThanhToan.Name = "panelThanhToan";
            panelThanhToan.Size = new Size(602, 180);
            panelThanhToan.TabIndex = 1;
            // 
            // labelTienHang
            // 
            labelTienHang.AutoSize = true;
            labelTienHang.Location = new Point(10, 10);
            labelTienHang.Name = "labelTienHang";
            labelTienHang.Size = new Size(90, 23);
            labelTienHang.TabIndex = 6;
            labelTienHang.Text = "Tiền hàng:";
            // 
            // lblTongTienHang
            // 
            lblTongTienHang.Location = new Point(200, 10);
            lblTongTienHang.Name = "lblTongTienHang";
            lblTongTienHang.Size = new Size(150, 23);
            lblTongTienHang.TabIndex = 5;
            lblTongTienHang.Text = "0 đ";
            lblTongTienHang.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelGiamGia
            // 
            labelGiamGia.AutoSize = true;
            labelGiamGia.Location = new Point(10, 45);
            labelGiamGia.Name = "labelGiamGia";
            labelGiamGia.Size = new Size(133, 23);
            labelGiamGia.TabIndex = 4;
            labelGiamGia.Text = "Giảm giá (VNĐ):";
            // 
            // txtGiamGia
            // 
            txtGiamGia.Location = new Point(150, 42);
            txtGiamGia.Name = "txtGiamGia";
            txtGiamGia.Size = new Size(150, 30);
            txtGiamGia.TabIndex = 3;
            txtGiamGia.Text = "0";
            txtGiamGia.TextAlign = HorizontalAlignment.Right;
            txtGiamGia.TextChanged += txtGiamGia_TextChanged;
            // 
            // labelTongCong
            // 
            labelTongCong.AutoSize = true;
            labelTongCong.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTongCong.Location = new Point(10, 85);
            labelTongCong.Name = "labelTongCong";
            labelTongCong.Size = new Size(125, 28);
            labelTongCong.TabIndex = 2;
            labelTongCong.Text = "TỔNG TIỀN:";
            // 
            // lblThanhTien
            // 
            lblThanhTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblThanhTien.ForeColor = Color.Red;
            lblThanhTien.Location = new Point(180, 85);
            lblThanhTien.Name = "lblThanhTien";
            lblThanhTien.Size = new Size(200, 30);
            lblThanhTien.TabIndex = 1;
            lblThanhTien.Text = "0 đ";
            lblThanhTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnThanhToan
            // 
            btnThanhToan.BackColor = Color.OrangeRed;
            btnThanhToan.Dock = DockStyle.Bottom;
            btnThanhToan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(0, 130);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(602, 50);
            btnThanhToan.TabIndex = 0;
            btnThanhToan.Text = "THANH TOÁN & IN HÓA ĐƠN";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // panelKhachHang
            // 
            panelKhachHang.BackColor = Color.Honeydew;
            panelKhachHang.Controls.Add(lblTenKhach);
            panelKhachHang.Controls.Add(btnThemKhach);
            panelKhachHang.Controls.Add(txtSDTKhach);
            panelKhachHang.Controls.Add(labelSDT);
            panelKhachHang.Dock = DockStyle.Top;
            panelKhachHang.Location = new Point(0, 0);
            panelKhachHang.Name = "panelKhachHang";
            panelKhachHang.Size = new Size(602, 80);
            panelKhachHang.TabIndex = 0;
            // 
            // lblTenKhach
            // 
            lblTenKhach.AutoSize = true;
            lblTenKhach.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTenKhach.ForeColor = Color.Blue;
            lblTenKhach.Location = new Point(10, 50);
            lblTenKhach.Name = "lblTenKhach";
            lblTenKhach.Size = new Size(77, 23);
            lblTenKhach.TabIndex = 3;
            lblTenKhach.Text = "Khách lẻ";
            // 
            // btnThemKhach
            // 
            btnThemKhach.Location = new Point(268, 12);
            btnThemKhach.Name = "btnThemKhach";
            btnThemKhach.Size = new Size(43, 31);
            btnThemKhach.TabIndex = 2;
            btnThemKhach.Text = "+";
            btnThemKhach.UseVisualStyleBackColor = true;
            // 
            // txtSDTKhach
            // 
            txtSDTKhach.Location = new Point(112, 12);
            txtSDTKhach.Name = "txtSDTKhach";
            txtSDTKhach.Size = new Size(150, 30);
            txtSDTKhach.TabIndex = 1;
            txtSDTKhach.Leave += txtSDTKhach_Leave;
            // 
            // labelSDT
            // 
            labelSDT.AutoSize = true;
            labelSDT.Location = new Point(10, 15);
            labelSDT.Name = "labelSDT";
            labelSDT.Size = new Size(96, 23);
            labelSDT.TabIndex = 0;
            labelSDT.Text = "SĐT Khách:";
            // 
            // QLDH
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(tabControlMain);
            Font = new Font("Segoe UI", 10.2F);
            Name = "QLDH";
            Text = "Quản lý đơn hàng";
            tabControlMain.ResumeLayout(false);
            tabDanhSach.ResumeLayout(false);
            groupBoxTools.ResumeLayout(false);
            groupBoxSearch.ResumeLayout(false);
            groupBoxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).EndInit();
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            tabBanHang.ResumeLayout(false);
            splitContainerPOS.Panel1.ResumeLayout(false);
            splitContainerPOS.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerPOS).EndInit();
            splitContainerPOS.ResumeLayout(false);
            tabControlSanpham.ResumeLayout(false);
            tabSP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
            tabDV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            panelSearchSP.ResumeLayout(false);
            panelSearchSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            panelGioHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
            panelThanhToan.ResumeLayout(false);
            panelThanhToan.PerformLayout();
            panelKhachHang.ResumeLayout(false);
            panelKhachHang.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabDanhSach;
        private System.Windows.Forms.TabPage tabBanHang;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.ComboBox cmbTrangThaiLoc;
        private System.Windows.Forms.Label labelTrangThaiLoc;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label labelTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label labelDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.DataGridView dgvDonHang;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox txtTimKiemDon;
        private System.Windows.Forms.Label labelTimKiem;
        private System.Windows.Forms.GroupBox groupBoxTools;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.Button btnInLai;
        private System.Windows.Forms.Button btnHuyDon;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.SplitContainer splitContainerPOS;
        private System.Windows.Forms.Panel panelSearchSP;
        private System.Windows.Forms.TabControl tabControlSanpham;
        private System.Windows.Forms.TabPage tabSP;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.TabPage tabDV;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.TextBox txtTimKiemSP;
        private System.Windows.Forms.Label labelTimSP;
        private System.Windows.Forms.Panel panelKhachHang;
        private System.Windows.Forms.Label labelSDT;
        private System.Windows.Forms.TextBox txtSDTKhach;
        private System.Windows.Forms.Button btnThemKhach;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.Panel panelThanhToan;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label labelTongCong;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.Label labelGiamGia;
        private System.Windows.Forms.Label lblTongTienHang;
        private System.Windows.Forms.Label labelTienHang;
        private System.Windows.Forms.Panel panelGioHang;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label labelSoLuong;
    }
}