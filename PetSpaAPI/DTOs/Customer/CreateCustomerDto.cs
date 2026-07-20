using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string? Phone { get; set; }
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        public string? Gender { get; set; } = "other";
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}