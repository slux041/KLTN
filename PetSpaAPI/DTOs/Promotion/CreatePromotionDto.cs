namespace PetSpaAPI.DTOs.Promotion
{
    public class CreatePromotionDto
    {
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}