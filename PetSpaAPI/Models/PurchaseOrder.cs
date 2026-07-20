using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public int StaffId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [JsonIgnore]
        public Supplier? Supplier { get; set; }
        
        [JsonIgnore]
        public User? Staff { get; set; }
        
        [JsonIgnore]
        public ICollection<PurchaseOrderItem>? Items { get; set; }
    }
}