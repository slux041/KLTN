using GD.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace GD.Services
{
    public class CustomerService
    {
        public async Task<CustomerPaginationData> GetCustomers(string search = null, int pageNumber = 1, int pageSize = 20)
        {
            string url = $"/api/Customers?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"&search={search}";
            }

            var response = await ApiClient.Instance.GetAsync<ApiResponse<CustomerPaginationData>>(url);
            return response?.Data;
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<CustomerDto>>($"/api/Customers/{id}");
            return response.Data;
        }

        public async Task CreateCustomer(CreateCustomerDto customer, string imagePath = null)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(customer.FullName ?? ""), "FullName");
                content.Add(new StringContent(customer.Email ?? ""), "Email");
                content.Add(new StringContent(customer.Phone ?? ""), "Phone");
                content.Add(new StringContent(customer.Password ?? ""), "Password");
                content.Add(new StringContent(customer.Address ?? ""), "Address");
                content.Add(new StringContent(customer.Gender ?? "other"), "Gender");

                if (customer.DateOfBirth.HasValue)
                    content.Add(new StringContent(customer.DateOfBirth.Value.ToString("yyyy-MM-dd")), "DateOfBirth");

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    var fileStream = File.OpenRead(imagePath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(fileContent, "imageFile", Path.GetFileName(imagePath));
                }

                await ApiClient.Instance.PostMultipartAsync<ApiResponse<CustomerDto>>("/api/Customers", content);
            }
        }

        public async Task UpdateCustomer(int customerId, UpdateCustomerDto customer, string imagePath = null)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(customer.FullName ?? ""), "FullName");
                content.Add(new StringContent(customer.Phone ?? ""), "Phone");
                content.Add(new StringContent(customer.Address ?? ""), "Address");
                content.Add(new StringContent(customer.Gender ?? "other"), "Gender");
                content.Add(new StringContent(customer.Status ?? "active"), "Status");

                if (customer.DateOfBirth.HasValue)
                    content.Add(new StringContent(customer.DateOfBirth.Value.ToString("yyyy-MM-dd")), "DateOfBirth");

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    var fileStream = File.OpenRead(imagePath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(fileContent, "imageFile", Path.GetFileName(imagePath));
                }

                await ApiClient.Instance.PostMultipartAsync<ApiResponse<CustomerDto>>($"/api/Customers/{customerId}/update", content);
            }
        }

        public async Task DeleteCustomer(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Customers/{id}");
        }
    }
}