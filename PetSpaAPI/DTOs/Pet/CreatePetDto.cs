namespace PetSpaAPI.DTOs.Pet
{
    public class CreatePetDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Breed { get; set; }
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }
    }
}