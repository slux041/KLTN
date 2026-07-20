namespace PetSpaAPI.DTOs.Product
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Unit { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Brand { get; set; }
    }
}