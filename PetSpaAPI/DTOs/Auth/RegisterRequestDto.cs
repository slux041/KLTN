using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(20)]
        public string? Phone { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Address { get; set; }
    }
}