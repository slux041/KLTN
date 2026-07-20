using System.ComponentModel.DataAnnotations;
namespace PetSpaAPI.Models
{
    public class TimeSlotConfig
    {
        [Key]
        public int SlotId { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public int MaxBookings { get; set; } = 3;
        public bool IsActive { get; set; } = true;
    }
}