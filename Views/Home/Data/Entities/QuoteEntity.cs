namespace quotes_project.Views.Home.Data.Entities
{
    public class QuoteEntity
    {
        public int Id { get; set; }

        public string? Client { get; set; }

        public string? Product { get; set; }

        public string? User { get; set; }

        public decimal Amount { get; set; }

        public DateTime DDate { get; set; }
    }
}