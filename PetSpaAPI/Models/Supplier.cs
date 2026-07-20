using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BankAccount { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}