using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GD.Services;
using GD.DTOs;

namespace GD
{
    public partial class FrmDashboard : Form
    {
        private readonly DashboardService _dashboardService;

        public FrmDashboard()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
            this.Load += FrmDashboard_Load;
            this.btnRefresh.Click += BtnRefresh_Click;
        }

        private async void FrmDashboard_Load(object sender, EventArgs e)
        {
            await LoadDashboardData();
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDashboardData();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                var stats = await _dashboardService.GetStats();
                if (stats != null)
                {
                    lblValue1.Text = stats.TotalRevenue.ToString("N0") + "đ";
                    lblValue2.Text = stats.TotalOrders.ToString();
                    lblValue3.Text = stats.TodayAppointments.ToString();
                    lblValue4.Text = stats.LowStockProducts.ToString();
                }

                var chartData = await _dashboardService.GetRevenueChart(7);
                if (chartData != null)
                {
                    chartRevenue.Series["Revenue"].Points.Clear();
                    foreach (var item in chartData)
                    {
                        chartRevenue.Series["Revenue"].Points.AddXY(item.Date, item.Revenue);
                    }
                }

                var appointmentService = new AppointmentService();
                var allAppointments = await appointmentService.GetAppointments();
                if (allAppointments != null)
                {
                    var today = DateTime.Today;
                    var todayAppointments = allAppointments
                        .Where(a => a.AppointmentDate.Date == today)
                        .Select(a => new
                        {
                            a.AppointmentId,
                            KhachHang = a.CustomerName,
                            ThuCung = a.PetInfo,
                            DichVu = a.ServiceName,
                            Gio = a.TimeSlot,
                            TrangThai = a.Status
                        })
                        .ToList();

                    dgvAppointments.DataSource = todayAppointments;

                    if (dgvAppointments.Columns["AppointmentId"] != null) dgvAppointments.Columns["AppointmentId"].HeaderText = "ID";
                    if (dgvAppointments.Columns["KhachHang"] != null) dgvAppointments.Columns["KhachHang"].HeaderText = "Khách hàng";
                    if (dgvAppointments.Columns["ThuCung"] != null) dgvAppointments.Columns["ThuCung"].HeaderText = "Thú cưng";
                    if (dgvAppointments.Columns["DichVu"] != null) dgvAppointments.Columns["DichVu"].HeaderText = "Dịch vụ";
                    if (dgvAppointments.Columns["Gio"] != null) dgvAppointments.Columns["Gio"].HeaderText = "Giờ";
                    if (dgvAppointments.Columns["TrangThai"] != null) dgvAppointments.Columns["TrangThai"].HeaderText = "Trạng thái";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message);
            }
        }
    }
}
