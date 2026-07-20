using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class ServicePrice
    {
        public int PriceId { get; set; }
        public int ServiceId { get; set; }
        public string PetType { get; set; } = string.Empty;
        public decimal MinWeight { get; set; } = 0;
        public decimal MaxWeight { get; set; } = 999;
        public decimal Price { get; set; }

        // Navigation property
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}
