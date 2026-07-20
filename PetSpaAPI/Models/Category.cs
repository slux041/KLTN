using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
        
        [JsonIgnore]
        public ICollection<Service>? Services { get; set; }
    }
}