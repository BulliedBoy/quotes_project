using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
using System.Web;
using System.Text;

namespace quotes_project.Pages.Cotizaciones
{
    public class CotizacionesModel : PageModel
    {
        public string? HtmlTable { get; private set; }

        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Cotizaciones.xlsx"); // Ruta relativa al archivo Excel

            if (!System.IO.File.Exists(filePath))
            {
                HtmlTable = "<p>El archivo de Excel no se encontró.</p>";
                return;
            }

            DataTable dataTable = ReadFromExcel(filePath);

            HTMLQuoteTable htmlQuoteTable = new HTMLQuoteTable();
            HtmlTable = htmlQuoteTable.GenerateHTMLTable(dataTable);

            // Imprimir para depuración
            Debug.WriteLine(HtmlTable);
        }

        public DataTable ReadFromExcel(string filePath)
        {
            var dataTable = new DataTable();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Hoja de Excel a leer
                var range = worksheet.RangeUsed();

                // Leer la primera fila como encabezados
                foreach (var cell in range.FirstRow().CellsUsed())
                {
                    dataTable.Columns.Add(cell.Value.ToString());
                }

                // Leer los datos de las filas restantes
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var dataRow = dataTable.NewRow();
                    int i = 0;
                    foreach (var cell in row.Cells())
                    {
                        dataRow[i++] = cell.Value.ToString() ?? string.Empty; // Manejar posibles valores nulos
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
    }

    public class HTMLQuoteTable
    {
        public string GenerateHTMLTable(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return "<p>No data available to display.</p>";
            }

            var html = new StringBuilder();
            html.Append("<table border='1'>");

            // Header row
            html.Append("<tr>");
            foreach (DataColumn column in dataTable.Columns)
            {
                html.Append($"<th>{HttpUtility.HtmlEncode(column.ColumnName)}</th>");
            }
            html.Append("</tr>");

            // Data rows
            foreach (DataRow row in dataTable.Rows)
            {
                html.Append("<tr>");
                foreach (var cell in row.ItemArray)
                {
                    var cellValue = cell?.ToString() ?? string.Empty; // Manejar posibles valores nulos
                    html.Append($"<td>{HttpUtility.HtmlEncode(cellValue)}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table>");
            return html.ToString();
        }
    }
}
