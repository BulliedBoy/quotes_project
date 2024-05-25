using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace quotes_project.Models
{
	public class CotizadorModel
	{
		public List<string> Clientes { get; set; }

		public CotizadorModel()
		{
			CustList();
		}

		public void CustList()
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Cotizaciones_SQL.xlsx");
			Clientes = LeerClientesDesdeExcel(filePath);
		}

		private List<string> LeerClientesDesdeExcel(string filePath)
		{
			var clientes = new List<string>();

			// Leer datos del archivo Excel
			using (var workbook = new XLWorkbook(filePath))
			{
				var worksheet = workbook.Worksheet("CLIENTES");
				var range = worksheet.RangeUsed();

				foreach (var cell in range.RowsUsed().Skip(1))
				{
					var cliente = cell.Cell(1).Value.ToString().Trim();
					clientes.Add(cliente);
				}
			}

			return clientes;
		}
	}
}
