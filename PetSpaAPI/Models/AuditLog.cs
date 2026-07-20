using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public User? User { get; set; }
    }
}