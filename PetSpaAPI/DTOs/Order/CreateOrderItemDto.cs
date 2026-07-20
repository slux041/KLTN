namespace PetSpaAPI.DTOs.Order
{
    public class CreateOrderItemDto
    {
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}