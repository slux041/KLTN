namespace PetSpaAPI.DTOs.Service
{
    public class CreateServiceDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string PricingMethod { get; set; } = "fixed";
        public List<ServicePriceDto>? ServicePrices { get; set; }
    }
}