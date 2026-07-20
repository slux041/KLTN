using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GD.Services
{
    public class OrderService
    {
        public async Task<List<OrderDto>> GetOrders(string search = null, string status = null, string fromDate = null, string toDate = null)
        {
            string url = "/api/Orders";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(search))
                queryParams.Add($"search={search}");

            if (!string.IsNullOrEmpty(status) && status != "Tất cả")
                queryParams.Add($"status={status}");

            if (!string.IsNullOrEmpty(fromDate))
                queryParams.Add($"fromDate={fromDate}");

            if (!string.IsNullOrEmpty(toDate))
                queryParams.Add($"toDate={toDate}");

            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<OrderDto>>>(url);
            return response.Data ?? new List<OrderDto>();
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<OrderDto>>($"/api/Orders/{id}");
            return response.Data;
        }
        public async Task CreateOrder(CreateOrderDto order)
        {
            await ApiClient.Instance.PostAsync<ApiResponse<OrderDto>>("/api/Orders", order);
        }

        public async Task UpdateOrder(int id, UpdateOrderDto order)
        {
            await ApiClient.Instance.PutAsync<ApiResponse<OrderDto>>($"/api/Orders/{id}", order);
        }

        public async Task UpdateOrderStatus(int orderId, string newStatus, string paymentStatus = null)
        {
            string url = $"/api/Orders/{orderId}/status";

            var dto = new UpdateOrderStatusDto
            {
                OrderStatus = newStatus,
                PaymentStatus = paymentStatus
            };

            await ApiClient.Instance.PostAsync<ApiResponse<OrderDto>>(url, dto);
        }

        public async Task DeleteOrder(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Orders/{id}");
        }
    }
}