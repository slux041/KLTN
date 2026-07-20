using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.DTOs.Auth;
using PetSpaAPI.DTOs.Common;
using PetSpaAPI.Services;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly PetSpaDbContext _context;

        public AuthController(IAuthService authService, PetSpaDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseDto<LoginResponseDto>.ErrorResponse("Dữ liệu không hợp lệ"));

            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(ResponseDto<LoginResponseDto>.ErrorResponse("Email hoặc mật khẩu không đúng"));

            return Ok(ResponseDto<LoginResponseDto>.SuccessResponse(result, "Đăng nhập thành công"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Register([FromBody] RegisterRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseDto<LoginResponseDto>.ErrorResponse("Dữ liệu không hợp lệ"));

            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return BadRequest(ResponseDto<LoginResponseDto>.ErrorResponse("Email đã tồn tại"));

            return Ok(ResponseDto<LoginResponseDto>.SuccessResponse(result, "Đăng ký thành công"));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(ResponseDto<LoginResponseDto>.ErrorResponse("Không tìm thấy thông tin người dùng"));

            var userId = int.Parse(userIdClaim);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || user.Status != "active")
                return NotFound(ResponseDto<LoginResponseDto>.ErrorResponse("Người dùng không tồn tại"));

            int? customerId = null;
            if (user.Role == "customer")
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                customerId = customer?.CustomerId;
            }

            var profile = new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                ImageUrl = user.ImageUrl,
                CustomerId = customerId,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender
            };

            return Ok(ResponseDto<LoginResponseDto>.SuccessResponse(profile));
        }
    }
}