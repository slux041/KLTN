using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Category;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public CategoriesController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CategoryDto>>>> GetCategories(
            [FromQuery] string? type = null,
            [FromQuery] bool? isActive = null)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                query = query.Where(c => c.Type == type);

            if (isActive.HasValue)
                query = query.Where(c => c.IsActive == isActive.Value);

            var categories = await query
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Type = c.Type,
                    Description = c.Description,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            return Ok(ResponseDto<List<CategoryDto>>.SuccessResponse(categories));
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound(ResponseDto<CategoryDto>.ErrorResponse("Danh mục không tồn tại"));

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Type = category.Type,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return Ok(ResponseDto<CategoryDto>.SuccessResponse(categoryDto));
        }

        // POST: api/categories
        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
                IsActive = true
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Type = category.Type,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId },
                ResponseDto<CategoryDto>.SuccessResponse(categoryDto, "Tạo danh mục thành công"));
        }

        // POST: api/categories/5/update
        [HttpPost("{id}/update")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<CategoryDto>>> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound(ResponseDto<CategoryDto>.ErrorResponse("Danh mục không tồn tại"));

            if (!string.IsNullOrEmpty(dto.Name))
                category.Name = dto.Name;

            if (dto.Description != null)
                category.Description = dto.Description;

            if (dto.IsActive.HasValue)
                category.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Type = category.Type,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return Ok(ResponseDto<CategoryDto>.SuccessResponse(categoryDto, "Cập nhật danh mục thành công"));
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Danh mục không tồn tại"));

            // Check if category has products or services
            var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == id);
            var hasServices = await _context.Services.AnyAsync(s => s.CategoryId == id);

            if (hasProducts || hasServices)
                return BadRequest(ResponseDto<string>.ErrorResponse("Không thể xóa danh mục đang có sản phẩm hoặc dịch vụ"));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa danh mục thành công"));
        }
    }
}