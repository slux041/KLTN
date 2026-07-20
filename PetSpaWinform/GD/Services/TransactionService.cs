using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GD.DTOs;

namespace GD.Services
{
    public class TransactionService
    {
        public async Task<List<TransactionDto>> GetTransactions(string type = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            string url = "/api/Transactions";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(type) && type != "All")
            {
                queryParams.Add($"type={type}");
            }

            if (startDate.HasValue)
                queryParams.Add($"startDate={startDate.Value:yyyy-MM-ddTHH:mm:ss}");

            if (endDate.HasValue)
                queryParams.Add($"endDate={endDate.Value:yyyy-MM-ddTHH:mm:ss}");

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<TransactionDto>>>(url);

            return response.Data ?? new List<TransactionDto>();
        }
    }
}