using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string Role { get; set; } = "customer";
        public string Status { get; set; } = "active";
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}