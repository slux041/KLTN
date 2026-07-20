namespace PetSpaAPI.DTOs.Service
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string PricingMethod { get; set; } = "fixed";
        public List<ServicePriceDto>? ServicePrices { get; set; }
    }
}