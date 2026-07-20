using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GD.Services
{
    public class UserService
    {
        public async Task<List<UserDto>> GetEmployees(string search = null)
        {
            string query = !string.IsNullOrEmpty(search) ? $"&search={search}" : "";

            var taskAdmin = ApiClient.Instance.GetAsync<ApiResponse<List<UserDto>>>($"/api/Users?role=admin{query}");
            var taskStaff = ApiClient.Instance.GetAsync<ApiResponse<List<UserDto>>>($"/api/Users?role=staff{query}");

            await Task.WhenAll(taskAdmin, taskStaff);

            var admins = taskAdmin.Result.Data ?? new List<UserDto>();
            var staffs = taskStaff.Result.Data ?? new List<UserDto>();

            var allEmployees = new List<UserDto>();
            allEmployees.AddRange(admins);
            allEmployees.AddRange(staffs);

            return allEmployees;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<UserDto>>($"/api/Users/{id}");
            return response.Data;
        }

        public async Task CreateUser(CreateUserDto user)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<UserDto>>("/api/Users", user);
        }

        public async Task UpdateUser(int id, UpdateUserDto user)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<UserDto>>($"/api/Users/{id}/update", user);
        }

        public async Task DeleteUser(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Users/{id}");
        }

        public async Task ChangeStatus(int id, string status)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<string>>($"/api/Users/{id}/status", status);
        }

        public async Task ResetPassword(int id, string newPassword)
        {
            var dto = new ChangePasswordDto { NewPassword = newPassword };
            await ApiClient.Instance.PostAsync<ApiResponse<string>>($"/api/Users/{id}/change-password", dto);
        }
    }
}