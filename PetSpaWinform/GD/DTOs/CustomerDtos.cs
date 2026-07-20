using System;

namespace GD.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int TotalPets { get; set; }
        public int TotalAppointments { get; set; }
    }

    public class CreateCustomerDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Status { get; set; }
    }
}