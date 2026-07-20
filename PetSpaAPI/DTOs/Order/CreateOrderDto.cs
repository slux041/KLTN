namespace PetSpaAPI.DTOs.Order
{
    public class CreateOrderDto
    {
        public string PaymentMethod { get; set; } = "cod"; // cod, bank
        
        public int? ShippingAddressId { get; set; }
        
        public string? ShippingFullName { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingAddressLine { get; set; }
        public string? ShippingProvinceId { get; set; }
        public string? ShippingProvinceName { get; set; }
        public string? ShippingWardId { get; set; }
        public string? ShippingWardName { get; set; }
        
        public string? PromotionCode { get; set; }
        public string? Note { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
        public string? PaymentStatus { get; set; }
        public double DiscountAmount { get; set; }
        public string? OrderStatus { get; set; }
        public int? UserId { get; set; }
    }
}