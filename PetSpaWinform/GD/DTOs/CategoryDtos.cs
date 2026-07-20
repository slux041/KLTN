namespace GD.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}