namespace quotes_project.Views.Home.Data.Entities
{
    public class QuoteEntity
    {
        public int IdQuote { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DDate { get; set; }
    }
}
