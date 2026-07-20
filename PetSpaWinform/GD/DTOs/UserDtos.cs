using System;

namespace GD.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }

    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; } = "staff";
    }

    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
    }
}