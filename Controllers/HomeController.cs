using Microsoft.AspNetCore.Mvc;
using quotes_project.Models;
using quotes_project.Views.Home.Data;

namespace quotes_project.Controllers
{
	public class HomeController : Controller
	{
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult Main()
		{
			return View();
		}
		public IActionResult Listado()
		{
			var model = new ListadoModel(_context);
			model.LoadData();
			ViewData["Title"] = "Listado";
			return View(model);
		}
		public IActionResult Cotizador()
		{
            var model = new CotizadorModel(_context);
            model.LoadData();
            ViewData["Title"] = "Cotizador";
            return View(model);
        }
		public IActionResult Privacy()
		{
			return View();
		}
	}
}