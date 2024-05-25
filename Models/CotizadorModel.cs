using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace quotes_project.Models
{
	public class CotizadorModel
	{
		public List<string> Clientes { get; set; }
		public List<string> Usuarios { get; set; }

		public CotizadorModel()
		{
			ExcelList();
		}

		public void ExcelList()
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "Data", "Cotizaciones_SQL.xlsx");
			Clientes = ClientesExcel(filePath);
			Usuarios = UsuariosExcel(filePath);
		}

		private List<string> ClientesExcel(string filePath)
		{
			var clientes = new List<string>();

			// Leer datos del archivo Excel
			using (var workbook = new XLWorkbook(filePath))
			{
				var custws = workbook.Worksheet("CLIENTES");
				var custrng = custws.RangeUsed();

				foreach (var row in custrng.RowsUsed())
				{
					var cliente = row.Cell(1).Value.ToString().Trim();
					clientes.Add(cliente);
				}
			}

			return clientes;
		}

		private List<string> UsuariosExcel(string filePath)
		{
			var usuarios = new List<string>();

			// Leer datos del archivo Excel
			using (var workbook = new XLWorkbook(filePath))
			{
				var usuws = workbook.Worksheet("USUARIOS");
				var usurng = usuws.RangeUsed();

				foreach (var row in usurng.RowsUsed())
				{
					var usuario = row.Cell(1).Value.ToString().Trim();
					usuarios.Add(usuario);
				}
			}
			return usuarios;
		}
	}
}