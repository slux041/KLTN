namespace PetSpaAPI.DTOs.Cart
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
        public string? ImageUrl { get; set; }
    }
}