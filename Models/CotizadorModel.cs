using System.Text;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities; // Asegúrate de importar el espacio de nombres correcto

namespace quotes_project.Models
{
    public class CotizadorModel
    {
        public List<CustomerEntity> CustomerEntity { get; set; } = new List<CustomerEntity>();
        public List<LocalProductEntity> LocalProductEntity { get; set; } = new List<LocalProductEntity>();
        public List<UserEntity> UserEntity { get; set; } = new List<UserEntity>();

        public string HtmlTable { get; set; } = string.Empty;

        private readonly ApplicationDbContext _context;

        public CotizadorModel(ApplicationDbContext context)
        {
            _context = context;
            CustomerEntity = new List<CustomerEntity>();
            LocalProductEntity = new List<LocalProductEntity>();
            UserEntity = new List<UserEntity>();
            LoadDropdownData();
        }

        public void LoadDropdownData()
        {
            CustomerEntity = _context.CustomerEntity.ToList();
            LocalProductEntity = _context.LocalProductEntity.ToList();
            UserEntity = _context.UserEntity.ToList();
        }
        public void LoadData()
        {
            try
            {
                var quotes = _context.QuoteEntity.ToList(); // Corrección aquí
                if (quotes == null || !quotes.Any()) // Corrección aquí
                {
                    HtmlTable = "<p>No se encontraron cotizaciones en la base de datos.</p>";
                    return;
                }

                HTMLQuoteTable htmlQuoteTable = new HTMLQuoteTable();
                HtmlTable = htmlQuoteTable.GenerateHTMLTable(quotes);
            }
            catch (Exception ex)
            {
                // Manejar excepciones y asignar un mensaje de error
                HtmlTable = $"<p>Error al cargar los datos: {HttpUtility.HtmlEncode(ex.Message)}</p>";
            }
        }

        public class HTMLQuoteTable
        {
            public string GenerateHTMLTable(List<QuoteEntity> quotes) // Cambio aquí
            {
                if (quotes == null || !quotes.Any())
                {
                    return "<p>No hay datos disponibles para mostrar.</p>";
                }

                var html = new StringBuilder();
                html.Append("<table class='quote-list'>");
                html.Append("<tr>");
                html.Append("<th>No. de Cotización</th>");
                html.Append("<th>Tipo de Producto</th>");
                html.Append("<th>Usuario</th>");
                html.Append("<th>Monto</th>");
                html.Append("<th>Fecha</th>");
                html.Append("</tr>");

                foreach (var quote in quotes)
                {
                    html.Append("<tr>");
                    html.Append($"<td>{quote.IdQuote}</td>");
                    html.Append($"<td>{quote.Product}</td>");
                    html.Append($"<td>{quote.User}</td>");
                    html.Append($"<td>{quote.Amount}</td>");
                    html.Append($"<td>{quote.DDate}</td>");
                    html.Append("</tr>");
                }

                html.Append("</table>");
                return html.ToString();
            }
        }
    }
}
