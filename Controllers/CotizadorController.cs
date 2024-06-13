using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quotes_project.Views.Home.Data;
using quotes_project.Views.Home.Data.Entities;

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
        public async Task<IActionResult> Guardar(string CustomerName, string Product, string User, decimal Amount, DateTime DDate)
        {
            if (ModelState.IsValid)
            {
                var quote = new QuoteEntity
                {
                    CustomerName = CustomerName,
                    Product = Product,
                    User = User,
                    Amount = Amount,
                    DDate = DDate
                };

                _context.QuoteEntity.Add(quote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Listado");
            }
            return View("Cotizador", new { CustomerName, Product, User, Amount, DDate }); // Pasa el modelo de vuelta a la vista en caso de error
        }
    }
}
