namespace PetSpaAPI.DTOs.Transaction
{
    public class TransactionDto
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public int ReferenceId { get; set; }
    }
}
