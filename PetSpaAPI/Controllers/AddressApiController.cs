using Microsoft.AspNetCore.Mvc;
using PetSpaAPI.DTOs.Address;
using PetSpaAPI.DTOs.Common;
using System.Text.Json;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AddressApiController> _logger;

        public AddressApiController(IHttpClientFactory httpClientFactory, ILogger<AddressApiController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/addressapi/provinces - Lấy 34 tỉnh thành
        [HttpGet("provinces")]
        public async Task<ActionResult<ResponseDto<List<ProvinceDto>>>> GetProvinces()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://esgoo.net/api-tinhthanh/1/0.htm");

                if (!response.IsSuccessStatusCode)
                    return StatusCode(500, ResponseDto<List<ProvinceDto>>.ErrorResponse("Không thể kết nối đến API địa chỉ"));

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<EsgooApiResponse<ProvinceDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse == null || apiResponse.Error != 0)
                    return StatusCode(500, ResponseDto<List<ProvinceDto>>.ErrorResponse("Lỗi khi tải danh sách 34 tỉnh thành"));

                return Ok(ResponseDto<List<ProvinceDto>>.SuccessResponse(apiResponse.Data, $"Lấy thành công {apiResponse.Data.Count} tỉnh thành"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching provinces");
                return StatusCode(500, ResponseDto<List<ProvinceDto>>.ErrorResponse("Lỗi hệ thống khi tải danh sách tỉnh thành"));
            }
        }

        // GET: api/addressapi/wards/01 - Lấy xã/phường trực thuộc tỉnh
        [HttpGet("wards/{provinceId}")]
        public async Task<ActionResult<ResponseDto<List<WardDto>>>> GetWards(string provinceId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"https://esgoo.net/api-tinhthanh/2/{provinceId}.htm");

                if (!response.IsSuccessStatusCode)
                    return StatusCode(500, ResponseDto<List<WardDto>>.ErrorResponse("Không thể kết nối đến API địa chỉ"));

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<EsgooApiResponse<WardDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse == null || apiResponse.Error != 0)
                    return StatusCode(500, ResponseDto<List<WardDto>>.ErrorResponse("Lỗi khi tải danh sách xã phường"));

                return Ok(ResponseDto<List<WardDto>>.SuccessResponse(apiResponse.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching wards for province {ProvinceId}", provinceId);
                return StatusCode(500, ResponseDto<List<WardDto>>.ErrorResponse("Lỗi hệ thống khi tải danh sách xã phường"));
            }
        }

        // GET: api/addressapi/full/{provinceId}/{wardId}
        [HttpGet("full/{provinceId}/{wardId}")]
        public async Task<ActionResult<ResponseDto<object>>> GetFullAddress(string provinceId, string wardId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Get Province (34 tỉnh thành)
                var provinceResponse = await client.GetAsync("https://esgoo.net/api-tinhthanh/1/0.htm");
                var provinceContent = await provinceResponse.Content.ReadAsStringAsync();
                var provinceData = JsonSerializer.Deserialize<EsgooApiResponse<ProvinceDto>>(provinceContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var province = provinceData?.Data.FirstOrDefault(p => p.Id == provinceId);

                // Get Ward (Xã/Phường trực thuộc tỉnh)
                var wardResponse = await client.GetAsync($"https://esgoo.net/api-tinhthanh/2/{provinceId}.htm");
                var wardContent = await wardResponse.Content.ReadAsStringAsync();
                var wardData = JsonSerializer.Deserialize<EsgooApiResponse<WardDto>>(wardContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var ward = wardData?.Data.FirstOrDefault(w => w.Id == wardId);

                var result = new
                {
                    Province = province,
                    Ward = ward,
                    FullAddress = ward != null && province != null 
                        ? $"{ward.Name}, {province.Name}" 
                        : string.Empty
                };

                return Ok(ResponseDto<object>.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching full address");
                return StatusCode(500, ResponseDto<object>.ErrorResponse("Lỗi hệ thống khi tải thông tin địa chỉ"));
            }
        }
    }
}