using System.Collections.Generic;
using System.Threading.Tasks;
using GD.DTOs;

namespace GD.Services
{
    public class DashboardService
    {
        public async Task<DashboardStatsDto> GetStats()
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<DashboardStatsDto>>("/api/Dashboard/stats");
            return response.Data;
        }
        public async Task<List<RevenueChartDto>> GetRevenueChart(int days = 7)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<RevenueChartDto>>>($"/api/Dashboard/revenue-chart?days={days}");
            return response.Data;
        }

        public async Task<List<TopProductDto>> GetTopProducts(int limit = 5)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<TopProductDto>>>($"/api/Dashboard/top-products?limit={limit}");
            return response.Data;
        }

        public async Task<List<TopServiceDto>> GetTopServices(int limit = 5)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<TopServiceDto>>>($"/api/Dashboard/top-services?limit={limit}");
            return response.Data;
        }

        public async Task<List<RecentOrderDto>> GetRecentOrders(int limit = 10)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<RecentOrderDto>>>($"/api/Dashboard/recent-orders?limit={limit}");
            return response.Data;
        }
    }
}
