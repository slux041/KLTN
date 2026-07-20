using System.Collections.Generic;

namespace GD.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string PricingMethod { get; set; }
        public List<ServicePriceDto> ServicePrices { get; set; }
    }

    public class ServicePriceDto
    {
        public int PriceId { get; set; }
        public int ServiceId { get; set; }
        public string PetType { get; set; }
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
        public double Price { get; set; }
    }

    public class CreateServiceDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string PricingMethod { get; set; }
        public List<ServicePriceDto> ServicePrices { get; set; }
    }

    public class UpdateServiceDto
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public int? DurationMinutes { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string PricingMethod { get; set; }
        public List<ServicePriceDto> ServicePrices { get; set; }
    }
}