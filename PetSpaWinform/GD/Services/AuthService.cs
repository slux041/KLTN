using System.Threading.Tasks;
using GD.DTOs;

namespace GD.Services
{
    public class AuthService
    {
        public async Task<LoginResponseDto> Login(string email, string password)
        {
            var request = new LoginRequestDto
            {
                Email = email,
                Password = password
            };

            var response = await ApiClient.Instance.PostAsync<ApiResponse<LoginResponseDto>>("/api/Auth/login", request);
            
            if (response.Success && response.Data != null)
            {
                ApiClient.Instance.SetAuthToken(response.Data.Token);
                return response.Data;
            }
            
            throw new System.Exception(response.Message ?? "Login failed");
        }
    }
}
