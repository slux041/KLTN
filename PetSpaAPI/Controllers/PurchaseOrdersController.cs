using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.PurchaseOrder;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin,staff")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public PurchaseOrdersController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/purchaseorders
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PurchaseOrderDto>>>> GetPurchaseOrders(
            [FromQuery] int? supplierId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var query = _context.PurchaseOrders
                .Include(p => p.Supplier)
                .Include(p => p.Staff)
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .AsQueryable();

            if (supplierId.HasValue)
                query = query.Where(p => p.SupplierId == supplierId.Value);

            if (fromDate.HasValue)
                query = query.Where(p => p.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(p => p.CreatedAt <= toDate.Value);

            var purchaseOrders = await query
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PurchaseOrderDto
                {
                    PurchaseOrderId = p.PurchaseOrderId,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier!.Name,
                    StaffId = p.StaffId,
                    StaffName = p.Staff!.FullName,
                    TotalAmount = p.TotalAmount,
                    CreatedAt = p.CreatedAt,
                    Items = p.Items!.Select(i => new PurchaseOrderItemDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                })
                .ToListAsync();

            return Ok(ResponseDto<List<PurchaseOrderDto>>.SuccessResponse(purchaseOrders));
        }

        // GET: api/purchaseorders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PurchaseOrderDto>>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Supplier)
                .Include(p => p.Staff)
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(p => p.PurchaseOrderId == id);

            if (purchaseOrder == null)
                return NotFound(ResponseDto<PurchaseOrderDto>.ErrorResponse("Đơn nhập hàng không tồn tại"));

            var purchaseOrderDto = new PurchaseOrderDto
            {
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                SupplierId = purchaseOrder.SupplierId,
                SupplierName = purchaseOrder.Supplier!.Name,
                StaffId = purchaseOrder.StaffId,
                StaffName = purchaseOrder.Staff!.FullName,
                TotalAmount = purchaseOrder.TotalAmount,
                CreatedAt = purchaseOrder.CreatedAt,
                Items = purchaseOrder.Items!.Select(i => new PurchaseOrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product!.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return Ok(ResponseDto<PurchaseOrderDto>.SuccessResponse(purchaseOrderDto));
        }

        // POST: api/purchaseorders
        [HttpPost]
        public async Task<ActionResult<ResponseDto<PurchaseOrderDto>>> CreatePurchaseOrder([FromBody] CreatePurchaseOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest(ResponseDto<PurchaseOrderDto>.ErrorResponse("Đơn nhập hàng phải có ít nhất một sản phẩm"));

            var supplierExists = await _context.Suppliers.AnyAsync(s => s.SupplierId == dto.SupplierId);
            if (!supplierExists)
                return BadRequest(ResponseDto<PurchaseOrderDto>.ErrorResponse("Nhà cung cấp không tồn tại"));

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            decimal totalAmount = 0;
            var purchaseOrderItems = new List<PurchaseOrderItem>();

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return BadRequest(ResponseDto<PurchaseOrderDto>.ErrorResponse($"Sản phẩm ID {item.ProductId} không tồn tại"));

                purchaseOrderItems.Add(new PurchaseOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });

                totalAmount += item.Price * item.Quantity;
                product.StockQuantity += item.Quantity;
            }

            var purchaseOrder = new PurchaseOrder
            {
                SupplierId = dto.SupplierId,
                StaffId = userId,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.Now,
                Items = purchaseOrderItems
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            var createdOrder = await _context.PurchaseOrders
                .Include(p => p.Supplier)
                .Include(p => p.Staff)
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(p => p.PurchaseOrderId == purchaseOrder.PurchaseOrderId);

            var purchaseOrderDto = new PurchaseOrderDto
            {
                PurchaseOrderId = createdOrder!.PurchaseOrderId,
                SupplierId = createdOrder.SupplierId,
                SupplierName = createdOrder.Supplier!.Name,
                StaffId = createdOrder.StaffId,
                StaffName = createdOrder.Staff!.FullName,
                TotalAmount = createdOrder.TotalAmount,
                CreatedAt = createdOrder.CreatedAt,
                Items = createdOrder.Items!.Select(i => new PurchaseOrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product!.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return CreatedAtAction(nameof(GetPurchaseOrder), new { id = purchaseOrder.PurchaseOrderId },
                ResponseDto<PurchaseOrderDto>.SuccessResponse(purchaseOrderDto, "Tạo đơn nhập hàng thành công"));
        }

        // DELETE: api/purchaseorders/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeletePurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Đơn nhập hàng không tồn tại"));

            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa đơn nhập hàng thành công"));
        }
    }
}