namespace ExpenseTrackerApi.DTOs
{
    public class UpdateTransactionDto
    {
        public decimal Amount { get; set; }
        public required string Type { get; set; }
        public int CategoryId { get; set; }
    }
}
