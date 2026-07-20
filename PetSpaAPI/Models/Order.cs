using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        
        public int? ShippingAddressId { get; set; }
        public string? ShippingFullName { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingAddressLine { get; set; }
        public string? ShippingProvinceId { get; set; }
        public string? ShippingProvinceName { get; set; }
        public string? ShippingWardId { get; set; }
        public string? ShippingWardName { get; set; }
        
        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; } = 25000;
        public int? PromotionId { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        
        public string? Note { get; set; }
        public string PaymentMethod { get; set; } = "cod";
        public string PaymentStatus { get; set; } = "pending";
        public string OrderStatus { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? TransId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public User? User { get; set; }
        
        [JsonIgnore]
        public ICollection<OrderItem>? Items { get; set; }
    }
}