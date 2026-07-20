using System;

namespace GD.DTOs
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
    }

    public class CreateSupplierDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
    }

    public class UpdateSupplierDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
    }
}
