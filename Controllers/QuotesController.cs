using Microsoft.AspNetCore.Mvc;
using quotes_project.Data;
using quotes_project.Models;

namespace quotes_project.Controllers
{
    public class QuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            var listadoModel = new ListadoModel(_context);
            listadoModel.LoadData();
            return View(listadoModel);
        }
    }
}
