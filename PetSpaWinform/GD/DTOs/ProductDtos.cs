using System;

namespace GD.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; }
    }

    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; }
    }

    public class UpdateProductDto : CreateProductDto
    {
    }
}
