using System;
using System.Collections.Generic;

namespace GD.DTOs
{
    public class CreatePurchaseOrderDto
    {
        public int SupplierId { get; set; }
        public List<CreatePurchaseOrderItemDto> Items { get; set; }
    }

    public class CreatePurchaseOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public string ProductName { get; set; }
        public double Total => Quantity * Price;
    }

    public class PurchaseOrderDto
    {
        public int PurchaseOrderId { get; set; }
        public string SupplierName { get; set; }
        public string StaffName { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}