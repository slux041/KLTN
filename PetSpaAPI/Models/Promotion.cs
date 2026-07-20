namespace PetSpaAPI.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}