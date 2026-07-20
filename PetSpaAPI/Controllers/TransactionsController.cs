using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.DTOs.Common;
using PetSpaAPI.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin,staff")]
    public class TransactionsController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public TransactionsController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<TransactionDto>>>> GetTransactions(
            [FromQuery] string? type = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var allTransactions = new List<TransactionDto>();

            bool getSales = string.IsNullOrEmpty(type) || type.ToLower() == "sale";
            bool getPurchases = string.IsNullOrEmpty(type) || type.ToLower() == "purchase";

            if (getSales)
            {
                var salesQuery = _context.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderStatus == "completed")
                    .Include(o => o.Items).ThenInclude(i => i.Product)
                    .Include(o => o.Items).ThenInclude(i => i.Service)
                    .Include(o => o.User)
                    .AsQueryable();

                if (startDate.HasValue)
                    salesQuery = salesQuery.Where(o => o.CreatedAt >= startDate.Value);
                if (endDate.HasValue)
                    salesQuery = salesQuery.Where(o => o.CreatedAt <= endDate.Value);

                var orders = await salesQuery.ToListAsync();

                var salesDtos = orders.SelectMany(o => o.Items!.Select(item => new TransactionDto
                {
                    Type = "Sale",
                    Name = item.ProductId.HasValue ? (item.Product?.Name ?? "Sản phẩm xóa") : (item.Service?.Name ?? "Dịch vụ xóa"),
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Quantity * item.Price,
                    Date = o.CreatedAt,
                    ReferenceId = o.OrderId
                }));

                allTransactions.AddRange(salesDtos);
            }

            if (getPurchases)
            {
                var purchaseQuery = _context.PurchaseOrders
                    .AsNoTracking()
                    .Include(po => po.Items).ThenInclude(i => i.Product)
                    .Include(po => po.Supplier)
                    .AsQueryable();

                if (startDate.HasValue)
                    purchaseQuery = purchaseQuery.Where(po => po.CreatedAt >= startDate.Value);
                if (endDate.HasValue)
                    purchaseQuery = purchaseQuery.Where(po => po.CreatedAt <= endDate.Value);

                var purchases = await purchaseQuery.ToListAsync();

                var purchaseDtos = purchases.SelectMany(po => po.Items!.Select(item => new TransactionDto
                {
                    Type = "Purchase",
                    Name = item.Product?.Name ?? "Sản phẩm đã xóa",
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Quantity * item.Price,
                    Date = po.CreatedAt,
                    ReferenceId = po.PurchaseOrderId
                }));

                allTransactions.AddRange(purchaseDtos);
            }

            var result = allTransactions.OrderByDescending(t => t.Date).ToList();

            return Ok(ResponseDto<List<TransactionDto>>.SuccessResponse(result));
        }
    }
}