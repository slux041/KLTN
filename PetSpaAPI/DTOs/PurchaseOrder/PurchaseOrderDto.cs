using System.Collections.Generic;

namespace PetSpaAPI.DTOs.PurchaseOrder
{
    public class PurchaseOrderDto
    {
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public int StaffId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new();
    }
}