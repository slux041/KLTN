using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string PricingMethod { get; set; } = "fixed";

        // Navigation properties
        [JsonIgnore]
        public Category? Category { get; set; }
        
        [JsonIgnore]
        public ICollection<ServicePrice>? ServicePrices { get; set; }
    }
}