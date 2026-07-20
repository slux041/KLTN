using System.Collections.Generic;
using System.Threading.Tasks;
using GD.DTOs;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace GD.Services
{
    public class ProductService
    {
        public async Task<ProductPaginationData> GetProducts(string search = null, int pageNumber = 1, int pageSize = 20)
        {
            string url = $"/api/Products?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"&search={search}";
            }

            var response = await ApiClient.Instance.GetAsync<ApiResponse<ProductPaginationData>>(url);

            return response?.Data;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<ProductDto>>($"/api/Products/{id}");
            return response.Data;
        }

        public async Task CreateProduct(CreateProductDto product, string imagePath = null)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(product.Name ?? ""), "Name");
                content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");
                content.Add(new StringContent(product.Price.ToString()), "Price");
                content.Add(new StringContent(product.Unit ?? ""), "Unit");
                content.Add(new StringContent(product.StockQuantity.ToString()), "StockQuantity");
                content.Add(new StringContent(product.Description ?? ""), "Description");
                content.Add(new StringContent(product.IsActive.ToString()), "IsActive");
                content.Add(new StringContent(product.Brand ?? ""), "Brand");

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    var fileStream = File.OpenRead(imagePath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(fileContent, "imageFile", Path.GetFileName(imagePath));
                }

                await ApiClient.Instance.PostMultipartAsync<ApiResponse<ProductDto>>("/api/Products", content);
            }
        }

        public async Task UpdateProduct(int id, UpdateProductDto product, string imagePath = null)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(product.Name ?? ""), "Name");
                content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");
                content.Add(new StringContent(product.Price.ToString()), "Price");
                content.Add(new StringContent(product.Unit ?? ""), "Unit");
                content.Add(new StringContent(product.StockQuantity.ToString()), "StockQuantity");
                content.Add(new StringContent(product.Description ?? ""), "Description");
                content.Add(new StringContent(product.IsActive.ToString()), "IsActive");
                content.Add(new StringContent(product.Brand ?? ""), "Brand");

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    var fileStream = File.OpenRead(imagePath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(fileContent, "imageFile", Path.GetFileName(imagePath));
                }

                await ApiClient.Instance.PostMultipartAsync<ApiResponse<ProductDto>>($"/api/Products/{id}/update", content);
            }
        }

        public async Task DeleteProduct(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Products/{id}");
        }
    }
}