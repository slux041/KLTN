using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // dog, cat, etc.
        public string? Breed { get; set; }
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation property
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}