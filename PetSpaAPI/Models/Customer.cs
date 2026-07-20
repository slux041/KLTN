using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? Address { get; set; }

        // Navigation properties
        [JsonIgnore]
        public User? User { get; set; }
        
        [JsonIgnore]
        public ICollection<Pet>? Pets { get; set; }
        
        [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}