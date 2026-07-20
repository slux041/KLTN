using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.DTOs.Dashboard;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin,staff")]
    public class DashboardController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public DashboardController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard/stats
        [HttpGet("stats")]
        public async Task<ActionResult<ResponseDto<DashboardStatsDto>>> GetStats()
        {
            var today = DateTime.Today;
            var thisMonth = new DateTime(today.Year, today.Month, 1);

            var stats = new DashboardStatsDto
            {
                TotalRevenue = await _context.Orders
                    .Where(o => o.OrderStatus == "completed" && o.PaymentStatus == "paid")
                    .SumAsync(o => o.TotalAmount),

                TotalOrders = await _context.Orders.CountAsync(),

                TotalCustomers = await _context.Customers.CountAsync(),

                TotalProducts = await _context.Products.CountAsync(),

                PendingOrders = await _context.Orders
                    .CountAsync(o => o.OrderStatus == "pending"),

                TodayAppointments = await _context.Appointments
                    .CountAsync(a => a.AppointmentDate.Date == today && a.Status != "canceled"),

                LowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 10 && p.IsActive)
            };

            return Ok(ResponseDto<DashboardStatsDto>.SuccessResponse(stats));
        }

        // GET: api/dashboard/revenue-chart
        [HttpGet("revenue-chart")]
        public async Task<ActionResult<ResponseDto<List<RevenueChartDto>>>> GetRevenueChart([FromQuery] int days = 7)
        {
            var startDate = DateTime.Today.AddDays(-days);

            var rawData = await _context.Orders
                .Where(o => o.CreatedAt >= startDate && o.OrderStatus == "completed" && o.PaymentStatus == "paid")
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new 
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count()
                })
                .ToListAsync();

            var result = rawData
                .OrderBy(x => x.Date)
                .Select(x => new RevenueChartDto
                {
                    Date = x.Date.ToString("dd/MM"),
                    Revenue = x.Revenue,
                    OrderCount = x.OrderCount
                })
                .ToList();

            return Ok(ResponseDto<List<RevenueChartDto>>.SuccessResponse(result));
        }

        // GET: api/dashboard/top-products
        [HttpGet("top-products")]
        public async Task<ActionResult<ResponseDto<List<TopProductDto>>>> GetTopProducts([FromQuery] int limit = 5)
        {
            var topProducts = await _context.OrderItems
                .Where(oi => oi.ProductId != null && oi.Order!.OrderStatus == "completed")
                .GroupBy(oi => new { oi.ProductId, oi.Product!.Name })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.ProductId!.Value,
                    ProductName = g.Key.Name,
                    TotalSold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Price * oi.Quantity)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .Take(limit)
                .ToListAsync();

            return Ok(ResponseDto<List<TopProductDto>>.SuccessResponse(topProducts));
        }

        // GET: api/dashboard/top-services
        [HttpGet("top-services")]
        public async Task<ActionResult<ResponseDto<List<TopServiceDto>>>> GetTopServices([FromQuery] int limit = 5)
        {
            var topServices = await _context.OrderItems
                .Where(oi => oi.ServiceId != null && oi.Order!.OrderStatus == "completed")
                .GroupBy(oi => new { oi.ServiceId, oi.Service!.Name })
                .Select(g => new TopServiceDto
                {
                    ServiceId = g.Key.ServiceId!.Value,
                    ServiceName = g.Key.Name,
                    TotalBookings = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Price * oi.Quantity)
                })
                .OrderByDescending(s => s.TotalRevenue)
                .Take(limit)
                .ToListAsync();

            return Ok(ResponseDto<List<TopServiceDto>>.SuccessResponse(topServices));
        }

        // GET: api/dashboard/recent-orders
        [HttpGet("recent-orders")]
        public async Task<ActionResult<ResponseDto<List<RecentOrderDto>>>> GetRecentOrders([FromQuery] int limit = 10)
        {
            var recentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedAt)
                .Take(limit)
                .Select(o => new RecentOrderDto
                {
                    OrderId = o.OrderId,
                    CustomerName = o.User!.FullName,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();

            return Ok(ResponseDto<List<RecentOrderDto>>.SuccessResponse(recentOrders));
        }

        // GET: api/dashboard/monthly-summary
        [HttpGet("monthly-summary")]
        public async Task<ActionResult<ResponseDto<object>>> GetMonthlySummary([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            if (year == 0) year = DateTime.Now.Year;
            if (month == 0) month = DateTime.Now.Month;

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var totalRevenue = await _context.Orders
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt < endDate && 
                       o.OrderStatus == "completed" && o.PaymentStatus == "paid")
                .SumAsync(o => o.TotalAmount);

            var totalOrders = await _context.Orders
                .CountAsync(o => o.CreatedAt >= startDate && o.CreatedAt < endDate);

            var completedOrders = await _context.Orders
                .CountAsync(o => o.CreatedAt >= startDate && o.CreatedAt < endDate && o.OrderStatus == "completed");

            var totalAppointments = await _context.Appointments
                .CountAsync(a => a.AppointmentDate >= startDate && a.AppointmentDate < endDate);

            var newCustomers = await _context.Customers
                .Include(c => c.User)
                .CountAsync(c => c.User!.CreatedAt >= startDate && c.User.CreatedAt < endDate);

            var summary = new
            {
                Year = year,
                Month = month,
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                CompletedOrders = completedOrders,
                TotalAppointments = totalAppointments,
                NewCustomers = newCustomers
            };

            return Ok(ResponseDto<object>.SuccessResponse(summary));
        }
    }
}