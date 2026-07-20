namespace PetSpaAPI.DTOs.Service
{
    public class ServicePriceDto
    {
        public int PriceId { get; set; }
        public string PetType { get; set; } = string.Empty;
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal Price { get; set; }
    }
}
