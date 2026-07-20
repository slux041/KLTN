namespace PetSpaAPI.DTOs.Pet
{
    public class UpdatePetDto
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Breed { get; set; }
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }
    }
}