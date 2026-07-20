using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD.Services
{
    public class PromotionService
    {
        public async Task<List<PromotionDto>> GetPromotions(string search = null, string status = null)
        {
            string url = "/api/Promotions";
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={search}");
            if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={status}");

            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<PromotionDto>>>(url);
            return response.Data;
        }

        public async Task<PromotionDto> GetPromotion(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<PromotionDto>>($"/api/Promotions/{id}");
            return response.Data;
        }

        public async Task CreatePromotion(CreatePromotionDto promotion)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<PromotionDto>>("/api/Promotions", promotion);
        }

        public async Task UpdatePromotion(int id, UpdatePromotionDto promotion)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<PromotionDto>>($"/api/Promotions/{id}/update", promotion);
        }

        public async Task DeletePromotion(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Promotions/{id}");
        }
    }
}