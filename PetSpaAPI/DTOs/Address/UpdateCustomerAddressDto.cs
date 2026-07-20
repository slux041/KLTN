namespace PetSpaAPI.DTOs.Address
{
    public class UpdateCustomerAddressDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? AddressLine { get; set; }

        public string? ProvinceId { get; set; }
        public string? ProvinceName { get; set; }

        public string? WardId { get; set; }
        public string? WardName { get; set; }

        public bool? IsDefault { get; set; }
    }
}