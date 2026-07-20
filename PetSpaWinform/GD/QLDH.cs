using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GD
{
    public partial class QLDH : Form
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly ServiceService _serviceService;
        private readonly CustomerService _customerService;
        private readonly AppointmentService _appointmentService;
        private int? _currentAppointmentId = null;
        private List<OrderDto> _listOrders;
        private List<ProductDto> _allProducts;
        private List<ServiceDto> _allServices;
        private List<CreateOrderItemDto> _cartItems;
        private int? _currentCustomerId = null;
        private double _cartTotal = 0;

        public QLDH()
        {
            InitializeComponent();
            _orderService = new OrderService();
            _productService = new ProductService();
            _serviceService = new ServiceService();
            _customerService = new CustomerService();
            _appointmentService = new AppointmentService();
            _cartItems = new List<CreateOrderItemDto>();

            SetupGrids();
            LoadComboBoxData();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadOrderList();
        }

        private void SetupGrids()
        {
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.ReadOnly = true;

            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.ReadOnly = true;

            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.ReadOnly = true;

            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.Columns.Add("Name", "Tên món");
            dgvGioHang.Columns.Add("Price", "Đơn giá");
            dgvGioHang.Columns.Add("Quantity", "SL");
            dgvGioHang.Columns.Add("Total", "Thành tiền");

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            dgvGioHang.Columns.Add(btnDelete);
            dgvGioHang.Columns["Name"].ReadOnly = true;
            dgvGioHang.Columns["Price"].ReadOnly = true;
            dgvGioHang.Columns["Total"].ReadOnly = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnFormClosing(e);
        }

        private void ResetFormState()
        {
            _cartItems.Clear();
            RenderCart();
            txtSDTKhach.Clear();
            lblTenKhach.Text = "Khách lẻ";
            _currentCustomerId = null;
            _currentAppointmentId = null;
            txtGiamGia.Text = "0";
        }

        private void LoadComboBoxData()
        {
            cmbTrangThaiLoc.Items.AddRange(new string[] { "Tất cả", "pending", "processing", "completed", "cancelled" });
            cmbTrangThaiLoc.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabDanhSach)
            {
                LoadOrderList();
            }
            else if (tabControlMain.SelectedTab == tabBanHang)
            {
                LoadPOSData();
            }
        }

        private async void LoadOrderList()
        {
            try
            {
                string status = cmbTrangThaiLoc.Text == "Tất cả" ? null : cmbTrangThaiLoc.Text;
                string from = dtpTuNgay.Value.ToString("yyyy-MM-dd");
                string to = dtpDenNgay.Value.ToString("yyyy-MM-dd");
                string search = txtTimKiemDon.Text.Trim();

                _listOrders = await _orderService.GetOrders(search, status, from, to);

                var viewData = _listOrders.Select(o => new
                {
                    o.OrderId,
                    o.CustomerName,
                    Total = o.TotalAmount.ToString("N0"),
                    o.OrderStatus,
                    o.PaymentStatus,
                    Date = o.CreatedAt.ToString("dd/MM HH:mm")
                }).ToList();

                dgvDonHang.DataSource = viewData;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải đơn: " + ex.Message); }
        }

        private void btnLoc_Click(object sender, EventArgs e) => LoadOrderList();
        private void txtTimKiemDon_TextChanged(object sender, EventArgs e) => LoadOrderList();

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) dgvDonHang.Rows[e.RowIndex].Selected = true;
        }

        private async void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;
            int orderId = (int)dgvDonHang.CurrentRow.Cells["OrderId"].Value;
            try
            {
                var fullOrderInfo = await _orderService.GetOrder(orderId);
                FrmChiTietDonHang frm = new FrmChiTietDonHang(fullOrderInfo);
                frm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private async void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;
            int orderId = (int)dgvDonHang.CurrentRow.Cells["OrderId"].Value;
            var order = _listOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return;

            string currentStatus = order.OrderStatus.ToLower();
            if (currentStatus == "completed" || currentStatus == "cancelled")
            {
                MessageBox.Show("Đơn hàng đã hoàn tất/hủy, không thể cập nhật!");
                return;
            }

            string nextStatus = "";
            string nextPayment = order.PaymentStatus;
            string msg = "";

            if (currentStatus == "pending") { nextStatus = "confirmed"; msg = "Xác nhận đơn hàng?"; }
            else if (currentStatus == "confirmed") { nextStatus = "shipping"; msg = "Chuyển sang đang giao/xử lý?"; }
            else if (currentStatus == "shipping" || currentStatus == "processing")
            {
                nextStatus = "completed";
                nextPayment = "paid";
                msg = "Xác nhận HOÀN THÀNH?";
            }

            if (MessageBox.Show(msg, "Cập nhật", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await _orderService.UpdateOrderStatus(orderId, nextStatus, nextPayment);
                LoadOrderList();
            }
        }

        private async void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;
            int orderId = (int)dgvDonHang.CurrentRow.Cells["OrderId"].Value;
            var order = _listOrders.FirstOrDefault(o => o.OrderId == orderId);

            if (order.OrderStatus == "completed") { MessageBox.Show("Đơn đã xong, không thể hủy!"); return; }
            if (order.PaymentStatus == "paid") { MessageBox.Show("Đơn đã trả tiền, không thể hủy!"); return; }
            if (order.OrderStatus == "cancelled") { MessageBox.Show("Đơn đã hủy rồi!"); return; }

            if (MessageBox.Show("Bạn muốn hủy đơn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await _orderService.UpdateOrderStatus(orderId, "cancelled", order.PaymentStatus);
                LoadOrderList();
            }
        }

        private async void btnInLai_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;
            int orderId = (int)dgvDonHang.CurrentRow.Cells["OrderId"].Value;
            var order = _listOrders.FirstOrDefault(o => o.OrderId == orderId);

            if (order.OrderStatus != "completed") { MessageBox.Show("Chỉ in được đơn ĐÃ HOÀN THÀNH!"); return; }

            var fullOrder = await _orderService.GetOrder(orderId);
            PrintInvoice(fullOrder);
        }

        private async Task LoadPOSData()
        {
            if (_allProducts != null && _allServices != null && _allProducts.Count > 0) return;

            try
            {
                var pData = await _productService.GetProducts(pageSize: 1000);
                _allProducts = pData.Items;
                dgvSanPham.DataSource = _allProducts.Select(p => new { p.ProductId, p.Name, Price = p.Price.ToString("N0"), Stock = p.StockQuantity }).ToList();

                var allServices = await _serviceService.GetServices(isActive: true);
                _allServices = allServices.Where(s => s.ServiceId != 5 && s.ServiceId != 6 && s.ServiceId != 7).ToList();

                dgvDichVu.DataSource = _allServices.Select(s => new { s.ServiceId, s.Name, Price = s.Price.ToString("N0") }).ToList();
            }
            catch (Exception ex)
            {
                _allProducts = null;
                _allServices = null;
                MessageBox.Show("Lỗi load POS: " + ex.Message);
            }
        }

        private void txtTimKiemSP_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiemSP.Text.ToLower();
            if (_allProducts != null && tabControlSanpham.SelectedTab == tabSP)
            {
                var filtered = _allProducts.Where(p => p.Name.ToLower().Contains(k)).Select(p => new { p.ProductId, p.Name, Price = p.Price.ToString("N0"), Stock = p.StockQuantity }).ToList();
                dgvSanPham.DataSource = filtered;
            }
            else if (_allServices != null && tabControlSanpham.SelectedTab == tabDV)
            {
                var filtered = _allServices.Where(s => s.Name.ToLower().Contains(k)).Select(s => new { s.ServiceId, s.Name, Price = s.Price.ToString("N0") }).ToList();
                dgvDichVu.DataSource = filtered;
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (tabControlSanpham.SelectedTab == tabSP)
            {
                if (dgvSanPham.CurrentRow == null) { MessageBox.Show("Chưa chọn sản phẩm!"); return; }
                int pId = (int)dgvSanPham.CurrentRow.Cells["ProductId"].Value;
                var product = _allProducts.FirstOrDefault(p => p.ProductId == pId);

                if (product.StockQuantity <= 0)
                {
                    MessageBox.Show($"Sản phẩm \"{product.Name}\" đã HẾT HÀNG!", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int qtyToAdd = (int)nudSoLuong.Value;

                var itemInCart = _cartItems.FirstOrDefault(i => i.ProductId == pId);
                int currentQty = itemInCart != null ? itemInCart.Quantity : 0;

                if (currentQty + qtyToAdd > product.StockQuantity)
                {
                    MessageBox.Show($"Không đủ hàng! Tồn kho: {product.StockQuantity}, Đang chọn: {currentQty + qtyToAdd}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddToCart(product.ProductId, null, product.Name, product.Price, qtyToAdd);
            }
            else
            {
                if (dgvDichVu.CurrentRow == null) { MessageBox.Show("Chưa chọn dịch vụ!"); return; }
                int sId = (int)dgvDichVu.CurrentRow.Cells["ServiceId"].Value;
                var service = _allServices.FirstOrDefault(s => s.ServiceId == sId);

                AddToCart(null, service.ServiceId, service.Name, service.Price, (int)nudSoLuong.Value);
            }
            nudSoLuong.Value = 1;
        }

        private void AddToCart(int? pId, int? sId, string name, double price, int quantity)
        {
            var existItem = _cartItems.FirstOrDefault(x => x.ProductId == pId && x.ServiceId == sId);
            if (existItem != null) existItem.Quantity += quantity;
            else _cartItems.Add(new CreateOrderItemDto { ProductId = pId, ServiceId = sId, Quantity = quantity, Price = price });
            RenderCart();
        }

        private void RenderCart()
        {
            dgvGioHang.Rows.Clear();
            _cartTotal = 0;
            foreach (var item in _cartItems)
            {
                string name = item.ProductId.HasValue
                    ? _allProducts.First(p => p.ProductId == item.ProductId).Name
                    : (item.ServiceId.HasValue && _allServices.Any(s => s.ServiceId == item.ServiceId)
                        ? _allServices.First(s => s.ServiceId == item.ServiceId).Name
                        : "Dịch vụ (Lịch hẹn)");
                double total = item.Quantity * item.Price;
                _cartTotal += total;
                dgvGioHang.Rows.Add(name, item.Price.ToString("N0"), item.Quantity, total.ToString("N0"), "X");
            }
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            lblTongTienHang.Text = _cartTotal.ToString("N0") + " đ";
            double.TryParse(txtGiamGia.Text, out double discount);
            double final = _cartTotal - discount;
            if (final < 0) final = 0;
            lblThanhTien.Text = final.ToString("N0") + " đ";
        }

        private void dgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (int.TryParse(dgvGioHang.Rows[e.RowIndex].Cells[2].Value.ToString(), out int newQty))
                {
                    if (newQty <= 0) { MessageBox.Show("Số lượng > 0"); RenderCart(); return; }

                    var item = _cartItems[e.RowIndex];
                    if (item.ProductId.HasValue)
                    {
                        var product = _allProducts.First(p => p.ProductId == item.ProductId);
                        if (newQty > product.StockQuantity)
                        {
                            MessageBox.Show($"Quá tồn kho! (Còn: {product.StockQuantity})");
                            item.Quantity = product.StockQuantity;
                            RenderCart();
                            return;
                        }
                    }
                    item.Quantity = newQty;
                    RenderCart();
                }
            }
        }

        private void dgvGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4) { _cartItems.RemoveAt(e.RowIndex); RenderCart(); }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e) => CalculateTotal();

        private async void txtSDTKhach_Leave(object sender, EventArgs e)
        {
            string phone = txtSDTKhach.Text.Trim();
            if (string.IsNullOrEmpty(phone)) { _currentCustomerId = null; lblTenKhach.Text = "Khách lẻ"; return; }
            var data = await _customerService.GetCustomers(search: phone);
            if (data != null && data.Items.Count > 0)
            {
                var cus = data.Items.First();
                _currentCustomerId = cus.UserId;
                lblTenKhach.Text = cus.FullName;
            }
            else { lblTenKhach.Text = "Khách mới"; _currentCustomerId = null; }
        }


        private async void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0) return;
            double.TryParse(txtGiamGia.Text, out double discount);
            double finalTotal = _cartTotal - discount;

            var newOrder = new CreateOrderDto
            {
                UserId = _currentCustomerId ?? 10,
                SubTotal = _cartTotal,
                DiscountAmount = discount,
                ShippingFee = 0,
                TotalAmount = finalTotal,
                PaymentMethod = "Tiền mặt",
                OrderStatus = "completed",
                PaymentStatus = "paid",
                Items = _cartItems,
                ShippingAddressLine = "Mua tại quầy",
            };

            try
            {
                await _orderService.CreateOrder(newOrder);

                if (_currentAppointmentId.HasValue)
                {
                    await _appointmentService.UpdateStatus(_currentAppointmentId.Value, "completed");
                    _currentAppointmentId = null;
                }

                MessageBox.Show("Thanh toán thành công!");

                var orderToPrint = new OrderDto
                {
                    OrderId = 0,
                    CustomerName = lblTenKhach.Text,
                    CreatedAt = DateTime.Now,
                    TotalAmount = finalTotal,
                    DiscountAmount = discount,
                    ShippingFee = 0,
                    Items = _cartItems.Select(i => new OrderItemDto
                    {
                        ProductName = i.ProductId.HasValue ? _allProducts.First(p => p.ProductId == i.ProductId).Name : "Dịch vụ",
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
                PrintInvoice(orderToPrint);

                this.Hide();
                ResetFormState();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        public async void AddAppointmentToCart(int aptId, int? customerId, string customerName, string customerPhone, CreateOrderItemDto item)
        {
            try
            {
                ResetFormState();
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();

                tabControlMain.SelectedTab = tabBanHang;

                await LoadPOSData();

                if (_allProducts == null || _allServices == null)
                {
                    MessageBox.Show("Không thể tải danh sách sản phẩm/dịch vụ. Vui lòng thử lại.", "Lỗi dữ liệu");
                    return;
                }

                _currentAppointmentId = aptId;
                _currentCustomerId = customerId;
                txtSDTKhach.Text = customerPhone;
                lblTenKhach.Text = customerName;

                AddToCart(null, item.ServiceId, "Dịch vụ từ lịch hẹn", item.Price, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi chuyển dữ liệu sang hóa đơn: " + ex.Message);
            }
        }

        private void PrintInvoice(OrderDto order)
        {
            PrintDocument pDoc = new PrintDocument();
            pDoc.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;

                Font fTitle = new Font("Arial", 21, FontStyle.Bold);
                Font fHeader = new Font("Arial", 15, FontStyle.Bold);
                Font fReg = new Font("Arial", 15, FontStyle.Regular);
                Font fSmall = new Font("Arial", 13, FontStyle.Italic);

                int y = 20;
                int w = e.PageBounds.Width;
                int c = w / 2;
                int margin = 40;

                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat left = new StringFormat { Alignment = StringAlignment.Near };

                string diachi = order.ShippingAddressLine ?? "";
                bool isAtCounter = diachi.ToLower().Contains("tại quầy") || order.ShippingFee == 0;

                if (isAtCounter) diachi = "Mua tại quầy";

                g.DrawString("PET SHOP 140", fTitle, Brushes.Black, c, y, center);
                y += 40;
                g.DrawString("ĐC: 140 Lê Trọng Tấn, P.Tây Thạnh, Q.Tân Phú, TP.HCM", fSmall, Brushes.Black, c, y, center);
                y += 25;
                g.DrawString("Hotline: 0917 203 062", fSmall, Brushes.Black, c, y, center);
                y += 50;

                g.DrawString("HÓA ĐƠN THANH TOÁN", fTitle, Brushes.Black, c, y, center);
                y += 45;

                g.DrawString($"Ngày: {order.CreatedAt:dd/MM/yyyy HH:mm}", fReg, Brushes.Black, margin, y);
                y += 30;
                g.DrawString($"Khách hàng: {order.CustomerName}", fReg, Brushes.Black, margin, y);
                y += 30;

                if (!isAtCounter)
                {
                    string sdt = !string.IsNullOrEmpty(order.ShippingPhone) ? order.ShippingPhone : order.CustomerPhone;
                    g.DrawString($"SĐT: {sdt}", fReg, Brushes.Black, margin, y);
                    y += 30;
                }

                if (isAtCounter)
                {
                    g.DrawString($"Địa chỉ: {diachi}", fReg, Brushes.Black, margin, y);
                    y += 45;
                }
                else
                {
                    if (!string.IsNullOrEmpty(order.ShippingWardName) && order.ShippingWardName != "N/A")
                        diachi += ", " + order.ShippingWardName;
                    if (!string.IsNullOrEmpty(order.ShippingProvinceName) && order.ShippingProvinceName != "N/A")
                        diachi += ", " + order.ShippingProvinceName;

                    RectangleF rectAddr = new RectangleF(margin, y, w - 2 * margin, 70);
                    g.DrawString($"Địa chỉ: {diachi}", fReg, Brushes.Black, rectAddr, left);
                    y += 50;
                }

                g.DrawLine(Pens.Black, margin, y, w - margin, y);
                y += 10;

                int xTT = margin;
                int xTen = margin + 60;
                int xSL = w - 350;
                int xGia = w - 260;
                int xTien = w - margin;

                g.DrawString("TT", fHeader, Brushes.Black, xTT, y);
                g.DrawString("Tên món", fHeader, Brushes.Black, xTen, y);
                g.DrawString("SL", fHeader, Brushes.Black, xSL, y);
                g.DrawString("Đơn giá", fHeader, Brushes.Black, xGia, y);
                g.DrawString("Thành tiền", fHeader, Brushes.Black, xTien, y, right);

                y += 35;
                g.DrawLine(Pens.Black, margin, y, w - margin, y);
                y += 15;

                int i = 1;
                int totalQty = 0;

                foreach (var item in order.Items)
                {
                    string name = item.ProductName ?? item.ServiceName ?? "Sản phẩm";
                    if (name.Length > 22) name = name.Substring(0, 20) + "...";

                    g.DrawString(i.ToString(), fReg, Brushes.Black, xTT, y);
                    g.DrawString(name, fReg, Brushes.Black, xTen, y);

                    g.DrawString(item.Quantity.ToString(), fReg, Brushes.Black, xSL + 10, y);

                    g.DrawString(item.Price.ToString("N0"), fReg, Brushes.Black, xGia, y);
                    g.DrawString(item.TotalPrice.ToString("N0"), fReg, Brushes.Black, xTien, y, right);

                    totalQty += item.Quantity;
                    y += 35;
                    i++;
                }

                g.DrawLine(Pens.Black, margin, y, w - margin, y);
                y += 15;

                g.DrawString($"Tổng số lượng: {totalQty}", fHeader, Brushes.Black, margin, y);
                y += 40;

                if (!isAtCounter && order.ShippingFee > 0)
                {
                    g.DrawString("Phí vận chuyển:", fReg, Brushes.Black, w - 350, y);
                    g.DrawString(order.ShippingFee.ToString("N0"), fReg, Brushes.Black, xTien, y, right);
                    y += 30;
                }

                if (order.DiscountAmount > 0)
                {
                    g.DrawString("Giảm giá:", fReg, Brushes.Black, w - 350, y);
                    g.DrawString("-" + order.DiscountAmount.ToString("N0"), fReg, Brushes.Black, xTien, y, right);
                    y += 30;
                }

                y += 10;
                g.DrawString("TỔNG TIỀN:", fTitle, Brushes.Black, margin, y);
                g.DrawString(order.TotalAmount.ToString("N0") + " đ", fTitle, Brushes.Red, xTien, y, right);

                y += 70;
                g.DrawString("Cảm ơn quý khách & Hẹn gặp lại!", fSmall, Brushes.Black, c, y, center);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pDoc;
            preview.Width = 1000;
            preview.Height = 800;
            preview.StartPosition = FormStartPosition.CenterScreen;
            preview.ShowDialog();
        }
    }
}