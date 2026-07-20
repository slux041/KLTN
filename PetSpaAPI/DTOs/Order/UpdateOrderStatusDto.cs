namespace PetSpaAPI.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public string? PaymentStatus { get; set; }
        public string? OrderStatus { get; set; }
    }
}