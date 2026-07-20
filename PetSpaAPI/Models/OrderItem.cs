using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Order? Order { get; set; }
        
        [JsonIgnore]
        public Product? Product { get; set; }
        
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}