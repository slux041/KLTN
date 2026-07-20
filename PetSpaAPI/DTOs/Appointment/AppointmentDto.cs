namespace PetSpaAPI.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal ServicePrice { get; set; }
        public string? PetInfo { get; set; }
        public string? PetType { get; set; }
        public string? PetBreed { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}