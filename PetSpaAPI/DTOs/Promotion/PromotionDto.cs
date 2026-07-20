namespace PetSpaAPI.DTOs.Promotion
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid => IsActive && DateTime.Now >= StartDate && DateTime.Now <= EndDate;
    }
}