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

        [HttpPost] //Post para enlazar clientes normales y outsourcing y asignarles el monto 
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

        [HttpPost] //Post para asociar el ususario con su cargo empresarial
        public async Task<JsonResult> ObtenerCargoUsuario(int userId)
        {
            try
            {
                var user = await _context.UserEntity
                    .FirstOrDefaultAsync(p => p.IdUser == userId);

                if (user != null)
                {
                    return Json(new { success = true, position = user.Position });
                }

                return Json(new { success = false, message = "Cargo no asignado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost] //Post para asociar la descripcion al producto
        public async Task<JsonResult> ObtenerProductoDescripcion(int productId)
        {
            try
            {
                var product = await _context.LocalProductEntity
                    .FirstOrDefaultAsync(p => p.IdProduct == productId);

                if (product != null)
                {
                    return Json(new { success = true, productDescription = product.ProductDescription });
                }

                return Json(new { success = false, message = "Producto no encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost] //Post para asociar tipo de cliente y tipo de licencia 
        public async Task<JsonResult> ObtenerClienteDetalles(int customerId)
        {
            try
            {
                var customer = await _context.CustomerEntity
                    .FirstOrDefaultAsync(c => c.IdCustomer == customerId);

                if (customer != null)
                {
                    return Json(new { success = true, customerType = customer.CustomerType, licenceType = customer.LicenceType });
                }

                return Json(new { success = false, message = "Cliente no encontrado." });
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
            Console.WriteLine("Entrando en el método Guardar.");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Datos de formulario inválidos.");
                ModelState.AddModelError("", "Datos de formulario inválidos.");
                await RecargarListasParaFormulario(model);
                return View("Cotizador", model);
            }

            try
            {
                Console.WriteLine($"Buscando cliente con ID: {model.CustomerId}");
                var customer = await _context.CustomerEntity.FirstOrDefaultAsync(c => c.IdCustomer == model.CustomerId);
                if (customer != null)
                {
                    Console.WriteLine($"Cliente encontrado: {customer.CustomerName}");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }

                Console.WriteLine($"Buscando producto con ID: {model.ProductId}");
                var product = await _context.LocalProductEntity.FirstOrDefaultAsync(p => p.IdProduct == model.ProductId);
                if (product != null)
                {
                    Console.WriteLine($"Producto encontrado: {product.ProductName}");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }

                Console.WriteLine($"Buscando usuario con ID: {model.UserId}");
                var user = await _context.UserEntity.FirstOrDefaultAsync(u => u.IdUser == model.UserId);
                if (user != null)
                {
                    Console.WriteLine($"Usuario encontrado: {user.Username}");
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }

                if (customer != null && product != null && user != null)
                {
                    Console.WriteLine("Entidades encontradas, creando nueva cotización.");
                    var quote = new QuoteEntity
                    {
                        CustomerName = customer.CustomerName,
                        Product = product.ProductName,
                        User = user.Username,
                        Amount = model.Amount,
                        DDate = model.DDate
                    };

                    _context.QuoteEntity.Add(quote);
                    await _context.SaveChangesAsync();

                    Console.WriteLine("Cotización guardada exitosamente.");
                    return RedirectToAction("Index", "Listado");
                }
                else
                {
                    Console.WriteLine("No se encontró alguna de las entidades necesarias para guardar la cotización.");
                    ModelState.AddModelError("", "No se encontró alguna de las entidades necesarias para guardar la cotización.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar guardar la cotización: {ex.Message}");
                ModelState.AddModelError("", $"Error al intentar guardar la cotización: {ex.Message}");
            }

            await RecargarListasParaFormulario(model);
            return View("Cotizador", model);
        }

        private async Task RecargarListasParaFormulario(CotizadorModel model)
        {
            Console.WriteLine("Recargando listas para el formulario.");
            model.CustomerEntity = await _context.CustomerEntity.ToListAsync();
            model.LocalProductEntity = await _context.LocalProductEntity.ToListAsync();
            model.UserEntity = await _context.UserEntity.ToListAsync();
            Console.WriteLine("Listas recargadas.");
        }
    }
}
