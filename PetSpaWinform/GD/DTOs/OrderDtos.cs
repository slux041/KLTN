using System;
using System.Collections.Generic;

namespace GD.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; } 
        public string CustomerPhone { get; set; } 
        public int? ShippingAddressId { get; set; }
        public string ShippingFullName { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingAddressLine { get; set; }
        public string ShippingProvinceName { get; set; }
        public string ShippingWardName { get; set; }
        public double TotalAmount { get; set; }
        public double SubTotal { get; set; }
        public double ShippingFee { get; set; }
        public double DiscountAmount { get; set; }
        public string PaymentMethod { get; set; } 
        public string PaymentStatus { get; set; } 
        public string OrderStatus { get; set; }   
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; } 
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; } 
        public int Quantity { get; set; }
        public double Price { get; set; }       
        public double TotalPrice => Quantity * Price;
    }

    public class CreateOrderDto
    {
        public int? UserId { get; set; }
        public double TotalAmount { get; set; }
        public double SubTotal { get; set; }
        public double ShippingFee { get; set; } = 0;
        public double DiscountAmount { get; set; } = 0;

        public string PaymentMethod { get; set; } = "Tiền mặt";
        public string OrderStatus { get; set; } = "completed"; 
        public string PaymentStatus { get; set; } = "paid";
        
        public string ShippingFullName { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingAddressLine { get; set; }
        
        public string Note { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }

    public class CreateOrderItemDto
    {
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class UpdateOrderDto
    {
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public double? TotalAmount { get; set; }
    }
    
    public class UpdateOrderStatusDto
    {
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
    }
}