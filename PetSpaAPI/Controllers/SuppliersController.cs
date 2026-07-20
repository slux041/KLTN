using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Supplier;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin,staff")]
    public class SuppliersController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public SuppliersController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<SupplierDto>>>> GetSuppliers([FromQuery] string? search = null)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search) || (s.Email != null && s.Email.Contains(search)));

            var suppliers = await query
                .OrderBy(s => s.Name)
                .Select(s => new SupplierDto
                {
                    SupplierId = s.SupplierId,
                    Name = s.Name,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    BankAccount = s.BankAccount
                })
                .ToListAsync();

            return Ok(ResponseDto<List<SupplierDto>>.SuccessResponse(suppliers));
        }

        // GET: api/suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<SupplierDto>>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound(ResponseDto<SupplierDto>.ErrorResponse("Nhà cung cấp không tồn tại"));

            var supplierDto = new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                BankAccount = supplier.BankAccount
            };

            return Ok(ResponseDto<SupplierDto>.SuccessResponse(supplierDto));
        }

        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult<ResponseDto<SupplierDto>>> CreateSupplier([FromBody] CreateSupplierDto dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                BankAccount = dto.BankAccount
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            var supplierDto = new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                BankAccount = supplier.BankAccount
            };

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.SupplierId },
                ResponseDto<SupplierDto>.SuccessResponse(supplierDto, "Tạo nhà cung cấp thành công"));
        }

        // POST: api/suppliers/5/update
        [HttpPost("{id}/update")]
        public async Task<ActionResult<ResponseDto<SupplierDto>>> UpdateSupplier(int id, [FromBody] UpdateSupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound(ResponseDto<SupplierDto>.ErrorResponse("Nhà cung cấp không tồn tại"));

            if (!string.IsNullOrEmpty(dto.Name))
                supplier.Name = dto.Name;

            if (dto.Address != null)
                supplier.Address = dto.Address;

            if (dto.Phone != null)
                supplier.Phone = dto.Phone;

            if (dto.Email != null)
                supplier.Email = dto.Email;

            if (dto.BankAccount != null)
                supplier.BankAccount = dto.BankAccount;

            await _context.SaveChangesAsync();

            var supplierDto = new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                BankAccount = supplier.BankAccount
            };

            return Ok(ResponseDto<SupplierDto>.SuccessResponse(supplierDto, "Cập nhật nhà cung cấp thành công"));
        }

        // DELETE: api/suppliers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Nhà cung cấp không tồn tại"));

            var hasPurchaseOrders = await _context.PurchaseOrders.AnyAsync(p => p.SupplierId == id);
            if (hasPurchaseOrders)
                return BadRequest(ResponseDto<string>.ErrorResponse("Không thể xóa nhà cung cấp đang có đơn nhập hàng"));

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa nhà cung cấp thành công"));
        }
    }
}