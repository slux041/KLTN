namespace PetSpaAPI.DTOs.Service
{
    public class UpdateServiceDto
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public int? DurationMinutes { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? PricingMethod { get; set; }
        public List<ServicePriceDto>? ServicePrices { get; set; }
    }
}