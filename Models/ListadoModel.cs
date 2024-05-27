using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using quotes_project.Data;

namespace quotes_project.Models
{
    public class ListadoModel
    {
        public string HtmlTable { get; set; } = string.Empty;

        private readonly ApplicationDbContext _context;

        public ListadoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ListadoModel()
        {
        }

        public void LoadData()
        {
            var quotes = _context.Quotes.ToList();
            if (quotes == null || quotes.Count == 0)
            {
                HtmlTable = "<p>No se encontraron cotizaciones en la base de datos.</p>";
                return;
            }

            // Generar la tabla HTML a partir de los datos
            HTMLQuoteTable htmlQuoteTable = new HTMLQuoteTable();
            HtmlTable = htmlQuoteTable.GenerateHTMLTable(quotes);

            // Registrar la tabla HTML en la consola para depuración
            System.Diagnostics.Debug.WriteLine(HtmlTable);
        }

        public class HTMLQuoteTable
        {
            public string GenerateHTMLTable(List<Quote> quotes)
            {
                if (quotes == null || !quotes.Any())
                {
                    return "<p>No data available to display.</p>";
                }

                var html = new StringBuilder();
                html.Append("<table class='quote-list'>");

                html.Append("<tr>");
                html.Append("<th>No. de Cotizacion</th>");
                html.Append("<th>No. de Cliente</th>");
                html.Append("<th>Tipo de Producto</th>");
                html.Append("<th>Usuario</th>");
                html.Append("<th>Monto</th>");
                html.Append("<th>Fecha</th>");
                html.Append("</tr>");

                foreach (var quote in quotes)
                {
                    html.Append("<tr>");
                    html.Append($"<td>{quote.id_quote}</td>");
                    html.Append($"<td>{quote.id_customer}</td>");
                    html.Append($"<td>{quote.id_producto}</td>");
                    html.Append($"<td>{quote.id_user}</td>");
                    html.Append($"<td>{quote.amount}</td>");
                    html.Append($"<td>{quote.dDate}</td>");
                    html.Append("</tr>");
                }

            html.Append("</table>");
                return html.ToString();
            }
        }
    }
}
