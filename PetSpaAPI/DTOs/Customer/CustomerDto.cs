namespace PetSpaAPI.DTOs.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalPets { get; set; } 
        public int TotalAppointments { get; set; }
    }
}