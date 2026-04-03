namespace ExpenseTrackerApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
    }
}
