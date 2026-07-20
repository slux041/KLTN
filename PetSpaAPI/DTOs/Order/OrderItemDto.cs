namespace PetSpaAPI.DTOs.Order
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}