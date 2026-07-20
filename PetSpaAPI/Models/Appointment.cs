using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int? CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string? PetInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; } = "09:00";
        public string Status { get; set; } = "pending";
        
        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? GuestAddress { get; set; }
        
        public string PetType { get; set; } = "dog";
        public string? PetBreed { get; set; }
        
        public string Source { get; set; } = "web";

        [JsonIgnore]
        public Customer? Customer { get; set; }
        
        [JsonIgnore]
        public Service? Service { get; set; }
    }
}