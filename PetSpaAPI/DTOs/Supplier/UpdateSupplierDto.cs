namespace PetSpaAPI.DTOs.Supplier
{
    public class UpdateSupplierDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BankAccount { get; set; }
    }
}