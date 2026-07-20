using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Promotion;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public PromotionsController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/promotions
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PromotionDto>>>> GetPromotions([FromQuery] bool? isActive = null)
        {
            var query = _context.Promotions.AsQueryable();

            if (isActive.HasValue)
                query = query.Where(p => p.IsActive == isActive.Value);

            var promotions = await query
                .OrderByDescending(p => p.StartDate)
                .Select(p => new PromotionDto
                {
                    PromotionId = p.PromotionId,
                    Code = p.Code,
                    Description = p.Description,
                    DiscountPercent = p.DiscountPercent,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsActive = p.IsActive
                })
                .ToListAsync();

            return Ok(ResponseDto<List<PromotionDto>>.SuccessResponse(promotions));
        }

        // GET: api/promotions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PromotionDto>>> GetPromotion(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
                return NotFound(ResponseDto<PromotionDto>.ErrorResponse("Khuyến mãi không tồn tại"));

            var promotionDto = new PromotionDto
            {
                PromotionId = promotion.PromotionId,
                Code = promotion.Code,
                Description = promotion.Description,
                DiscountPercent = promotion.DiscountPercent,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                IsActive = promotion.IsActive
            };

            return Ok(ResponseDto<PromotionDto>.SuccessResponse(promotionDto));
        }

        // POST: api/promotions/validate
        [HttpPost("validate")]
        public async Task<ActionResult<ResponseDto<PromotionDto>>> ValidatePromotion([FromBody] ValidatePromotionDto dto)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Code == dto.Code);

            if (promotion == null)
                return NotFound(ResponseDto<PromotionDto>.ErrorResponse("Mã khuyến mãi không tồn tại"));

            if (!promotion.IsActive)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Mã khuyến mãi không còn khả dụng"));

            var now = DateTime.Now;
            if (now < promotion.StartDate)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Mã khuyến mãi chưa có hiệu lực"));

            if (now > promotion.EndDate)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Mã khuyến mãi đã hết hạn"));

            var promotionDto = new PromotionDto
            {
                PromotionId = promotion.PromotionId,
                Code = promotion.Code,
                Description = promotion.Description,
                DiscountPercent = promotion.DiscountPercent,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                IsActive = promotion.IsActive
            };

            return Ok(ResponseDto<PromotionDto>.SuccessResponse(promotionDto, "Mã khuyến mãi hợp lệ"));
        }

        // POST: api/promotions
        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<PromotionDto>>> CreatePromotion([FromBody] CreatePromotionDto dto)
        {
            if (await _context.Promotions.AnyAsync(p => p.Code == dto.Code))
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Mã khuyến mãi đã tồn tại"));

            if (dto.DiscountPercent <= 0 || dto.DiscountPercent > 100)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Phần trăm giảm giá phải từ 0 đến 100"));

            if (dto.EndDate <= dto.StartDate)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Ngày kết thúc phải sau ngày bắt đầu"));

            var promotion = new Promotion
            {
                Code = dto.Code,
                Description = dto.Description,
                DiscountPercent = dto.DiscountPercent,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = true
            };

            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();

            var promotionDto = new PromotionDto
            {
                PromotionId = promotion.PromotionId,
                Code = promotion.Code,
                Description = promotion.Description,
                DiscountPercent = promotion.DiscountPercent,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                IsActive = promotion.IsActive
            };

            return CreatedAtAction(nameof(GetPromotion), new { id = promotion.PromotionId },
                ResponseDto<PromotionDto>.SuccessResponse(promotionDto, "Tạo khuyến mãi thành công"));
        }

        // POST: api/promotions/5/update
        [HttpPost("{id}/update")]
        [Authorize(Roles = "admin,staff")]
        public async Task<ActionResult<ResponseDto<PromotionDto>>> UpdatePromotion(int id, [FromBody] UpdatePromotionDto dto)
        {
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
                return NotFound(ResponseDto<PromotionDto>.ErrorResponse("Khuyến mãi không tồn tại"));

            if (dto.Description != null)
                promotion.Description = dto.Description;

            if (dto.DiscountPercent.HasValue)
            {
                if (dto.DiscountPercent.Value <= 0 || dto.DiscountPercent.Value > 100)
                    return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Phần trăm giảm giá phải từ 0 đến 100"));
                promotion.DiscountPercent = dto.DiscountPercent.Value;
            }

            if (dto.StartDate.HasValue)
                promotion.StartDate = dto.StartDate.Value;

            if (dto.EndDate.HasValue)
                promotion.EndDate = dto.EndDate.Value;

            if (dto.IsActive.HasValue)
                promotion.IsActive = dto.IsActive.Value;

            if (promotion.EndDate <= promotion.StartDate)
                return BadRequest(ResponseDto<PromotionDto>.ErrorResponse("Ngày kết thúc phải sau ngày bắt đầu"));

            await _context.SaveChangesAsync();

            var promotionDto = new PromotionDto
            {
                PromotionId = promotion.PromotionId,
                Code = promotion.Code,
                Description = promotion.Description,
                DiscountPercent = promotion.DiscountPercent,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                IsActive = promotion.IsActive
            };

            return Ok(ResponseDto<PromotionDto>.SuccessResponse(promotionDto, "Cập nhật khuyến mãi thành công"));
        }

        // DELETE: api/promotions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeletePromotion(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Khuyến mãi không tồn tại"));

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa khuyến mãi thành công"));
        }
    }
}