using System;

namespace GD.DTOs
{
    public class DashboardStatsDto
    {
        public double TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int PendingOrders { get; set; }
        public int TodayAppointments { get; set; }
        public int LowStockProducts { get; set; }
    }
    public class RevenueChartDto
    {
        public string Date { get; set; }
        public double Revenue { get; set; }
        public int OrderCount { get; set; }
    }

    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalSold { get; set; }
        public double TotalRevenue { get; set; }
    }

    public class TopServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int TotalBookings { get; set; }
        public double TotalRevenue { get; set; }
    }

    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
