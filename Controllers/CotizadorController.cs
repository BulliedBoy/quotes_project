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
                var customer = await _context.CustomerEntity.FindAsync(customerId);
                var product = await _context.LocalProductEntity.FindAsync(productId);

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

                return Json(new { success = false, message = "Cliente o producto no encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener el monto: {ex.Message}" });
            }
        }

        [HttpPost] //Post para asociar el ususario con su cargo empresarial
        public async Task<JsonResult> ObtenerCargoUsuario(int userId)
        {
            try
            {
                var user = await _context.UserEntity.FindAsync(userId);

                if (user != null)
                {
                    return Json(new { success = true, position = user.Position });
                }

                return Json(new { success = false, message = "Usuario no encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener el cargo: {ex.Message}" });
            }
        }

        [HttpPost] //Post para asociar la descripcion al producto
        public async Task<JsonResult> ObtenerProductoDescripcion(int productId)
        {
            try
            {
                var product = await _context.LocalProductEntity.FindAsync(productId);

                if (product != null)
                {
                    return Json(new { success = true, productDescription = product.ProductDescription });
                }

                return Json(new { success = false, message = "Producto no encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener la descripción del producto: {ex.Message}" });
            }
        }

        [HttpPost] //Post para asociar tipo de cliente y tipo de licencia 
        public async Task<JsonResult> ObtenerClienteDetalles(int customerId)
        {
            try
            {
                var customer = await _context.CustomerEntity.FindAsync(customerId);

                if (customer != null)
                {
                    return Json(new { success = true, customerType = customer.CustomerType, licenceType = customer.LicenceType });
                }

                return Json(new { success = false, message = "Cliente no encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener los detalles del cliente: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var quote = await _context.QuoteEntity.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            var model = new CotizadorModel(_context, quote);
            return View("Cotizador", model); // Reutilizar la vista Cotizador.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Asegura que el token anti-forgery esté presente para prevenir ataques CSRF
        public async Task<IActionResult> Guardar(CotizadorModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recargamos los datos necesarios y mostramos la vista con errores
                model.LoadData(); // Método para cargar datos como clientes, productos, etc.
                return View("Cotizador", model);
            }

            try
            {
                // Crear una nueva entidad de cotización y asignar valores del modelo
                var quote = new QuoteEntity
                {
                    CustomerId = model.CustomerId,
                    ProductId = model.ProductId,
                    Amount = model.Amount,
                    DDate = model.DDate,
                    UserId = model.UserId,
                    ProductDescription = model.ProductDescription,
                    // Asegúrate de asignar otros campos necesarios según tu modelo de datos
                };

                _context.QuoteEntity.Add(quote);
                await _context.SaveChangesAsync();

                return RedirectToAction("Listado", "Home"); // Redirecciona a la acción de listado o a donde sea necesario
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al intentar guardar la cotización: {ex.Message}");
                model.LoadData(); // Recargar datos en caso de error
                return View("Cotizador", model);
            }
        }

    }
}
