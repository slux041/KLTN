using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int Quantity { get; set; } = 1;

        // Navigation properties
        [JsonIgnore]
        public User? User { get; set; }
        
        [JsonIgnore]
        public Product? Product { get; set; }
        
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}