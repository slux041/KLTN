namespace PetSpaAPI.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
    }
}