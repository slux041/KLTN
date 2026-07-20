using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.User;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public UsersController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<List<UserDto>>>> GetUsers(
            [FromQuery] string? role = null,
            [FromQuery] string? status = null,
            [FromQuery] string? search = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(u => u.Status == status);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));

            var users = await query
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    Role = u.Role,
                    Status = u.Status,
                    ImageUrl = u.ImageUrl,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();

            return Ok(ResponseDto<List<UserDto>>.SuccessResponse(users));
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<UserDto>>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(ResponseDto<UserDto>.ErrorResponse("Người dùng không tồn tại"));

            var userDto = new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Role = user.Role,
                Status = user.Status,
                ImageUrl = user.ImageUrl,
                CreatedAt = user.CreatedAt
            };

            return Ok(ResponseDto<UserDto>.SuccessResponse(userDto));
        }

        // POST: api/users
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<UserDto>>> CreateUser([FromBody] CreateUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(ResponseDto<UserDto>.ErrorResponse("Email đã tồn tại"));

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                Status = "active",
                ImageUrl = dto.ImageUrl,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (user.Role == "customer")
            {
                var customer = new Customer { UserId = user.UserId };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Role = user.Role,
                Status = user.Status,
                ImageUrl = user.ImageUrl,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId },
                ResponseDto<UserDto>.SuccessResponse(userDto, "Tạo người dùng thành công"));
        }

        // POST: api/users/5/update
        [HttpPost("{id}/update")]
        public async Task<ActionResult<ResponseDto<UserDto>>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(ResponseDto<UserDto>.ErrorResponse("Người dùng không tồn tại"));

            if (!string.IsNullOrEmpty(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Phone))
                user.Phone = dto.Phone;

            if (dto.DateOfBirth.HasValue)
                user.DateOfBirth = dto.DateOfBirth;

            if (!string.IsNullOrEmpty(dto.Gender))
                user.Gender = dto.Gender;

            if (!string.IsNullOrEmpty(dto.ImageUrl))
                user.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Role = user.Role,
                Status = user.Status,
                ImageUrl = user.ImageUrl,
                CreatedAt = user.CreatedAt
            };

            return Ok(ResponseDto<UserDto>.SuccessResponse(userDto, "Cập nhật thành công"));
        }

        // POST: api/users/5/status
        [HttpPost("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> UpdateUserStatus(int id, [FromBody] string status)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Người dùng không tồn tại"));

            user.Status = status;
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse(status, "Cập nhật trạng thái thành công"));
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Người dùng không tồn tại"));

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa người dùng thành công"));
        }

        // POST: api/users/5/change-password
        [HttpPost("{id}/change-password")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Người dùng không tồn tại"));

            if (string.IsNullOrEmpty(dto.NewPassword) || dto.NewPassword.Length < 6)
                return BadRequest(ResponseDto<string>.ErrorResponse("Mật khẩu mới phải có ít nhất 6 ký tự"));

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Đổi mật khẩu thành công"));
        }
    }
}