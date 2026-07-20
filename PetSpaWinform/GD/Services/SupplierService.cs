using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD.Services
{
    public class SupplierService
    {
        public async Task<List<SupplierDto>> GetSuppliers(string search = null)
        {
            string url = "/api/Suppliers";
            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<SupplierDto>>>(url);
            return response.Data;
        }

        public async Task<SupplierDto> GetSupplier(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<SupplierDto>>($"/api/Suppliers/{id}");
            return response.Data;
        }

        public async Task CreateSupplier(CreateSupplierDto supplier)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<SupplierDto>>("/api/Suppliers", supplier);
        }

        public async Task UpdateSupplier(int id, UpdateSupplierDto supplier)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<SupplierDto>>($"/api/Suppliers/{id}/update", supplier);
        }

        public async Task DeleteSupplier(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Suppliers/{id}");
        }
    }
}
