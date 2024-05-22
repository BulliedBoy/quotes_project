using Microsoft.AspNetCore.Mvc.RazorPages;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace quotes_project.Views.Cotizaciones
{
    public class CotizacionesModel : PageModel
    {
        public void OnGet()
        {
            Console.WriteLine("OnGet method called"); // Línea de depuración
            try
            {
                // Ruta al archivo Excel
                var xlPath = "E:\\Source\\Cotizaciones_SQL.xlsx";
                Console.WriteLine($"Leyendo archivo Excel en: {xlPath}");
                var xlQuotes = new ExcelQuotes();
                var dataTable = xlQuotes.ReadExcel(xlPath);

                var htmlTableGenerator = new HtmlTableGenerator();
                var htmlTable = htmlTableGenerator.GenerateHtmlTable(dataTable);

                // Asignar la tabla HTML a ViewData
                ViewData["HtmlTable"] = htmlTable;

                // Depuración: imprimir el HTML generado
                Console.WriteLine("HTML Table Generated:");
                Console.WriteLine(htmlTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Para depuración
                Console.WriteLine("Stack Trace: " + ex.StackTrace); // Más detalles
                ViewData["HtmlTable"] = "<p>Error generating table.</p>";
            }
        }

    }

    public class ExcelQuotes
    {
        public DataTable ReadExcel(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var dataTable = new DataTable();

                // Añadir columnas al DataTable
                foreach (var headerCell in worksheet.Row(1).Cells())
                {
                    dataTable.Columns.Add(headerCell.Value.ToString());
                }

                // Añadir filas al DataTable
                foreach (var row in worksheet.RowsUsed().Skip(1)) // Omitir fila de títulos
                {
                    var dataRow = dataTable.NewRow();
                    int i = 0;
                    foreach (var cell in row.Cells())
                    {
                        dataRow[i++] = cell.Value.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
                return dataTable;
            }
        }
    }

    public class HtmlTableGenerator
    {
        public string GenerateHtmlTable(DataTable dataTable)
        {
            var html = new StringBuilder();

            html.Append("<table border='1'>");

            // Fila de encabezado
            html.Append("<tr>");
            foreach (DataColumn column in dataTable.Columns)
            {
                html.Append($"<th>{column.ColumnName}</th>");
            }
            html.Append("</tr>");

            // Filas de datos
            foreach (DataRow row in dataTable.Rows)
            {
                html.Append("<tr>");
                foreach (var cell in row.ItemArray)
                {
                    html.Append($"<td>{cell}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table>");
            return html.ToString();
        }
    }
}
