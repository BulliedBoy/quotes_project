using System;
using System.Linq;
using System.Text;
using System.Web;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;
using System.Collections.Generic;

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

        public void LoadData()
        {
            try
            {
                var quotes = _context.QuoteEntity.ToList(); // Obtiene las cotizaciones desde la base de datos
                if (quotes == null || !quotes.Any())
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
            public string GenerateHTMLTable(List<QuoteEntity> quotes)
            {
                if (quotes == null || !quotes.Any())
                {
                    return "<p>No hay datos disponibles para mostrar.</p>";
                }

                var html = new StringBuilder();
                html.Append("<table class='table table-bordered table-striped text-center align-middle'>");
                html.Append("<thead>");
                html.Append("<tr>");
                html.Append("<th>No. de Cotización</th>");
                html.Append("<th>Cliente</th>");
                html.Append("<th>Tipo de Producto</th>");
                html.Append("<th>Usuario</th>");
                html.Append("<th>Monto</th>");
                html.Append("<th>Fecha</th>");
                html.Append("<th>Acciones</th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody>");

                foreach (var quote in quotes)
                {
                    html.Append("<tr>");
                    html.Append($"<td class='align-middle'>{quote.IdQuote}</td>");
                    html.Append($"<td class='align-middle'>{quote.CustomerName}</td>");
                    html.Append($"<td class='align-middle'>{quote.Product}</td>");
                    html.Append($"<td class='align-middle'>{quote.User}</td>");
                    html.Append($"<td class='align-middle'>{quote.Amount.ToString("F2")}</td>");
                    html.Append($"<td class='align-middle'>{quote.DDate.ToString("dd/MM/yyyy")}</td>");
                    html.Append("<td class='d-flex justify-content-center align-items-center'>");
                    html.Append("<button class='btn btn-primary btn-sm fixed-size-button'>Editar</button>");
                    html.Append("<button class='btn btn-success btn-sm fixed-size-button'>Descargar</button>");
                    html.Append("</td>");
                    html.Append("</tr>");
                }

                html.Append("</tbody>");
                html.Append("</table>");

                return html.ToString();
            }
        }
    }
}
