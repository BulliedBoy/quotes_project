using Microsoft.AspNetCore.Mvc;
using quotes_project.Models;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;
using System.Threading.Tasks;

namespace quotes_project.Controllers
{
    public class CotizadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CotizadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cotizador
        public IActionResult Index
        {
            get
            {
                var model = new QuoteEntity(); // Crea un nuevo objeto QuoteEntity
                return View(model); // Pasa el modelo a la vista
            }
        }

        // POST: Cotizador/Guardar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(QuoteEntity model)
        {
            if (ModelState.IsValid)
            {
                _context.QuoteEntity.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Listado");
            }
            return View(model); // Pasa el modelo de vuelta a la vista en caso de error
        }
    }

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
            return View(model);
        }
    }
}
