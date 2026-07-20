using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoPaymentController : ControllerBase
    {
        private readonly PetSpaDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MomoPaymentController(PetSpaDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentRequest request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId);

            if (order == null) return NotFound(new { message = "Không tìm thấy đơn hàng" });
            if (order.TotalAmount <= 0) return BadRequest(new { message = "Số tiền không hợp lệ" });

            string partnerCode = _configuration["MomoSettings:PartnerCode"];
            string accessKey = _configuration["MomoSettings:AccessKey"];
            string secretKey = _configuration["MomoSettings:SecretKey"];
            string apiUrl = _configuration["MomoSettings:ApiUrl"];
            string redirectUrl = _configuration["MomoSettings:RedirectUrl"];
            string ipnUrl = _configuration["MomoSettings:IpnUrl"];
            string requestType = "payWithATM";
            string amount = ((long)order.TotalAmount).ToString();
            string orderIdString = order.OrderId.ToString() + "_" + DateTime.Now.Ticks.ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";
            string orderInfo = "Thanh toan don hang #" + orderIdString;

            string rawHash = "accessKey=" + accessKey +
                             "&amount=" + amount +
                             "&extraData=" + extraData +
                             "&ipnUrl=" + ipnUrl +
                             "&orderId=" + orderIdString +
                             "&orderInfo=" + orderInfo +
                             "&partnerCode=" + partnerCode +
                             "&redirectUrl=" + redirectUrl +
                             "&requestId=" + requestId +
                             "&requestType=" + requestType;

            string signature = ComputeHmacSha256(rawHash, secretKey);

            var message = new
            {
                partnerCode = partnerCode,
                partnerName = "PetSpa Payment",
                storeId = "MomoTestStore",
                requestId = requestId,
                amount = amount,
                orderId = orderIdString,
                orderInfo = orderInfo,
                redirectUrl = redirectUrl,
                ipnUrl = ipnUrl,
                lang = "vi",
                extraData = extraData,
                requestType = requestType,
                signature = signature
            };

            var client = _httpClientFactory.CreateClient();
            var jsonContent = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var response = await client.PostAsync(apiUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

            if (jsonResponse.resultCode != null && jsonResponse.resultCode == 0)
            {
                return Ok(new { payUrl = jsonResponse.payUrl.ToString() });
            }

            return BadRequest(new { message = "Lỗi MoMo", errorContent = responseContent });
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] MomoResultRequest request)
        {
            if (request.ResultCode == "0")
            {
                string[] rawId = request.OrderId.Split('_');
                string originalId = rawId[0];

                if (int.TryParse(originalId, out int dbOrderId))
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == dbOrderId);
                    if (order != null)
                    {
                        order.PaymentStatus = "paid";
                        order.TransId = request.TransId;

                        await _context.SaveChangesAsync();
                        return Ok(new { message = "Đã cập nhật đơn hàng thành công" });
                    }
                }
            }
            return BadRequest(new { message = "Giao dịch thất bại" });
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hashBytes;
            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public class PaymentRequest
    {
        public int OrderId { get; set; }
    }

    public class MomoResultRequest
    {
        public string OrderId { get; set; }
        public string ResultCode { get; set; }
        public string TransId { get; set; }
        public string Message { get; set; }
    }
}