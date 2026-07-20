namespace GD
{
    partial class FrmChiTietDonHang
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblNgayDat = new System.Windows.Forms.Label();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblValTongCong = new System.Windows.Forms.Label();
            this.lblLabelTongCong = new System.Windows.Forms.Label();
            this.lblValGiamGia = new System.Windows.Forms.Label();
            this.lblLabelGiamGia = new System.Windows.Forms.Label();
            this.lblValShip = new System.Windows.Forms.Label();
            this.lblLabelShip = new System.Windows.Forms.Label();
            this.lblValTienHang = new System.Windows.Forms.Label();
            this.lblLabelTienHang = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.grpThongTin);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 230);
            this.pnlHeader.TabIndex = 0;
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.Color.Honeydew;
            this.grpThongTin.Controls.Add(this.lblDiaChi);
            this.grpThongTin.Controls.Add(this.lblSDT);
            this.grpThongTin.Controls.Add(this.lblKhachHang);
            this.grpThongTin.Controls.Add(this.lblTrangThai);
            this.grpThongTin.Controls.Add(this.lblNgayDat);
            this.grpThongTin.Controls.Add(this.lblMaDon);
            this.grpThongTin.Location = new System.Drawing.Point(10, 50);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(860, 170);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chung";
            // 
            // lblMaDon
            // 
            this.lblMaDon.AutoSize = true;
            this.lblMaDon.Location = new System.Drawing.Point(20, 30);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Size = new System.Drawing.Size(73, 23);
            this.lblMaDon.TabIndex = 0;
            this.lblMaDon.Text = "Mã đơn:";
            // 
            // lblNgayDat
            // 
            this.lblNgayDat.AutoSize = true;
            this.lblNgayDat.Location = new System.Drawing.Point(20, 60);
            this.lblNgayDat.Name = "lblNgayDat";
            this.lblNgayDat.Size = new System.Drawing.Size(82, 23);
            this.lblNgayDat.TabIndex = 1;
            this.lblNgayDat.Text = "Ngày đặt:";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.Location = new System.Drawing.Point(20, 90);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(92, 23);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(450, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(107, 23);
            this.lblKhachHang.TabIndex = 3;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(450, 60);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(44, 23);
            this.lblSDT.TabIndex = 4;
            this.lblSDT.Text = "SĐT:";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Location = new System.Drawing.Point(20, 138);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(800, 30);
            this.lblDiaChi.TabIndex = 5;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTieuDe.ForeColor = System.Drawing.Color.IndianRed;
            this.lblTieuDe.Location = new System.Drawing.Point(350, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(277, 37);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CHI TIẾT ĐƠN HÀNG";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlFooter.Controls.Add(this.btnDong);
            this.pnlFooter.Controls.Add(this.lblValTongCong);
            this.pnlFooter.Controls.Add(this.lblLabelTongCong);
            this.pnlFooter.Controls.Add(this.lblValGiamGia);
            this.pnlFooter.Controls.Add(this.lblLabelGiamGia);
            this.pnlFooter.Controls.Add(this.lblValShip);
            this.pnlFooter.Controls.Add(this.lblLabelShip);
            this.pnlFooter.Controls.Add(this.lblValTienHang);
            this.pnlFooter.Controls.Add(this.lblLabelTienHang);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 490);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(900, 160);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.LightGray;
            this.btnDong.Location = new System.Drawing.Point(20, 100);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 40);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // lblValTongCong
            // 
            this.lblValTongCong.AutoSize = true;
            this.lblValTongCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValTongCong.ForeColor = System.Drawing.Color.Red;
            this.lblValTongCong.Location = new System.Drawing.Point(850, 105);
            this.lblValTongCong.Name = "lblValTongCong";
            this.lblValTongCong.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblValTongCong.Size = new System.Drawing.Size(28, 32);
            this.lblValTongCong.TabIndex = 7;
            this.lblValTongCong.Text = "0";
            // 
            // lblLabelTongCong
            // 
            this.lblLabelTongCong.AutoSize = true;
            this.lblLabelTongCong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLabelTongCong.Location = new System.Drawing.Point(600, 105);
            this.lblLabelTongCong.Name = "lblLabelTongCong";
            this.lblLabelTongCong.Size = new System.Drawing.Size(125, 28);
            this.lblLabelTongCong.TabIndex = 6;
            this.lblLabelTongCong.Text = "TỔNG TIỀN:";
            // 
            // lblValGiamGia
            // 
            this.lblValGiamGia.AutoSize = true;
            this.lblValGiamGia.Location = new System.Drawing.Point(850, 70);
            this.lblValGiamGia.Name = "lblValGiamGia";
            this.lblValGiamGia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblValGiamGia.Size = new System.Drawing.Size(19, 23);
            this.lblValGiamGia.TabIndex = 5;
            this.lblValGiamGia.Text = "0";
            // 
            // lblLabelGiamGia
            // 
            this.lblLabelGiamGia.AutoSize = true;
            this.lblLabelGiamGia.Location = new System.Drawing.Point(600, 70);
            this.lblLabelGiamGia.Name = "lblLabelGiamGia";
            this.lblLabelGiamGia.Size = new System.Drawing.Size(82, 23);
            this.lblLabelGiamGia.TabIndex = 4;
            this.lblLabelGiamGia.Text = "Giảm giá:";
            // 
            // lblValShip
            // 
            this.lblValShip.AutoSize = true;
            this.lblValShip.Location = new System.Drawing.Point(850, 40);
            this.lblValShip.Name = "lblValShip";
            this.lblValShip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblValShip.Size = new System.Drawing.Size(19, 23);
            this.lblValShip.TabIndex = 3;
            this.lblValShip.Text = "0";
            // 
            // lblLabelShip
            // 
            this.lblLabelShip.AutoSize = true;
            this.lblLabelShip.Location = new System.Drawing.Point(600, 40);
            this.lblLabelShip.Name = "lblLabelShip";
            this.lblLabelShip.Size = new System.Drawing.Size(130, 23);
            this.lblLabelShip.TabIndex = 2;
            this.lblLabelShip.Text = "Phí vận chuyển:";
            // 
            // lblValTienHang
            // 
            this.lblValTienHang.AutoSize = true;
            this.lblValTienHang.Location = new System.Drawing.Point(850, 10);
            this.lblValTienHang.Name = "lblValTienHang";
            this.lblValTienHang.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblValTienHang.Size = new System.Drawing.Size(19, 23);
            this.lblValTienHang.TabIndex = 1;
            this.lblValTienHang.Text = "0";
            // 
            // lblLabelTienHang
            // 
            this.lblLabelTienHang.AutoSize = true;
            this.lblLabelTienHang.Location = new System.Drawing.Point(600, 10);
            this.lblLabelTienHang.Name = "lblLabelTienHang";
            this.lblLabelTienHang.Size = new System.Drawing.Size(133, 23);
            this.lblLabelTienHang.TabIndex = 0;
            this.lblLabelTienHang.Text = "Tổng tiền hàng:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 230);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.Size = new System.Drawing.Size(900, 260);
            this.dgvChiTiet.TabIndex = 2;
            // 
            // FrmChiTietDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmChiTietDonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết đơn hàng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblNgayDat;
        private System.Windows.Forms.Label lblLabelTienHang;
        private System.Windows.Forms.Label lblValTongCong;
        private System.Windows.Forms.Label lblLabelTongCong;
        private System.Windows.Forms.Label lblValGiamGia;
        private System.Windows.Forms.Label lblLabelGiamGia;
        private System.Windows.Forms.Label lblValShip;
        private System.Windows.Forms.Label lblLabelShip;
        private System.Windows.Forms.Label lblValTienHang;
        private System.Windows.Forms.Button btnDong;
    }
}