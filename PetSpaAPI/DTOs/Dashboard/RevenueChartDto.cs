namespace PetSpaAPI.DTOs.Dashboard
{
    public class RevenueChartDto
    {
        public string Date { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
    }
}