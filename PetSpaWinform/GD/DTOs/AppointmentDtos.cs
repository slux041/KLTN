using System;

namespace GD.DTOs
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public string PetInfo { get; set; }
        public string PetType { get; set; }
        public string PetBreed { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
    }

    public class CreateAppointmentDto
    {
        public int ServiceId { get; set; }
        public string PetInfo { get; set; }
        public int? CustomerId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string PetType { get; set; }
        public string PetBreed { get; set; }
        public string Source { get; set; }
    }

    public class UpdateAppointmentStatusDto
    {
        public string Status { get; set; }
    }
    public class TimeSlotDto
    {
        public string TimeSlot { get; set; }
        public int MaxBookings { get; set; }
        public int CurrentBookings { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
    }
}