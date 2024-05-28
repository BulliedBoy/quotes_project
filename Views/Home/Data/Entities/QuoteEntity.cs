namespace quotes_project.Views.Home.Data.Entities
{
    public class QuoteEntity
    {
        public int IdQuote { get; set; }
        public int IdCustomer { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int IdProduct { get; set; }
        public int IdUser { get; set; }
        public decimal Amount { get; set; }
        public DateTime DDate { get; set; }
    }
}
