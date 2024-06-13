using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quotes_project.Models;
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
                    decimal amount = customer.CustomerType switch
                    {
                        "Normal" => product.AmountNormal,
                        "Outsourcing" => product.AmountOutsourcing,
                        _ => 0M  // 0M indica un literal decimal
                    };

                    return Json(new { success = true, monto = amount });
                }

                return Json(new { success = false, message = "Error al obtener el monto del cliente o el producto." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(CotizadorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Crear entidad de cotización
                    var quote = new QuoteEntity
                    {
                        CustomerName = _context.CustomerEntity.FirstOrDefault(c => c.IdCustomer == model.CustomerId)?.CustomerName,
                        Product = _context.LocalProductEntity.FirstOrDefault(p => p.IdProduct == model.ProductId)?.ProductName,
                        User = _context.UserEntity.FirstOrDefault(u => u.IdUser == model.UserId)?.Username,
                        Amount = model.Amount,
                        DDate = model.DDate
                    };

                    // Guardar en la base de datos
                    _context.QuoteEntity.Add(quote);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Listado");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al guardar la cotización: {ex.Message}");
                }
            }

            // Recargar listas necesarias para el formulario
            model.CustomerEntity = await _context.CustomerEntity.ToListAsync();
            model.LocalProductEntity = await _context.LocalProductEntity.ToListAsync();
            model.UserEntity = await _context.UserEntity.ToListAsync();

            return View("Cotizador", model);
        }

    }
}
