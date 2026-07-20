namespace PetSpaAPI.DTOs.Dashboard
{
    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}