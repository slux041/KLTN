namespace PetSpaAPI.DTOs.Appointment
{
    public class TimeSlotDto
    {
        public string TimeSlot { get; set; } = string.Empty;
        public int MaxBookings { get; set; }
        public int CurrentBookings { get; set; }
        public bool IsAvailable => CurrentBookings < MaxBookings;
        public bool IsActive { get; set; }
    }
}