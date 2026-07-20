using System.Collections.Generic;
using System.Threading.Tasks;
using GD.DTOs;

namespace GD.Services
{
    public class ServiceService
    {
        public async Task<List<ServiceDto>> GetServices(string search = null, int? categoryId = null, bool? isActive = null)
        {
            string url = "/api/Services";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(search))
                queryParams.Add($"search={search}");

            if (categoryId.HasValue)
                queryParams.Add($"categoryId={categoryId}");

            if (isActive.HasValue)
                queryParams.Add($"isActive={isActive}");

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<ServiceDto>>>(url);
            return response.Data ?? new List<ServiceDto>();
        }

        public async Task<ServiceDto> GetService(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<ServiceDto>>($"/api/Services/{id}");
            return response.Data;
        }

        public async Task CreateService(CreateServiceDto service)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<ServiceDto>>("/api/Services", service);
        }

        public async Task UpdateService(int id, UpdateServiceDto service)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<ServiceDto>>($"/api/Services/{id}/update", service);
        }

        public async Task DeleteService(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Services/{id}");
        }

        public double CalculateServicePrice(ServiceDto service, double weight, string petType)
        {
            if (service.PricingMethod == "fixed" || service.ServicePrices == null || service.ServicePrices.Count == 0)
            {
                return service.Price;
            }

            string normalizedType = petType?.ToLower().Trim();

            if (normalizedType == "mèo" || normalizedType == "meo") normalizedType = "cat";
            if (normalizedType == "chó" || normalizedType == "cho") normalizedType = "dog";

            var matchPrice = service.ServicePrices.FirstOrDefault(p =>
                p.PetType.Equals(normalizedType, System.StringComparison.OrdinalIgnoreCase) &&
                weight >= p.MinWeight && weight < p.MaxWeight);

            return matchPrice != null ? matchPrice.Price : 0;
        }
    }
}