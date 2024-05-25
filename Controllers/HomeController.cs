using Microsoft.AspNetCore.Mvc;
using quotes_project.Models;

namespace quotes_project.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		// Acción para la página Main
		public IActionResult Main()
		{
			return View();
		}

		public IActionResult Listado()
		{
			var model = new ListadoModel();
			model.LoadData();
			ViewData["Title"] = "Listado";
			return View(model);
		}

		public IActionResult Cotizador()
		{
			var cotizadorModel = new CotizadorModel();
			cotizadorModel.CustList(); // Cargar la lista de clientes
			return View(cotizadorModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}
