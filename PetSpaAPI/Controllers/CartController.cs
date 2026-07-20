using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Cart;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "customer")]
    public class CartController : ControllerBase
    {
        private readonly PetSpaDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CartController(PetSpaDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private string GetFullImageUrl(string? imageName)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            if (string.IsNullOrEmpty(imageName)) return $"{baseUrl}/images/products/default.png";
            return $"{baseUrl}/images/products/{imageName}";
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<ResponseDto<CartSummaryDto>>> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Service)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var items = cartItems.Select(c => new CartItemDto
            {
                CartItemId = c.CartItemId,
                ProductId = c.ProductId,
                ProductName = c.Product?.Name,
                ServiceId = c.ServiceId,
                ServiceName = c.Service?.Name,
                Price = c.Product?.Price ?? c.Service?.Price ?? 0,
                Quantity = c.Quantity,
                ImageUrl = GetFullImageUrl(c.Product?.ImageUrl) 
            }).ToList();

            var summary = new CartSummaryDto
            {
                Items = items,
                TotalItems = items.Sum(i => i.Quantity),
                TotalAmount = items.Sum(i => i.TotalPrice)
            };

            return Ok(ResponseDto<CartSummaryDto>.SuccessResponse(summary));
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<ActionResult<ResponseDto<CartItemDto>>> AddToCart([FromBody] AddToCartDto dto)
        {
            if (!dto.ProductId.HasValue && !dto.ServiceId.HasValue)
                return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Phải chọn sản phẩm hoặc dịch vụ"));

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Check if item already in cart
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && 
                    c.ProductId == dto.ProductId && 
                    c.ServiceId == dto.ServiceId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                await _context.SaveChangesAsync();

                var product = dto.ProductId.HasValue ? await _context.Products.FindAsync(dto.ProductId) : null;
                var service = dto.ServiceId.HasValue ? await _context.Services.FindAsync(dto.ServiceId) : null;

                return Ok(ResponseDto<CartItemDto>.SuccessResponse(new CartItemDto
                {
                    CartItemId = existingItem.CartItemId,
                    ProductId = existingItem.ProductId,
                    ProductName = product?.Name,
                    ServiceId = existingItem.ServiceId,
                    ServiceName = service?.Name,
                    Price = product?.Price ?? service?.Price ?? 0,
                    Quantity = existingItem.Quantity,
                    ImageUrl = GetFullImageUrl(product?.ImageUrl) 
                }, "Đã cập nhật số lượng trong giỏ hàng"));
            }

            // Validate product or service exists
            if (dto.ProductId.HasValue)
            {
                var product = await _context.Products.FindAsync(dto.ProductId);
                if (product == null || !product.IsActive)
                    return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Sản phẩm không tồn tại hoặc không khả dụng"));
                
                if (product.StockQuantity < dto.Quantity)
                    return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Số lượng sản phẩm không đủ"));
            }

            if (dto.ServiceId.HasValue)
            {
                var service = await _context.Services.FindAsync(dto.ServiceId);
                if (service == null || !service.IsActive)
                    return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Dịch vụ không tồn tại hoặc không khả dụng"));
            }

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                ServiceId = dto.ServiceId,
                Quantity = dto.Quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            var addedProduct = dto.ProductId.HasValue ? await _context.Products.FindAsync(dto.ProductId) : null;
            var addedService = dto.ServiceId.HasValue ? await _context.Services.FindAsync(dto.ServiceId) : null;

            var cartItemDto = new CartItemDto
            {
                CartItemId = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                ProductName = addedProduct?.Name,
                ServiceId = cartItem.ServiceId,
                ServiceName = addedService?.Name,
                Price = addedProduct?.Price ?? addedService?.Price ?? 0,
                Quantity = cartItem.Quantity,
                ImageUrl = GetFullImageUrl(addedProduct?.ImageUrl) 
            };

            return Ok(ResponseDto<CartItemDto>.SuccessResponse(cartItemDto, "Đã thêm vào giỏ hàng"));
        }

        // POST: api/cart/5/update
        [HttpPost("{id}/update")]
        public async Task<ActionResult<ResponseDto<CartItemDto>>> UpdateCartItem(int id, [FromBody] UpdateCartItemDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cartItem = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(c => c.CartItemId == id && c.UserId == userId);

            if (cartItem == null)
                return NotFound(ResponseDto<CartItemDto>.ErrorResponse("Không tìm thấy sản phẩm trong giỏ hàng"));

            if (dto.Quantity <= 0)
                return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Số lượng phải lớn hơn 0"));

            // Check stock for products
            if (cartItem.ProductId.HasValue && cartItem.Product != null)
            {
                if (cartItem.Product.StockQuantity < dto.Quantity)
                    return BadRequest(ResponseDto<CartItemDto>.ErrorResponse("Số lượng sản phẩm không đủ"));
            }

            cartItem.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();

            var cartItemDto = new CartItemDto
            {
                CartItemId = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product?.Name,
                ServiceId = cartItem.ServiceId,
                ServiceName = cartItem.Service?.Name,
                Price = cartItem.Product?.Price ?? cartItem.Service?.Price ?? 0,
                Quantity = cartItem.Quantity,
                ImageUrl = GetFullImageUrl(cartItem.Product?.ImageUrl) 
            };

            return Ok(ResponseDto<CartItemDto>.SuccessResponse(cartItemDto, "Cập nhật giỏ hàng thành công"));
        }

        // DELETE: api/cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<string>>> RemoveFromCart(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.CartItemId == id && c.UserId == userId);

            if (cartItem == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Không tìm thấy sản phẩm trong giỏ hàng"));

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Đã xóa khỏi giỏ hàng"));
        }

        // DELETE: api/cart/clear
        [HttpDelete("clear")]
        public async Task<ActionResult<ResponseDto<string>>> ClearCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cartItems = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Đã xóa toàn bộ giỏ hàng"));
        }
    }
}