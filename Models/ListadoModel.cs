using System;
using System.Collections.Generic;
using System.Linq;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;

namespace quotes_project.Models
{
    public class ListadoModel
    {
        public List<QuoteDto> Quotes { get; set; } = new List<QuoteDto>();

        private readonly ApplicationDbContext _context;

        public ListadoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void LoadData()
        {
            try
            {
                var quotes = _context.QuoteEntity.ToList();
                if (quotes == null || !quotes.Any())
                {
                    Quotes = new List<QuoteDto>();
                    return;
                }

                Quotes = quotes.Select(q => new QuoteDto
                {
                    Id = q.Id,
                    Name = q.Name,
                    Product = q.Product,
                    User = q.User,
                    Amount = q.Amount,
                    DDate = q.DDate
                }).ToList();
            }
            catch (Exception ex)
            {
                // Manejar excepciones según sea necesario
                throw new Exception($"Error al cargar los datos: {ex.Message}", ex);
            }
        }
    }

    public class QuoteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Product { get; set; }
        public string? User { get; set; }
        public decimal Amount { get; set; }
        public DateTime DDate { get; set; }
    }
}