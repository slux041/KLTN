namespace PetSpaAPI.DTOs.Promotion
{
    public class UpdatePromotionDto
    {
        public string? Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}   