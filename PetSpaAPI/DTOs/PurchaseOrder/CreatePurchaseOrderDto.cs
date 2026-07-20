using System.Collections.Generic;

namespace PetSpaAPI.DTOs.PurchaseOrder
{
    public class CreatePurchaseOrderDto
    {
        public int SupplierId { get; set; }
        public List<CreatePurchaseOrderItemDto> Items { get; set; } = new();
    }
}