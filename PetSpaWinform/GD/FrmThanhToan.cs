using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GD
{
    public partial class FrmThanhToan : Form
    {
        private int _apptId;
        private AppointmentDto _appt;
        private ServiceDto _mainService;
        private List<ServiceDto> _allServices;
        private List<ProductDto> _products;
        private List<OrderItemDto> _cartProducts = new List<OrderItemDto>();
        private decimal _mainServicePrice = 0;

        private readonly AppointmentService _apptService = new AppointmentService();
        private readonly ServiceService _serviceService = new ServiceService();
        private readonly ProductService _productService = new ProductService();
        private readonly OrderService _orderService = new OrderService();

        public FrmThanhToan(int appointmentId)
        {
            InitializeComponent();
            _apptId = appointmentId;
            this.Load += FrmThanhToan_Load;
        }

        private async void FrmThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                _appt = await _apptService.GetAppointment(_apptId);
                lblKhachHang.Text = _appt.CustomerName;
                lblDichVuChinh.Text = _appt.ServiceName;

                _mainService = await _serviceService.GetService(_appt.ServiceId);

                _allServices = await _serviceService.GetServices();
                var subServices = _allServices.Where(s => s.ServiceId != 5 && s.ServiceId != 6 && s.ServiceId != 7).ToList();
                clbDichVuPhu.DataSource = subServices;
                clbDichVuPhu.DisplayMember = "Name";

                var pData = await _productService.GetProducts(pageSize: 1000);
                _products = pData.Items;
                cmbSanPham.DataSource = _products;
                cmbSanPham.DisplayMember = "Name";
                cmbSanPham.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load dữ liệu: " + ex.Message); }
        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtCanNang.Text, out decimal weight))
            {
                _mainServicePrice = 0; lblGiaDVChinh.Text = "0"; UpdateTotal(); return;
            }

            if (_mainService.PricingMethod == "fixed")
            {
                _mainServicePrice = (decimal)_mainService.Price;
            }
            else if (_mainService.ServicePrices != null)
            {
                string petType = _appt.PetType;

                if (string.IsNullOrEmpty(petType)) petType = "dog";

                var priceOpt = _mainService.ServicePrices.FirstOrDefault(p =>
                    p.PetType == petType && (double)weight >= p.MinWeight && (double)weight < p.MaxWeight);

                if (priceOpt != null) _mainServicePrice = (decimal)priceOpt.Price;
                else _mainServicePrice = 0;
            }

            lblGiaDVChinh.Text = _mainServicePrice.ToString("N0") + " đ";
            UpdateTotal();
        }

        private void clbDichVuPhu_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate { UpdateTotal(); });
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            var sp = (ProductDto)cmbSanPham.SelectedItem;
            if (sp == null) return;

            _cartProducts.Add(new OrderItemDto { ProductId = sp.ProductId, Price = sp.Price, Quantity = 1 });
            lstCart.Items.Add($"{sp.Name} - {sp.Price:N0} đ");
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = _mainServicePrice;
            foreach (ServiceDto s in clbDichVuPhu.CheckedItems) total += (decimal)s.Price;
            foreach (var p in _cartProducts) total += (decimal)p.Price * p.Quantity;
            lblTongTien.Text = total.ToString("N0") + " VNĐ";
        }

        private async void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_mainServicePrice == 0 && _mainService.PricingMethod != "fixed")
            {
                MessageBox.Show("Vui lòng kiểm tra lại cân nặng!"); return;
            }

            var items = new List<CreateOrderItemDto>();
            items.Add(new CreateOrderItemDto { ServiceId = _appt.ServiceId, Quantity = 1 });
            foreach (ServiceDto s in clbDichVuPhu.CheckedItems) items.Add(new CreateOrderItemDto { ServiceId = s.ServiceId, Quantity = 1 });
            foreach (var p in _cartProducts) items.Add(new CreateOrderItemDto { ProductId = p.ProductId, Quantity = p.Quantity });

            var order = new CreateOrderDto
            {
                UserId = 1,
                Items = items,
                Note = $"Lịch hẹn #{_apptId} - {_appt.PetType} {txtCanNang.Text}kg"
            };

            try
            {
                await _orderService.CreateOrder(order);
                MessageBox.Show("Thanh toán thành công!");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thanh toán: " + ex.Message); }
        }
    }
}