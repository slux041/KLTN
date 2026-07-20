using System;

namespace GD.DTOs
{
    public class TransactionDto
    {
        public string Type { get; set; } 
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public int ReferenceId { get; set; }
    }
}