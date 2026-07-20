using PetSpaAPI.DTOs.Auth;

namespace PetSpaAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
        Task<LoginResponseDto?> RegisterAsync(RegisterRequestDto dto);
        string GenerateJwtToken(int userId, string email, string role);
    }
}