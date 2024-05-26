using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using System.Web;
using System.Collections.Generic;

namespace quotes_project.Models
{
    public class ListadoModel
    {
        public string HtmlTable { get; set; } = string.Empty;

        public void LoadData()
        {
            // Ruta del archivo Excel
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "Data", "Cotizaciones_SQL.xlsx");

            // Verificar si el archivo existe
            if (!File.Exists(filePath))
            {
                // Si el archivo no existe, asignar un mensaje de error al HtmlTable y salir del método
                HtmlTable = "<p>El archivo de Excel no se encontró o no cuenta con acceso.</p>";
                return;
            }

            // Leer los datos del archivo Excel
            DataTable dataTable = ReadFromExcel(filePath);

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                // Si no se pudo leer o no hay datos, asignar un mensaje de error al HtmlTable y salir del método
                HtmlTable = "<p>No se pudieron leer los datos del archivo de Excel.</p>";
                return;
            }

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
                var worksheet = workbook.Worksheet("LISTADO");
                var range = worksheet.RangeUsed();

                var columnNames = new HashSet<string>();
                foreach (var cell in range.FirstRow().CellsUsed())
                {
                    string columnName = cell.GetString();
                    if (columnNames.Contains(columnName))
                    {
                        int suffix = 1;
                        string uniqueName;
                        do
                        {
                            uniqueName = $"{columnName}_{suffix++}";
                        } while (columnNames.Contains(uniqueName));
                        columnName = uniqueName;
                    }
                    columnNames.Add(columnName);
                    dataTable.Columns.Add(columnName);
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

        public class HTMLQuoteTable
        {
            public string GenerateHTMLTable(DataTable dataTable)
            {
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return "<p>No data available to display.</p>";
                }

                var html = new StringBuilder();
                html.Append("<table class='quote-list'>");

                html.Append("<tr>");
                foreach (DataColumn column in dataTable.Columns)
                {
                    html.Append($"<th>{HttpUtility.HtmlEncode(column.ColumnName)}</th>");
                }
                html.Append("</tr>");

                foreach (DataRow row in dataTable.Rows)
                {
                    html.Append("<tr>");
                    int columnIndex = 0;
                    foreach (var cell in row.ItemArray)
                    {
                        var cellValue = cell?.ToString() ?? string.Empty;
                        // Comprueba si estamos en la columna 11 o posterior para la vista resumida
                        if (columnIndex==3)
                        {
							html.Append($"<td><a href='/Home/Cotizador?id={HttpUtility.UrlEncode(cellValue)}'>{HttpUtility.HtmlEncode(cellValue)}</a></td>");
						}
                        else
                        {
                            // Si no estamos en la columna 11 o posterior, mostrar el valor normal
                            html.Append($"<td>{HttpUtility.HtmlEncode(cellValue)}</td>");
                        }
                        columnIndex++;
                    }
                    html.Append("</tr>");
                }

                html.Append("</table>");
                return html.ToString();
            }
        }
    }
}