using System.Collections.Generic;
using System.Threading.Tasks;
using GD.DTOs;

namespace GD.Services
{
    public class CategoryService
    {
        public async Task<List<CategoryDto>> GetCategories(string type = null, bool? isActive = null)
        {
            string url = "/api/Categories";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(type))
                queryParams.Add($"type={type}");

            if (isActive.HasValue)
                queryParams.Add($"isActive={isActive.Value}");

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<CategoryDto>>>(url);

            return response.Data ?? new List<CategoryDto>();
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<CategoryDto>>($"/api/Categories/{id}");
            return response.Data;
        }
        public async Task CreateCategory(CreateCategoryDto category)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<CategoryDto>>("/api/Categories", category);
        }

        public async Task UpdateCategory(int id, UpdateCategoryDto category)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<CategoryDto>>($"/api/Categories/{id}/update", category);
        }

        public async Task DeleteCategory(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Categories/{id}");
        }
    }
}