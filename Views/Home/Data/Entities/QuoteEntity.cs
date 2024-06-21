﻿namespace quotes_project.Views.Home.Data.Entities
{
    public class QuoteEntity
    {
        public int IdQuote { get; set; }

        public string? CustomerName { get; set; }

        public string? Product { get; set; }

        public string? User { get; set; }

        public decimal Amount { get; set; }

        public DateTime DDate { get; set; }
    }
}