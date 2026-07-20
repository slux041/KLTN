using GD.DTOs;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class FrmChiTietDonHang : Form
    {
        private OrderDto _order;

        public FrmChiTietDonHang(OrderDto order)
        {
            InitializeComponent();
            _order = order;
            LoadData();
        }

        private void LoadData()
        {
            if (_order == null) return;

            lblMaDon.Text = $"Mã đơn: #{_order.OrderId}";
            lblNgayDat.Text = $"Ngày đặt: {_order.CreatedAt:dd/MM/yyyy HH:mm}";

            string statusDon = MapStatusToVN(_order.OrderStatus).ToUpper();
            string statusTT = (_order.PaymentStatus == "paid") ? "ĐÃ THANH TOÁN" : "CHƯA THANH TOÁN";

            lblTrangThai.Text = $"Đơn hàng: {statusDon}" +
                                $"\nThanh toán: {statusTT}";

            if (_order.OrderStatus == "completed") lblTrangThai.ForeColor = Color.Green;
            else if (_order.OrderStatus == "cancelled") lblTrangThai.ForeColor = Color.Red;
            else lblTrangThai.ForeColor = Color.Blue;

            lblKhachHang.Text = $"Khách hàng: {_order.CustomerName}";

            string sdt = !string.IsNullOrEmpty(_order.ShippingPhone) ? _order.ShippingPhone : _order.CustomerPhone;
            lblSDT.Text = $"SĐT: {sdt}";

            string addr = _order.ShippingAddressLine ?? "";
            if (!string.IsNullOrEmpty(_order.ShippingWardName) && _order.ShippingWardName != "N/A")
                addr += ", " + _order.ShippingWardName;
            if (!string.IsNullOrEmpty(_order.ShippingProvinceName) && _order.ShippingProvinceName != "N/A")
                addr += ", " + _order.ShippingProvinceName;

            lblDiaChi.Text = $"Địa chỉ: {addr}";

            StyleDataGridView();
            if (_order.Items != null)
            {
                var displayList = _order.Items.Select(i => new
                {
                    TenMon = i.ProductName ?? i.ServiceName,
                    Loai = i.ProductId != null ? "Sản phẩm" : "Dịch vụ",
                    DonGia = i.Price.ToString("N0"),
                    SoLuong = i.Quantity,
                    ThanhTien = (i.Price * i.Quantity).ToString("N0")
                }).ToList();

                dgvChiTiet.DataSource = displayList;

                if (dgvChiTiet.Columns.Count >= 5)
                {
                    dgvChiTiet.Columns[0].HeaderText = "Tên món";
                    dgvChiTiet.Columns[1].HeaderText = "Loại";
                    dgvChiTiet.Columns[2].HeaderText = "Đơn giá";
                    dgvChiTiet.Columns[3].HeaderText = "SL";
                    dgvChiTiet.Columns[4].HeaderText = "Thành tiền";
                }
            }

            lblValTienHang.Text = $"{_order.SubTotal:N0} đ";
            bool isAtCounter = addr.ToLower().Contains("tại quầy");

            if (_order.ShippingFee == 0 || isAtCounter)
                lblValShip.Text = "0 đ";
            else
                lblValShip.Text = $"{_order.ShippingFee:N0} đ";

            lblValGiamGia.Text = $"{_order.DiscountAmount:N0} đ";
            lblValTongCong.Text = $"{_order.TotalAmount:N0} đ";

            int rightMargin = 880;
            AlignLabelRight(lblValTienHang, rightMargin);
            AlignLabelRight(lblValShip, rightMargin);
            AlignLabelRight(lblValGiamGia, rightMargin);
            AlignLabelRight(lblValTongCong, rightMargin);
        }

        private void StyleDataGridView()
        {
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.MultiSelect = false;
        }

        private string MapStatusToVN(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.ToLower();
            if (s == "completed") return "Hoàn thành";
            if (s == "cancelled") return "Đã hủy";
            if (s == "shipping") return "Đang giao";
            if (s == "processing") return "Đang xử lý";
            if (s == "confirmed") return "Đã xác nhận";
            return "Chờ xác nhận";
        }

        private void AlignLabelRight(Label lbl, int rightMarginX)
        {
            lbl.Location = new Point(rightMarginX - lbl.Width, lbl.Location.Y);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}