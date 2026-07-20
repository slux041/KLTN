using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.Models
{
    public class CustomerAddress
    {
        [Key]
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        
        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;
        
        public string WardId { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        
        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}