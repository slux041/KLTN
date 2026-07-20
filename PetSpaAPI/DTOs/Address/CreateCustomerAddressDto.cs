namespace PetSpaAPI.DTOs.Address
{
    public class CreateCustomerAddressDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;

        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;

        public string WardId { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;

        public bool IsDefault { get; set; } = false;
    }
}