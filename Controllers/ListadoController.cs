using Microsoft.AspNetCore.Mvc;
using quotes_project.Models;
using quotes_project.Views.Home.Data;

namespace quotes_project.Controllers
{
    public class ListadoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Listado
        public IActionResult Index()
        {
            var model = new ListadoModel(_context);
            model.LoadData();
            return View(model); // Devuelve la vista Listado.cshtml con el modelo
        }
    }
}
