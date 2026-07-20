namespace PetSpaAPI.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int PendingOrders { get; set; }
        public int TodayAppointments { get; set; }
        public int LowStockProducts { get; set; }
    }
}