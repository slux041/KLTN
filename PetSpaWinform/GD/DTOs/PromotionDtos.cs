using System;

namespace GD.DTOs
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreatePromotionDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdatePromotionDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
