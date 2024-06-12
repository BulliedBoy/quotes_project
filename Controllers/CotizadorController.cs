using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<JsonResult> ObtenerMontoCliente(int customerId, int productId)
        {
            try
            {
                var customer = await _context.CustomerEntity
                    .FirstOrDefaultAsync(c => c.IdCustomer == customerId);

                var product = await _context.LocalProductEntity
                    .FirstOrDefaultAsync(p => p.IdProduct == productId);

                if (customer != null && product != null)
                {
                    // Determinar el monto basado en el tipo de cliente y el tipo de producto
                    double monto = customer.CustomerType switch
                    {
                        "Normal" => product.AmountNormal,
                        "Outsourcing" => product.AmountOutsourcing,
                        _ => 0
                    };

                    return Json(new { success = true, monto });
                }

                return Json(new { success = false, message = "Error al obtener el monto del cliente o el producto." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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
            return View(model); // Devuelve la vista Listado.cshtml con el modelo
        }
    }
}
