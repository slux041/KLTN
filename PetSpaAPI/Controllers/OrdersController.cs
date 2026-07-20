using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Order;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public OrdersController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<OrderDto>>>> GetOrders(
            [FromQuery] string? status = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] string? search = null)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            IQueryable<Order> query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Include(o => o.Items)
                .ThenInclude(i => i.Service);

            if (userRole == "customer")
            {
                query = query.Where(o => o.UserId == userId);
            }

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.OrderStatus == status);

            if (fromDate.HasValue)
                query = query.Where(o => o.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.CreatedAt <= toDate.Value.Date.AddDays(1).AddTicks(-1));

            if (!string.IsNullOrEmpty(search))
            {
                bool isNum = int.TryParse(search, out int searchId);
                if (isNum)
                {
                    query = query.Where(o => o.OrderId == searchId);
                }
                else
                {
                    query = query.Where(o => o.ShippingFullName.Contains(search) ||
                                             (o.User != null && o.User.FullName.Contains(search)));
                }
            }

            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    CustomerName = o.User != null ? o.User.FullName : "Unknown",
                    CustomerPhone = o.User != null ? (o.User.Phone ?? "") : "",

                    ShippingAddressId = o.ShippingAddressId,
                    ShippingFullName = o.ShippingFullName,
                    ShippingPhone = o.ShippingPhone,
                    ShippingAddressLine = o.ShippingAddressLine,
                    ShippingProvinceName = o.ShippingProvinceName,
                    ShippingWardName = o.ShippingWardName,

                    Subtotal = o.Subtotal,
                    ShippingFee = o.ShippingFee,
                    PromotionId = o.PromotionId,
                    DiscountAmount = o.DiscountAmount,
                    TotalAmount = o.TotalAmount,
                    Note = o.Note,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus,
                    OrderStatus = o.OrderStatus,
                    CreatedAt = o.CreatedAt,
                    Items = o.Items!.Select(i => new OrderItemDto
                    {
                        OrderItemId = i.OrderItemId,
                        ProductId = i.ProductId,
                        ProductName = i.Product != null ? i.Product.Name : null,
                        ServiceId = i.ServiceId,
                        ServiceName = i.Service != null ? i.Service.Name : null,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                })
                .ToListAsync();

            return Ok(ResponseDto<List<OrderDto>>.SuccessResponse(orders));
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<OrderDto>>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Include(o => o.Items)
                .ThenInclude(i => i.Service)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound(ResponseDto<OrderDto>.ErrorResponse("Đơn hàng không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                if (order.UserId != userId)
                    return Forbid();
            }

            var orderDto = new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CustomerName = order.User != null ? order.User.FullName : "Unknown",
                CustomerPhone = order.User != null ? (order.User.Phone ?? "") : "",

                ShippingAddressId = order.ShippingAddressId,
                ShippingFullName = order.ShippingFullName,
                ShippingPhone = order.ShippingPhone,
                ShippingAddressLine = order.ShippingAddressLine,
                ShippingProvinceName = order.ShippingProvinceName,
                ShippingWardName = order.ShippingWardName,

                Subtotal = order.Subtotal,
                ShippingFee = order.ShippingFee,
                PromotionId = order.PromotionId,
                DiscountAmount = order.DiscountAmount,
                TotalAmount = order.TotalAmount,
                Note = order.Note,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus,
                CreatedAt = order.CreatedAt,
                Items = order.Items!.Select(i => new OrderItemDto
                {
                    OrderItemId = i.OrderItemId,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    ServiceId = i.ServiceId,
                    ServiceName = i.Service?.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return Ok(ResponseDto<OrderDto>.SuccessResponse(orderDto));
        }

        // POST: api/orders
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto<OrderDto>>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Đơn hàng phải có ít nhất một sản phẩm"));

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            int orderOwnerId = loggedInUserId;

            if (role == "admin" || role == "staff")
            {
                var linkedCustomer = await _context.Customers
        .FirstOrDefaultAsync(c => c.CustomerId == dto.UserId.Value);

                if (linkedCustomer != null)
                {
                    orderOwnerId = linkedCustomer.UserId;
                }
                else
                {
                    orderOwnerId = 10;
                }
            }

            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == orderOwnerId);

            string? shippingFullName = null;
            string? shippingPhone = null;
            string? shippingAddressLine = null;
            string? shippingProvinceId = null;
            string? shippingProvinceName = null;
            string? shippingWardId = null;
            string? shippingWardName = null;
            int? shippingAddressId = null;
            bool isPosOrder = (dto.PaymentStatus == "paid" || dto.OrderStatus == "completed");
            if (isPosOrder)
            {
                shippingFullName = dto.ShippingFullName ?? (customer?.User?.FullName ?? "Khách lẻ");
                shippingPhone = dto.ShippingPhone ?? (customer?.User?.Phone ?? "");

                var defaultAddress = await _context.CustomerAddresses
                    .FirstOrDefaultAsync(a => customer != null && a.CustomerId == customer.CustomerId && a.IsDefault);

                if (defaultAddress != null)
                {
                    shippingAddressLine = defaultAddress.AddressLine;
                    shippingProvinceId = defaultAddress.ProvinceId;
                    shippingProvinceName = defaultAddress.ProvinceName;
                    shippingWardId = defaultAddress.WardId;
                    shippingWardName = defaultAddress.WardName;
                    shippingAddressId = defaultAddress.AddressId;
                }
                else if (customer != null && !string.IsNullOrEmpty(customer.Address) && customer.Address.ToLower() != "mua tại quầy")
                {
                    shippingAddressLine = customer.Address;

                    shippingProvinceName = "N/A";
                    shippingWardName = "N/A";
                    shippingProvinceId = "00";
                    shippingWardId = "0000";
                }
                else
                {
                    shippingAddressLine = "Mua tại quầy";
                    shippingProvinceName = "N/A";
                    shippingWardName = "N/A";
                    shippingProvinceId = "00";
                    shippingWardId = "0000";
                }
            }
            else
            {
                if (dto.ShippingAddressId.HasValue)
                {
                    if (customer == null)
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Tài khoản này chưa có hồ sơ khách hàng (Customer Profile)."));

                    var existingAddress = await _context.CustomerAddresses
                        .FirstOrDefaultAsync(a => a.AddressId == dto.ShippingAddressId.Value &&
                                                a.CustomerId == customer.CustomerId);

                    if (existingAddress == null)
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Địa chỉ giao hàng không tồn tại"));

                    shippingAddressId = existingAddress.AddressId;
                    shippingFullName = existingAddress.FullName;
                    shippingPhone = existingAddress.Phone;
                    shippingAddressLine = existingAddress.AddressLine;
                    shippingProvinceId = existingAddress.ProvinceId;
                    shippingProvinceName = existingAddress.ProvinceName;
                    shippingWardId = existingAddress.WardId;
                    shippingWardName = existingAddress.WardName;
                }
                else
                {
                    if (string.IsNullOrEmpty(dto.ShippingFullName) || string.IsNullOrEmpty(dto.ShippingPhone) ||
                        string.IsNullOrEmpty(dto.ShippingAddressLine) || string.IsNullOrEmpty(dto.ShippingProvinceId) ||
                        string.IsNullOrEmpty(dto.ShippingWardId))
                    {
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Vui lòng nhập đầy đủ thông tin giao hàng (Tên, SĐT, Địa chỉ, Tỉnh/Thành, Phường/Xã)"));
                    }

                    shippingFullName = dto.ShippingFullName;
                    shippingPhone = dto.ShippingPhone;
                    shippingAddressLine = dto.ShippingAddressLine;
                    shippingProvinceId = dto.ShippingProvinceId;
                    shippingProvinceName = dto.ShippingProvinceName;
                    shippingWardId = dto.ShippingWardId;
                    shippingWardName = dto.ShippingWardName;
                }
            }

            decimal subtotal = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                if (item.ProductId.HasValue)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null || !product.IsActive)
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse($"Sản phẩm ID {item.ProductId} không tồn tại hoặc ngừng kinh doanh"));

                    if (product.StockQuantity < item.Quantity)
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse($"Sản phẩm {product.Name} không đủ số lượng (Còn: {product.StockQuantity})"));

                    orderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });

                    subtotal += product.Price * item.Quantity;
                }
                else if (item.ServiceId.HasValue)
                {
                    var service = await _context.Services.FindAsync(item.ServiceId);
                    if (service == null || !service.IsActive)
                        return BadRequest(ResponseDto<OrderDto>.ErrorResponse($"Dịch vụ ID {item.ServiceId} không tồn tại"));

                    decimal finalPrice = service.Price;
                    if (item.Price > 0)
                    {
                        finalPrice = (decimal)item.Price;
                    }

                    orderItems.Add(new OrderItem
                    {
                        ServiceId = item.ServiceId,
                        Quantity = item.Quantity,
                        Price = finalPrice
                    });

                    subtotal += finalPrice * item.Quantity;
                }
            }

            decimal discountAmount = 0;
            int? promotionId = null;

            if (!string.IsNullOrEmpty(dto.PromotionCode))
            {
                var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Code == dto.PromotionCode);

                if (promotion == null)
                    return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Mã giảm giá không tồn tại"));

                if (!promotion.IsActive)
                    return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Mã giảm giá đã bị khóa"));

                var now = DateTime.Now;
                if (now < promotion.StartDate || now > promotion.EndDate)
                    return BadRequest(ResponseDto<OrderDto>.ErrorResponse("Mã giảm giá đã hết hạn"));

                promotionId = promotion.PromotionId;
                discountAmount = subtotal * (promotion.DiscountPercent / 100);
            }
            else
            {
                discountAmount = (decimal)dto.DiscountAmount;
            }

            decimal shippingFee = isPosOrder ? 0 : 25000;

            decimal totalAmount = subtotal - discountAmount + shippingFee;
            if (totalAmount < 0) totalAmount = 0;

            var order = new Order
            {
                UserId = orderOwnerId,
                ShippingAddressId = shippingAddressId,
                ShippingFullName = shippingFullName,
                ShippingPhone = shippingPhone,
                ShippingAddressLine = shippingAddressLine,
                ShippingProvinceId = shippingProvinceId,
                ShippingProvinceName = shippingProvinceName,
                ShippingWardId = shippingWardId,
                ShippingWardName = shippingWardName,

                Subtotal = subtotal,
                ShippingFee = shippingFee,
                PromotionId = promotionId,
                DiscountAmount = discountAmount,
                TotalAmount = totalAmount,

                Note = dto.Note,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus ?? "pending",
                OrderStatus = dto.OrderStatus ?? "pending",
                CreatedAt = DateTime.Now,
                Items = orderItems
            };

            _context.Orders.Add(order);

            foreach (var item in orderItems.Where(i => i.ProductId.HasValue))
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                }
            }

            await _context.SaveChangesAsync();

            if (!isPosOrder)
            {
                var cartItems = await _context.CartItems.Where(c => c.UserId == orderOwnerId).ToListAsync();
                if (cartItems.Any())
                {
                    _context.CartItems.RemoveRange(cartItems);
                    await _context.SaveChangesAsync();
                }
            }

            var createdOrderDto = new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CustomerName = shippingFullName ?? "Unknown",
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                CreatedAt = order.CreatedAt
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId },
                ResponseDto<OrderDto>.SuccessResponse(createdOrderDto, "Tạo đơn hàng thành công"));
        }

        // POST: api/orders/5/status
        [HttpPost("{id}/status")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<OrderDto>>> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound(ResponseDto<OrderDto>.ErrorResponse("Đơn hàng không tồn tại"));

            if (!string.IsNullOrEmpty(dto.PaymentStatus))
                order.PaymentStatus = dto.PaymentStatus;

            if (!string.IsNullOrEmpty(dto.OrderStatus))
                order.OrderStatus = dto.OrderStatus;

            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse(null, "Cập nhật trạng thái thành công"));
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Đơn hàng không tồn tại"));

            if (order.OrderStatus != "cancelled")
            {
                foreach (var item in order.Items)
                {
                    if (item.ProductId.HasValue)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        if (product != null) product.StockQuantity += item.Quantity;
                    }
                }
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa đơn hàng thành công"));
        }
    }
}