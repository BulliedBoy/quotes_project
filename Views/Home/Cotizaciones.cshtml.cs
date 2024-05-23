using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text;
using System.Diagnostics; // Para depuración

namespace quotes_project.Pages.Cotizaciones
{

        public void OnGet()
        {
            string connectionString = "YourConnectionStringHere"; // Actualiza con tu cadena de conexión
            string query = "SELECT * FROM YourTableNameHere"; // Actualiza con tu consulta

            SqlReader sqlReader = new SqlReader();
            DataTable dataTable = sqlReader.ReadFromDatabase(connectionString, query);

            HTMLQuoteTable htmlQuoteTable = new HTMLQuoteTable();
            HtmlTable = htmlQuoteTable.GenerateHTMLTable(dataTable);

            // Imprimir para depuración
            Debug.WriteLine(HtmlTable);
        }
    }

    public class SqlReader
    {
        public DataTable ReadFromDatabase(string connectionString, string query)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
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
                html.Append($"<th>{column.ColumnName}</th>");
            }
            html.Append("</tr>");

            // Data rows
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