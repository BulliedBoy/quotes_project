using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace quotes_project.Views.Home
{
    public class CotizacionesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

public class SqlReader
{
    public DataTable ReadFromDatabase(string.connectionString,string query)
    {
        var dataTable= new DataTable();
        using (var connection = new SqlConnection(connectionString))
        {
            using var command = new SqlCommand(query, connection))
                {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    DataTable.load(reader);
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
        var html = new StringBuilder();
        html.Append("<table border='1'>");

        //Header row
        html.Append("<tr>");
        foreach (DataColumn column in dataTable.Columns)
        {
            html.Append($"<th>{column.ColumnName}</th>");
        }
        html.Append("</tr>");

        // Data rows
        foreach (var cell in row.itemarray)

    }
}
