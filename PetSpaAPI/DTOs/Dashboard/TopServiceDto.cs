namespace PetSpaAPI.DTOs.Dashboard
{
    public class TopServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}