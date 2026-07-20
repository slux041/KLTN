using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.DTOs.Common;
using PetSpaAPI.DTOs.Customer;
using PetSpaAPI.Models;
using System.Security.Claims;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly PetSpaDbContext _context;
        private readonly IConfiguration _configuration;

        public CustomersController(PetSpaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GetFullImageUrl(string? imageName)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            if (string.IsNullOrEmpty(imageName)) return $"{baseUrl}/images/users/default.png";
            return $"{baseUrl}/images/users/{imageName}";
        }

        private CustomerDto MapToCustomerDto(Customer c)
        {
            return new CustomerDto
            {
                CustomerId = c.CustomerId,
                UserId = c.UserId,
                FullName = c.User!.FullName,
                Email = c.User.Email,
                Phone = c.User.Phone,
                ImageUrl = GetFullImageUrl(c.User.ImageUrl),
                DateOfBirth = c.User.DateOfBirth,
                Gender = c.User.Gender,
                Status = c.User.Status,
                CreatedAt = c.User.CreatedAt,
                Address = c.Address,
                TotalPets = c.Pets != null ? c.Pets.Count : 0,
                TotalAppointments = c.Appointments != null ? c.Appointments.Count : 0
            };
        }

        [HttpGet]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<PaginationDto<CustomerDto>>>> GetCustomers(
            [FromQuery] string? search = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = _context.Customers
                .Include(c => c.User)
                .Include(c => c.Pets)
                .Include(c => c.Appointments)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => 
                    c.User!.FullName.Contains(search) ||
                    c.User.Email.Contains(search) ||
                    (c.User.Phone != null && c.User.Phone.Contains(search)));
            }

            var totalCount = await query.CountAsync();

            var customers = await query
                .OrderByDescending(c => c.User!.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var customerDtos = customers.Select(MapToCustomerDto).ToList();

            var result = new PaginationDto<CustomerDto>
            {
                Items = customerDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(ResponseDto<PaginationDto<CustomerDto>>.SuccessResponse(result));
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Pets)
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return NotFound(ResponseDto<CustomerDto>.ErrorResponse("Khách hàng không tồn tại"));

            return Ok(ResponseDto<CustomerDto>.SuccessResponse(MapToCustomerDto(customer)));
        }

        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> CreateCustomer([FromForm] CreateCustomerDto dto, IFormFile? imageFile)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(ResponseDto<CustomerDto>.ErrorResponse("Email này đã được sử dụng!"));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string? fileName = null;
                if (imageFile != null && imageFile.Length > 0)
                {
                    var rootPath = _configuration["UploadPath"];
                    var folderPath = Path.Combine(rootPath, "users");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    
                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                    using (var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password); 

                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    PasswordHash = passwordHash,
                    Role = "customer",
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    ImageUrl = fileName,
                    Status = "active",
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var customer = new Customer
                {
                    UserId = user.UserId,
                    Address = dto.Address
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var newCustomer = await _context.Customers
                    .Include(c => c.User).Include(c => c.Pets).Include(c => c.Appointments)
                    .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

                return Ok(ResponseDto<CustomerDto>.SuccessResponse(MapToCustomerDto(newCustomer!), "Thêm khách hàng thành công"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ResponseDto<CustomerDto>.ErrorResponse($"Lỗi hệ thống: {ex.Message}"));
            }
        }

        [HttpPost("{id}/update")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> UpdateCustomer(int id, [FromForm] UpdateCustomerDto dto, IFormFile? imageFile)
        {
            var customer = await _context.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null) return NotFound(ResponseDto<CustomerDto>.ErrorResponse("Khách hàng không tồn tại"));

            var user = customer.User!;

            if (imageFile != null && imageFile.Length > 0)
            {
                var rootPath = _configuration["UploadPath"];
                var folderPath = Path.Combine(rootPath, "users");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldPath = Path.Combine(folderPath, user.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                using (var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                user.ImageUrl = fileName;
            }

            if (dto.FullName != null) user.FullName = dto.FullName;
            if (dto.Phone != null) user.Phone = dto.Phone;
            if (dto.Gender != null) user.Gender = dto.Gender;
            if (dto.DateOfBirth.HasValue) user.DateOfBirth = dto.DateOfBirth;
            if (dto.Status != null) user.Status = dto.Status;
            
            if (dto.Address != null) customer.Address = dto.Address;

            await _context.SaveChangesAsync();

            var updatedCustomer = await _context.Customers
                .Include(c => c.User).Include(c => c.Pets).Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            return Ok(ResponseDto<CustomerDto>.SuccessResponse(MapToCustomerDto(updatedCustomer!), "Cập nhật thành công"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null) return NotFound(ResponseDto<string>.ErrorResponse("Khách hàng không tồn tại"));

            var user = customer.User!;

            bool hasOrders = await _context.Orders.AnyAsync(o => o.UserId == user.UserId);
            bool hasAppointments = await _context.Appointments.AnyAsync(a => a.CustomerId == customer.CustomerId);

            if (hasOrders || hasAppointments)
            {
                user.Status = "inactive";
                await _context.SaveChangesAsync();
                return Ok(ResponseDto<string>.SuccessResponse("", "Khách hàng đã có giao dịch. Đã chuyển trạng thái sang 'Ngưng hoạt động' để bảo toàn dữ liệu."));
            }

            try
            {
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var rootPath = _configuration["UploadPath"];
                    var oldPath = Path.Combine(rootPath, "users", user.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                _context.Customers.Remove(customer);
                _context.Users.Remove(user);
                
                await _context.SaveChangesAsync();

                return Ok(ResponseDto<string>.SuccessResponse("", "Đã xóa vĩnh viễn khách hàng."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto<string>.ErrorResponse($"Lỗi khi xóa: {ex.Message}"));
            }
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "customer")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> GetMyProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.Include(c => c.User).Include(c => c.Pets).Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null) return NotFound(ResponseDto<CustomerDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));
            return Ok(ResponseDto<CustomerDto>.SuccessResponse(MapToCustomerDto(customer)));
        }

        [HttpPost("update-profile")]
        [Authorize(Roles = "customer")]
        public async Task<ActionResult<ResponseDto<CustomerDto>>> UpdateProfile([FromBody] UpdateCustomerDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null) return NotFound(ResponseDto<CustomerDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var user = customer.User!;
            if (!string.IsNullOrEmpty(dto.FullName)) user.FullName = dto.FullName;
            if (!string.IsNullOrEmpty(dto.Phone)) user.Phone = dto.Phone;
            if (dto.DateOfBirth.HasValue) user.DateOfBirth = dto.DateOfBirth.Value;
            if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
            
            await _context.SaveChangesAsync();
            return Ok(ResponseDto<CustomerDto>.SuccessResponse(MapToCustomerDto(customer), "Cập nhật profile thành công"));
        }
    }
}