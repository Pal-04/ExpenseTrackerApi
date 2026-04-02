namespace ExpenseTrackerApi.DTOs
{
    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public required string Type { get; set; }
        public string ? CategoryName { get; set; }
        public DateTime Date { get; set; }
    }
}
