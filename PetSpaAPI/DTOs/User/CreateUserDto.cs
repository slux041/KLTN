namespace PetSpaAPI.DTOs.User
{
    public class CreateUserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "customer";
        public string? ImageUrl { get; set; }
    }
}