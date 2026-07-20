namespace PetSpaAPI.DTOs.Product
{
    public class UpdateProductDto
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Unit { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? Brand { get; set; }
    }
}