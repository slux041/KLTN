namespace PetSpaAPI.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public int ServiceId { get; set; }
        public string? PetInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; } = "09:00"; // Required: 09:00, 10:00, 11:00, 14:00, 15:00, 16:00, 17:00
        
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        
        public string PetType { get; set; } = "dog"; // dog, cat
        public string? PetBreed { get; set; }
        
        public string Source { get; set; } = "web";
    }
}