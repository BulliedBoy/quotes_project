using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using System.Web;

namespace quotes_project.Models
{
    public class CotizacionesModel : PageModel
    {
        public string HtmlTable { get; private set; } = string.Empty;

        public void OnGet()
        {
            // Ruta del archivo Excel
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "Data", "Cotizaciones_SQL.xlsx");

            // Verificar si el archivo existe
            if (!System.IO.File.Exists(filePath))
            {
                // Si el archivo no existe, asignar un mensaje de error al HtmlTable y salir del método
                HtmlTable = "<p>El archivo de Excel no se encontró.</p>";
                return;
            }

            // Leer los datos del archivo Excel
            DataTable dataTable = ReadFromExcel(filePath);

            // Generar la tabla HTML a partir de los datos
            HTMLQuoteTable htmlQuoteTable = new HTMLQuoteTable();
            HtmlTable = htmlQuoteTable.GenerateHTMLTable(dataTable);

            // Registrar la tabla HTML en la consola para depuración
            Debug.WriteLine(HtmlTable);
        }

        public DataTable ReadFromExcel(string filePath)
        {
            var dataTable = new DataTable();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                foreach (var cell in range.FirstRow().CellsUsed())
                {
                    dataTable.Columns.Add(cell.GetString());
                }

                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var dataRow = dataTable.NewRow();
                    int i = 0;
                    foreach (var cell in row.Cells())
                    {
                        dataRow[i++] = cell.GetString();
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

            html.Append("<tr>");
            foreach (DataColumn column in dataTable.Columns)
            {
                html.Append($"<th>{HttpUtility.HtmlEncode(column.ColumnName)}</th>");
            }
            html.Append("</tr>");

            foreach (DataRow row in dataTable.Rows)
            {
                html.Append("<tr>");
                foreach (var cell in row.ItemArray)
                {
                    var cellValue = cell?.ToString() ?? string.Empty;
                    html.Append($"<td>{HttpUtility.HtmlEncode(cellValue)}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table>");
            return html.ToString();
        }
    }
}
