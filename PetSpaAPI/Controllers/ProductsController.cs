using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Product;
using PetSpaAPI.DTOs.Common;
using System.Globalization;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly PetSpaDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(PetSpaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GetFullImageUrl(string? imageName)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            if (string.IsNullOrEmpty(imageName))
            {
                return $"{baseUrl}/images/products/default.jpg";
            }

            return $"{baseUrl}/images/products/{imageName}";
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ResponseDto<List<string>>>> GetBrands()
        {
            var brands = await _context.Products
                .Where(p => !string.IsNullOrEmpty(p.Brand))
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(b => b)
                .ToListAsync();

            return Ok(ResponseDto<List<string>>.SuccessResponse(brands!));
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto<ProductDto>>>> GetProducts(
            [FromQuery] int? categoryId = null,
            [FromQuery] string? search = null,
            [FromQuery] string? brand = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(p => p.Brand == brand);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search));

            if (isActive.HasValue)
                query = query.Where(p => p.IsActive == isActive.Value);

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category!.Name,
                    Name = p.Name,
                    Price = p.Price,
                    Unit = p.Unit,
                    StockQuantity = p.StockQuantity,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Brand = p.Brand,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            foreach (var item in products)
            {
                item.ImageUrl = GetFullImageUrl(item.ImageUrl);
            }

            var result = new PaginationDto<ProductDto>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(ResponseDto<PaginationDto<ProductDto>>.SuccessResponse(result));
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ProductDto>>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound(ResponseDto<ProductDto>.ErrorResponse("Sản phẩm không tồn tại"));

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                CategoryName = product.Category!.Name,
                Name = product.Name,
                Price = product.Price,
                Unit = product.Unit,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = GetFullImageUrl(product.ImageUrl),
                Brand = product.Brand,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };

            return Ok(ResponseDto<ProductDto>.SuccessResponse(productDto));
        }

        // POST: api/products
        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<ProductDto>>> CreateProduct([FromForm] CreateProductDto dto, IFormFile? imageFile)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!categoryExists)
                return BadRequest(ResponseDto<ProductDto>.ErrorResponse("Danh mục không tồn tại"));

            string? fileName = null;
            
            if (imageFile != null && imageFile.Length > 0)
            {
                var rootPath = _configuration["UploadPath"];
                var folderPath = Path.Combine(rootPath, "products");
                
                var extension = Path.GetExtension(imageFile.FileName);
                fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(folderPath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }

            string? formattedBrand = string.IsNullOrWhiteSpace(dto.Brand)
                ? null
                : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dto.Brand.Trim().ToLower());

            var product = new Product
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Price = dto.Price,
                Unit = dto.Unit,
                StockQuantity = dto.StockQuantity,
                Description = dto.Description,
                ImageUrl = fileName,
                Brand = formattedBrand,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                CategoryName = category!.Name,
                Name = product.Name,
                Price = product.Price,
                Unit = product.Unit,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = GetFullImageUrl(product.ImageUrl),
                Brand = product.Brand,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId },
                ResponseDto<ProductDto>.SuccessResponse(productDto, "Tạo sản phẩm thành công"));
        }

        // POST: api/products/5/update
        [HttpPost("{id}/update")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<ProductDto>>> UpdateProduct(int id, [FromForm] UpdateProductDto dto, IFormFile? imageFile)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound(ResponseDto<ProductDto>.ErrorResponse("Sản phẩm không tồn tại"));

            if (imageFile != null && imageFile.Length > 0)
            {
                var rootPath = _configuration["UploadPath"]; // Lấy đường dẫn từ Program.cs
                var folderPath = Path.Combine(rootPath, "products");

                if (!string.IsNullOrEmpty(product.ImageUrl) && !product.ImageUrl.StartsWith("http"))
                {
                    var oldPath = Path.Combine(folderPath, product.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                var extension = Path.GetExtension(imageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(folderPath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.ImageUrl = fileName;
            }

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId.Value);
                if (!categoryExists) return BadRequest(ResponseDto<ProductDto>.ErrorResponse("Danh mục không tồn tại"));
                product.CategoryId = dto.CategoryId.Value;
            }

            if (!string.IsNullOrEmpty(dto.Name)) product.Name = dto.Name;
            if (dto.Price.HasValue) product.Price = dto.Price.Value;
            if (dto.Unit != null) product.Unit = dto.Unit;
            if (dto.StockQuantity.HasValue) product.StockQuantity = dto.StockQuantity.Value;
            if (dto.Description != null) product.Description = dto.Description;
            if (dto.IsActive.HasValue) product.IsActive = dto.IsActive.Value;

            if (dto.Brand != null)
            {
                product.Brand = string.IsNullOrWhiteSpace(dto.Brand)
                    ? null
                    : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dto.Brand.Trim().ToLower());
            }

            await _context.SaveChangesAsync();

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                CategoryName = product.Category!.Name,
                Name = product.Name,
                Price = product.Price,
                Unit = product.Unit,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = GetFullImageUrl(product.ImageUrl),
                Brand = product.Brand,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };

            return Ok(ResponseDto<ProductDto>.SuccessResponse(productDto, "Cập nhật sản phẩm thành công"));
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Sản phẩm không tồn tại"));

            bool hasPurchaseOrders = await _context.PurchaseOrderItems.AnyAsync(po => po.ProductId == id);
            bool hasOrders = await _context.OrderItems.AnyAsync(o => o.ProductId == id);

            if (hasPurchaseOrders || hasOrders)
            {
                product.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(ResponseDto<string>.SuccessResponse("",
                    "Sản phẩm này đã có giao dịch (nhập/bán) nên không thể xóa vĩnh viễn. Hệ thống đã chuyển sang trạng thái 'Ngưng bán' để bảo toàn dữ liệu."));
            }

            try
            {
                if (!string.IsNullOrEmpty(product.ImageUrl) && !product.ImageUrl.StartsWith("http"))
                {
                    var rootPath = _configuration["UploadPath"];
                    var oldPath = Path.Combine(rootPath, "products", product.ImageUrl);
                    
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(ResponseDto<string>.SuccessResponse("", "Đã xóa vĩnh viễn sản phẩm và hình ảnh."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto<string>.ErrorResponse($"Lỗi khi xóa: {ex.Message}"));
            }
        }

        // GET: api/products/low-stock
        [HttpGet("low-stock")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<List<ProductDto>>>> GetLowStockProducts([FromQuery] int threshold = 10)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.StockQuantity <= threshold && p.IsActive)
                .ToListAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                CategoryId = p.CategoryId,
                CategoryName = p.Category!.Name,
                Name = p.Name,
                Price = p.Price,
                Unit = p.Unit,
                StockQuantity = p.StockQuantity,
                Description = p.Description,
                ImageUrl = GetFullImageUrl(p.ImageUrl),
                Brand = p.Brand,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt
            }).ToList();

            return Ok(ResponseDto<List<ProductDto>>.SuccessResponse(productDtos));
        }
    }
}