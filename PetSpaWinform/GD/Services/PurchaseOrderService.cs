using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD.Services
{
    public class PurchaseOrderService
    {
        public async Task CreatePurchaseOrder(CreatePurchaseOrderDto dto)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<object>>("/api/PurchaseOrders", dto);
        }

        public async Task<List<PurchaseOrderDto>> GetPurchaseOrders()
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<PurchaseOrderDto>>>("/api/PurchaseOrders");
            return response.Data;
        }
    }
}