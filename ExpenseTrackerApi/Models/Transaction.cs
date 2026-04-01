namespace ExpenseTrackerApi.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }        
        public decimal Amount { get; set; }
        public required string Type { get; set; }
        public DateTime Date { get; set; }

        //Foregin Key
        public int UserId { get; set; }
        public User ? User { get; set; }

        //Foreign Key
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }
    }
}
