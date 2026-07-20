namespace PetSpaAPI.DTOs.User
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ImageUrl { get; set; }
    }
}