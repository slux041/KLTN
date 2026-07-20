namespace GD
{
    partial class FrmThanhToan
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblDichVuChinh = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCalc = new System.Windows.Forms.GroupBox();
            this.lblGiaDVChinh = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCanNang = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxAddon = new System.Windows.Forms.GroupBox();
            this.clbDichVuPhu = new System.Windows.Forms.CheckedListBox();
            this.groupBoxProduct = new System.Windows.Forms.GroupBox();
            this.lstCart = new System.Windows.Forms.ListBox();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.cmbSanPham = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.labelTitle.Text = "THANH TOÁN DỊCH VỤ";
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.IndianRed;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.Height = 60;
            this.groupBoxInfo.Text = "Thông tin chung";
            this.groupBoxInfo.Location = new System.Drawing.Point(20, 70);
            this.groupBoxInfo.Size = new System.Drawing.Size(760, 80);
            this.groupBoxInfo.Controls.Add(this.label1); this.groupBoxInfo.Controls.Add(this.lblKhachHang);
            this.groupBoxInfo.Controls.Add(this.label2); this.groupBoxInfo.Controls.Add(this.lblDichVuChinh);
            this.label1.Text = "Khách hàng:"; this.label1.Location = new System.Drawing.Point(20, 30);
            this.lblKhachHang.Text = "..."; this.lblKhachHang.Location = new System.Drawing.Point(120, 30); this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Text = "Dịch vụ chính:"; this.label2.Location = new System.Drawing.Point(400, 30);
            this.lblDichVuChinh.Text = "..."; this.lblDichVuChinh.Location = new System.Drawing.Point(500, 30); this.lblDichVuChinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCalc.Text = "Tính tiền theo cân nặng";
            this.groupBoxCalc.Location = new System.Drawing.Point(20, 160);
            this.groupBoxCalc.Size = new System.Drawing.Size(370, 100);
            this.label3.Text = "Nhập Cân nặng (kg):"; this.label3.Location = new System.Drawing.Point(20, 40); this.label3.AutoSize = true;
            this.txtCanNang.Location = new System.Drawing.Point(150, 35); this.txtCanNang.Size = new System.Drawing.Size(80, 30);
            this.txtCanNang.TextChanged += new System.EventHandler(this.txtCanNang_TextChanged);
            this.label4.Text = "Thành tiền:"; this.label4.Location = new System.Drawing.Point(20, 75);
            this.lblGiaDVChinh.Text = "0 VNĐ"; this.lblGiaDVChinh.Location = new System.Drawing.Point(150, 75); this.lblGiaDVChinh.ForeColor = System.Drawing.Color.Red;
            this.groupBoxCalc.Controls.Add(this.label3); this.groupBoxCalc.Controls.Add(this.txtCanNang);
            this.groupBoxCalc.Controls.Add(this.label4); this.groupBoxCalc.Controls.Add(this.lblGiaDVChinh);
            this.groupBoxAddon.Text = "Dịch vụ phụ (Chọn thêm)";
            this.groupBoxAddon.Location = new System.Drawing.Point(410, 160);
            this.groupBoxAddon.Size = new System.Drawing.Size(370, 100);
            this.clbDichVuPhu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbDichVuPhu.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDichVuPhu_ItemCheck);
            this.groupBoxAddon.Controls.Add(this.clbDichVuPhu);
            this.groupBoxProduct.Text = "Mua thêm sản phẩm";
            this.groupBoxProduct.Location = new System.Drawing.Point(20, 270);
            this.groupBoxProduct.Size = new System.Drawing.Size(760, 150);
            this.label5.Text = "Chọn sản phẩm:"; this.label5.Location = new System.Drawing.Point(20, 35);
            this.cmbSanPham.Location = new System.Drawing.Point(130, 30); this.cmbSanPham.Size = new System.Drawing.Size(250, 30);
            this.btnThemSP.Text = "Thêm"; this.btnThemSP.Location = new System.Drawing.Point(400, 30); this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);
            this.lstCart.Location = new System.Drawing.Point(20, 70); this.lstCart.Size = new System.Drawing.Size(720, 70);
            this.groupBoxProduct.Controls.Add(this.label5); this.groupBoxProduct.Controls.Add(this.cmbSanPham);
            this.groupBoxProduct.Controls.Add(this.btnThemSP); this.groupBoxProduct.Controls.Add(this.lstCart);
            this.labelTotal.Text = "TỔNG THANH TOÁN:"; this.labelTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTotal.Location = new System.Drawing.Point(300, 450); this.labelTotal.AutoSize = true;
            this.lblTongTien.Text = "0 VNĐ"; this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Red; this.lblTongTien.Location = new System.Drawing.Point(520, 440); this.lblTongTien.AutoSize = true;
            this.btnThanhToan.Text = "THANH TOÁN & IN HĐ"; this.btnThanhToan.BackColor = System.Drawing.Color.OrangeRed; this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.Location = new System.Drawing.Point(550, 500); this.btnThanhToan.Size = new System.Drawing.Size(230, 50);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.groupBoxCalc);
            this.Controls.Add(this.groupBoxAddon);
            this.Controls.Add(this.groupBoxProduct);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.btnThanhToan);
        }
        private System.Windows.Forms.Label labelTitle, label1, lblKhachHang, label2, lblDichVuChinh, label3, label4, lblGiaDVChinh, label5, labelTotal, lblTongTien;
        private System.Windows.Forms.GroupBox groupBoxInfo, groupBoxCalc, groupBoxAddon, groupBoxProduct;
        private System.Windows.Forms.TextBox txtCanNang;
        private System.Windows.Forms.CheckedListBox clbDichVuPhu;
        private System.Windows.Forms.ComboBox cmbSanPham;
        private System.Windows.Forms.Button btnThemSP, btnThanhToan, btnHuy;
        private System.Windows.Forms.ListBox lstCart;
    }
}