using PetSpaAPI.DTOs.Order;

public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        
        public int? ShippingAddressId { get; set; }
        public string? ShippingFullName { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingAddressLine { get; set; }
        public string? ShippingProvinceName { get; set; }
        public string? ShippingWardName { get; set; }
        public string ShippingFullAddress => !string.IsNullOrEmpty(ShippingAddressLine) 
            ? $"{ShippingAddressLine}, {ShippingWardName}, {ShippingProvinceName}"
            : string.Empty;
        
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public int? PromotionId { get; set; }
        public string? PromotionCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        
        public string? Note { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }