using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Service;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public ServicesController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/services
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ServiceDto>>>> GetServices(
            [FromQuery] int? categoryId = null,
            [FromQuery] string? search = null,
            [FromQuery] bool? isActive = null)
        {
            var query = _context.Services
                .Include(s => s.Category)
                .Include(s => s.ServicePrices)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(s => s.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search));

            if (isActive.HasValue)
                query = query.Where(s => s.IsActive == isActive.Value);

            var services = await query
                .OrderBy(s => s.Name)
                .Select(s => new ServiceDto
                {
                    ServiceId = s.ServiceId,
                    CategoryId = s.CategoryId,
                    CategoryName = s.Category!.Name,
                    Name = s.Name,
                    DurationMinutes = s.DurationMinutes,
                    Price = s.Price,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    PricingMethod = s.PricingMethod,
                    ServicePrices = s.PricingMethod == "weight_based" && s.ServicePrices != null
                        ? s.ServicePrices.Select(sp => new ServicePriceDto
                        {
                            PriceId = sp.PriceId,
                            PetType = sp.PetType,
                            MinWeight = sp.MinWeight,
                            MaxWeight = sp.MaxWeight,
                            Price = sp.Price
                        }).ToList()
                        : null
                })
                .ToListAsync();

            return Ok(ResponseDto<List<ServiceDto>>.SuccessResponse(services));
        }

        // GET: api/services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ServiceDto>>> GetService(int id)
        {
            var service = await _context.Services
                .Include(s => s.Category)
                .Include(s => s.ServicePrices)
                .FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
                return NotFound(ResponseDto<ServiceDto>.ErrorResponse("Dịch vụ không tồn tại"));

            var serviceDto = new ServiceDto
            {
                ServiceId = service.ServiceId,
                CategoryId = service.CategoryId,
                CategoryName = service.Category!.Name,
                Name = service.Name,
                DurationMinutes = service.DurationMinutes,
                Price = service.Price,
                Description = service.Description,
                IsActive = service.IsActive,
                PricingMethod = service.PricingMethod,
                ServicePrices = service.PricingMethod == "weight_based" && service.ServicePrices != null
                    ? service.ServicePrices.Select(sp => new ServicePriceDto
                    {
                        PriceId = sp.PriceId,
                        PetType = sp.PetType,
                        MinWeight = sp.MinWeight,
                        MaxWeight = sp.MaxWeight,
                        Price = sp.Price
                    }).ToList()
                    : null
            };

            return Ok(ResponseDto<ServiceDto>.SuccessResponse(serviceDto));
        }

        // POST: api/services
        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<ServiceDto>>> CreateService([FromBody] CreateServiceDto dto)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!categoryExists)
                return BadRequest(ResponseDto<ServiceDto>.ErrorResponse("Danh mục không tồn tại"));

            if (dto.PricingMethod == "weight_based" && (dto.ServicePrices == null || !dto.ServicePrices.Any()))
                return BadRequest(ResponseDto<ServiceDto>.ErrorResponse("Dịch vụ theo cân nặng phải có bảng giá"));

            var service = new Service
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                DurationMinutes = dto.DurationMinutes,
                Price = dto.Price,
                Description = dto.Description,
                IsActive = true,
                PricingMethod = dto.PricingMethod
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            if (dto.PricingMethod == "weight_based" && dto.ServicePrices != null)
            {
                foreach (var priceDto in dto.ServicePrices)
                {
                    var servicePrice = new ServicePrice
                    {
                        ServiceId = service.ServiceId,
                        PetType = priceDto.PetType,
                        MinWeight = priceDto.MinWeight,
                        MaxWeight = priceDto.MaxWeight,
                        Price = priceDto.Price
                    };
                    _context.ServicePrices.Add(servicePrice);
                }
                await _context.SaveChangesAsync();
            }

            await _context.Entry(service).Reference(s => s.Category).LoadAsync();
            await _context.Entry(service).Collection(s => s.ServicePrices!).LoadAsync();

            var serviceDto = new ServiceDto
            {
                ServiceId = service.ServiceId,
                CategoryId = service.CategoryId,
                CategoryName = service.Category!.Name,
                Name = service.Name,
                DurationMinutes = service.DurationMinutes,
                Price = service.Price,
                Description = service.Description,
                IsActive = service.IsActive,
                PricingMethod = service.PricingMethod,
                ServicePrices = service.PricingMethod == "weight_based" && service.ServicePrices != null
                    ? service.ServicePrices.Select(sp => new ServicePriceDto
                    {
                        PriceId = sp.PriceId,
                        PetType = sp.PetType,
                        MinWeight = sp.MinWeight,
                        MaxWeight = sp.MaxWeight,
                        Price = sp.Price
                    }).ToList()
                    : null
            };

            return CreatedAtAction(nameof(GetService), new { id = service.ServiceId },
                ResponseDto<ServiceDto>.SuccessResponse(serviceDto, "Tạo dịch vụ thành công"));
        }

        // POST: api/services/5/update
        [HttpPost("{id}/update")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<ServiceDto>>> UpdateService(int id, [FromBody] UpdateServiceDto dto)
        {
            var service = await _context.Services
                .Include(s => s.Category)
                .Include(s => s.ServicePrices)
                .FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
                return NotFound(ResponseDto<ServiceDto>.ErrorResponse("Dịch vụ không tồn tại"));

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId.Value);
                if (!categoryExists)
                    return BadRequest(ResponseDto<ServiceDto>.ErrorResponse("Danh mục không tồn tại"));
                service.CategoryId = dto.CategoryId.Value;
            }

            if (!string.IsNullOrEmpty(dto.Name))
                service.Name = dto.Name;

            if (dto.DurationMinutes.HasValue)
                service.DurationMinutes = dto.DurationMinutes.Value;

            if (dto.Price.HasValue)
                service.Price = dto.Price.Value;

            if (dto.Description != null)
                service.Description = dto.Description;

            if (dto.IsActive.HasValue)
                service.IsActive = dto.IsActive.Value;

            if (!string.IsNullOrEmpty(dto.PricingMethod))
            {
                if (dto.PricingMethod == "weight_based" && (dto.ServicePrices == null || !dto.ServicePrices.Any()))
                    return BadRequest(ResponseDto<ServiceDto>.ErrorResponse("Dịch vụ theo cân nặng phải có bảng giá"));

                service.PricingMethod = dto.PricingMethod;

                if (service.ServicePrices != null && service.ServicePrices.Any())
                {
                    _context.ServicePrices.RemoveRange(service.ServicePrices);
                }

                if (dto.PricingMethod == "weight_based" && dto.ServicePrices != null)
                {
                    foreach (var priceDto in dto.ServicePrices)
                    {
                        var servicePrice = new ServicePrice
                        {
                            ServiceId = service.ServiceId,
                            PetType = priceDto.PetType,
                            MinWeight = priceDto.MinWeight,
                            MaxWeight = priceDto.MaxWeight,
                            Price = priceDto.Price
                        };
                        _context.ServicePrices.Add(servicePrice);
                    }
                }
            }
            else if (dto.ServicePrices != null)
            {
                if (service.PricingMethod == "weight_based")
                {
                    if (service.ServicePrices != null && service.ServicePrices.Any())
                    {
                        _context.ServicePrices.RemoveRange(service.ServicePrices);
                    }

                    foreach (var priceDto in dto.ServicePrices)
                    {
                        var servicePrice = new ServicePrice
                        {
                            ServiceId = service.ServiceId,
                            PetType = priceDto.PetType,
                            MinWeight = priceDto.MinWeight,
                            MaxWeight = priceDto.MaxWeight,
                            Price = priceDto.Price
                        };
                        _context.ServicePrices.Add(servicePrice);
                    }
                }
            }

            await _context.SaveChangesAsync();

            await _context.Entry(service).Collection(s => s.ServicePrices!).LoadAsync();

            var serviceDto = new ServiceDto
            {
                ServiceId = service.ServiceId,
                CategoryId = service.CategoryId,
                CategoryName = service.Category!.Name,
                Name = service.Name,
                DurationMinutes = service.DurationMinutes,
                Price = service.Price,
                Description = service.Description,
                IsActive = service.IsActive,
                PricingMethod = service.PricingMethod,
                ServicePrices = service.PricingMethod == "weight_based" && service.ServicePrices != null
                    ? service.ServicePrices.Select(sp => new ServicePriceDto
                    {
                        PriceId = sp.PriceId,
                        PetType = sp.PetType,
                        MinWeight = sp.MinWeight,
                        MaxWeight = sp.MaxWeight,
                        Price = sp.Price
                    }).ToList()
                    : null
            };

            return Ok(ResponseDto<ServiceDto>.SuccessResponse(serviceDto, "Cập nhật dịch vụ thành công"));
        }

        // DELETE: api/services/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Dịch vụ không tồn tại"));

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa dịch vụ thành công"));
        }
    }
}