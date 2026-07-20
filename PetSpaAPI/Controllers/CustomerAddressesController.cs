using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Address;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "customer")]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public CustomerAddressesController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/customeraddresses
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CustomerAddressDto>>>> GetAddresses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<List<CustomerAddressDto>>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var addresses = await _context.CustomerAddresses
                .Where(a => a.CustomerId == customer.CustomerId)
                .OrderByDescending(a => a.IsDefault)
                .ThenByDescending(a => a.CreatedAt)
                .Select(a => new CustomerAddressDto
                {
                    AddressId = a.AddressId,
                    CustomerId = a.CustomerId,
                    FullName = a.FullName,
                    Phone = a.Phone,
                    AddressLine = a.AddressLine,
                    ProvinceId = a.ProvinceId,
                    ProvinceName = a.ProvinceName,
                    WardId = a.WardId,
                    WardName = a.WardName,
                    IsDefault = a.IsDefault,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(ResponseDto<List<CustomerAddressDto>>.SuccessResponse(addresses));
        }

        // GET: api/customeraddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CustomerAddressDto>>> GetAddress(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<CustomerAddressDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var address = await _context.CustomerAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.CustomerId == customer.CustomerId);

            if (address == null)
                return NotFound(ResponseDto<CustomerAddressDto>.ErrorResponse("Địa chỉ không tồn tại"));

            var addressDto = new CustomerAddressDto
            {
                AddressId = address.AddressId,
                CustomerId = address.CustomerId,
                FullName = address.FullName,
                Phone = address.Phone,
                AddressLine = address.AddressLine,
                ProvinceId = address.ProvinceId,
                ProvinceName = address.ProvinceName,
                WardId = address.WardId,
                WardName = address.WardName,
                IsDefault = address.IsDefault,
                CreatedAt = address.CreatedAt
            };

            return Ok(ResponseDto<CustomerAddressDto>.SuccessResponse(addressDto));
        }

        // POST: api/customeraddresses
        [HttpPost]
        public async Task<ActionResult<ResponseDto<CustomerAddressDto>>> CreateAddress([FromBody] CreateCustomerAddressDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<CustomerAddressDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            // Validate địa chỉ 2 cấp
            if (string.IsNullOrEmpty(dto.ProvinceId) || string.IsNullOrEmpty(dto.WardId))
                return BadRequest(ResponseDto<CustomerAddressDto>.ErrorResponse("Vui lòng chọn đầy đủ Tỉnh/Thành phố và Xã/Phường"));

            // If this is set as default, remove default from other addresses
            if (dto.IsDefault)
            {
                var currentDefault = await _context.CustomerAddresses
                    .Where(a => a.CustomerId == customer.CustomerId && a.IsDefault)
                    .ToListAsync();
                
                foreach (var addr in currentDefault)
                {
                    addr.IsDefault = false;
                }
            }

            var address = new CustomerAddress
            {
                CustomerId = customer.CustomerId,
                FullName = dto.FullName,
                Phone = dto.Phone,
                AddressLine = dto.AddressLine,
                ProvinceId = dto.ProvinceId,
                ProvinceName = dto.ProvinceName,
                WardId = dto.WardId,
                WardName = dto.WardName,
                IsDefault = dto.IsDefault,
                CreatedAt = DateTime.Now
            };

            _context.CustomerAddresses.Add(address);
            await _context.SaveChangesAsync();

            var addressDto = new CustomerAddressDto
            {
                AddressId = address.AddressId,
                CustomerId = address.CustomerId,
                FullName = address.FullName,
                Phone = address.Phone,
                AddressLine = address.AddressLine,
                ProvinceId = address.ProvinceId,
                ProvinceName = address.ProvinceName,
                WardId = address.WardId,
                WardName = address.WardName,
                IsDefault = address.IsDefault,
                CreatedAt = address.CreatedAt
            };

            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId },
                ResponseDto<CustomerAddressDto>.SuccessResponse(addressDto, "Thêm địa chỉ thành công"));
        }

        // POST: api/customeraddresses/5/update
        [HttpPost("{id}/update")]
        public async Task<ActionResult<ResponseDto<CustomerAddressDto>>> UpdateAddress(int id, [FromBody] UpdateCustomerAddressDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<CustomerAddressDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var address = await _context.CustomerAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.CustomerId == customer.CustomerId);

            if (address == null)
                return NotFound(ResponseDto<CustomerAddressDto>.ErrorResponse("Địa chỉ không tồn tại"));

            // Update basic info
            if (!string.IsNullOrEmpty(dto.FullName))
                address.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Phone))
                address.Phone = dto.Phone;

            if (!string.IsNullOrEmpty(dto.AddressLine))
                address.AddressLine = dto.AddressLine;

            if (!string.IsNullOrEmpty(dto.ProvinceId))
            {
                address.ProvinceId = dto.ProvinceId;
                address.ProvinceName = dto.ProvinceName ?? address.ProvinceName;
            }

            if (!string.IsNullOrEmpty(dto.WardId))
            {
                address.WardId = dto.WardId;
                address.WardName = dto.WardName ?? address.WardName;
            }

            // Update default status
            if (dto.IsDefault.HasValue && dto.IsDefault.Value)
            {
                var currentDefault = await _context.CustomerAddresses
                    .Where(a => a.CustomerId == customer.CustomerId && a.IsDefault && a.AddressId != id)
                    .ToListAsync();
                
                foreach (var addr in currentDefault)
                {
                    addr.IsDefault = false;
                }
                
                address.IsDefault = true;
            }

            await _context.SaveChangesAsync();

            var addressDto = new CustomerAddressDto
            {
                AddressId = address.AddressId,
                CustomerId = address.CustomerId,
                FullName = address.FullName,
                Phone = address.Phone,
                AddressLine = address.AddressLine,
                ProvinceId = address.ProvinceId,
                ProvinceName = address.ProvinceName,
                WardId = address.WardId,
                WardName = address.WardName,
                IsDefault = address.IsDefault,
                CreatedAt = address.CreatedAt
            };

            return Ok(ResponseDto<CustomerAddressDto>.SuccessResponse(addressDto, "Cập nhật địa chỉ thành công"));
        }

        // POST: api/customeraddresses/5/set-default
        [HttpPost("{id}/set-default")]
        public async Task<ActionResult<ResponseDto<string>>> SetDefaultAddress(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var address = await _context.CustomerAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.CustomerId == customer.CustomerId);

            if (address == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Địa chỉ không tồn tại"));

            var allAddresses = await _context.CustomerAddresses
                .Where(a => a.CustomerId == customer.CustomerId)
                .ToListAsync();

            foreach (var addr in allAddresses)
            {
                addr.IsDefault = addr.AddressId == id;
            }

            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Đã đặt làm địa chỉ mặc định"));
        }

        // DELETE: api/customeraddresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteAddress(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var address = await _context.CustomerAddresses
                .FirstOrDefaultAsync(a => a.AddressId == id && a.CustomerId == customer.CustomerId);

            if (address == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Địa chỉ không tồn tại"));

            _context.CustomerAddresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa địa chỉ thành công"));
        }
    }
}