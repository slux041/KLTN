using GD.DTOs;
using GD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GD
{
    public partial class FrmThongKe : Form
    {
        private readonly DashboardService _dashboardService;
        private readonly TransactionService _transactionService;

        public FrmThongKe()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
            _transactionService = new TransactionService();
            LoadAllData();
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await LoadAllData();
        }

        private async Task LoadAllData()
        {
            try
            {
                var stats = await _dashboardService.GetStats();
                lblDoanhThu.Text = $"Tổng thu: {stats.TotalRevenue:N0} đ";

                var purchases = await _transactionService.GetTransactions(type: "purchase");
                double totalExpense = purchases.Sum(x => x.Total);
                lblChiPhi.Text = $"Tổng chi: {totalExpense:N0} đ";

                double profit = stats.TotalRevenue - totalExpense;
                lblLoiNhuan.Text = $"Lợi nhuận: {profit:N0} đ";
                lblLoiNhuan.ForeColor = profit >= 0 ? System.Drawing.Color.Blue : System.Drawing.Color.Red;

                var chartData = await _dashboardService.GetRevenueChart(7);
                DrawChart(chartData);

                var topProducts = await _dashboardService.GetTopProducts(5);
                dgvTopProduct.DataSource = topProducts.Select(x => new {
                    TenSP = x.ProductName,
                    DaBan = x.TotalSold,
                    DoanhThu = x.TotalRevenue.ToString("N0")
                }).ToList();

                dgvTopProduct.Columns[0].HeaderText = "Tên Sản Phẩm";
                dgvTopProduct.Columns[1].HeaderText = "Đã Bán";
                dgvTopProduct.Columns[2].HeaderText = "Doanh Thu";

                var topServices = await _dashboardService.GetTopServices(5);
                dgvTopService.DataSource = topServices.Select(x => new {
                    TenDV = x.ServiceName,
                    LuotDat = x.TotalBookings,
                    DoanhThu = x.TotalRevenue.ToString("N0")
                }).ToList();

                dgvTopService.Columns[0].HeaderText = "Tên Dịch Vụ";
                dgvTopService.Columns[1].HeaderText = "Lượt Đặt";
                dgvTopService.Columns[2].HeaderText = "Doanh Thu";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải báo cáo: " + ex.Message); }
        }

        private void DrawChart(List<RevenueChartDto> data)
        {
            chartRevenue.Series["DoanhThu"].Points.Clear();
            var sortedData = data.OrderBy(x =>
            {
                if (DateTime.TryParse(x.Date, out DateTime dtFull)) return dtFull;
                if (DateTime.TryParseExact(x.Date + "/" + DateTime.Now.Year, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dtShort))
                    return dtShort;
                return DateTime.MinValue;
            }).ToList();

            foreach (var item in sortedData)
            {
                if (DateTime.TryParse(item.Date, out DateTime dt))
                    chartRevenue.Series["DoanhThu"].Points.AddXY(dt.ToString("dd/MM"), item.Revenue);
                else
                    chartRevenue.Series["DoanhThu"].Points.AddXY(item.Date, item.Revenue);
            }

            chartRevenue.Series["DoanhThu"].Name = "Doanh Thu (VNĐ)";
            chartRevenue.ChartAreas[0].AxisX.Title = "Ngày";
            chartRevenue.ChartAreas[0].AxisY.Title = "Tiền";
            chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
        }
    }
}