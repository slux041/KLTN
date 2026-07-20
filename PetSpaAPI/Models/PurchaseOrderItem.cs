using System.Text.Json.Serialization;

namespace PetSpaAPI.Models
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        [JsonIgnore]
        public PurchaseOrder? PurchaseOrder { get; set; }
        
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}