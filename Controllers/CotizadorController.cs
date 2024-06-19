﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost] //Post para guardar la cotizacion realizada
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(CotizadorModel model)
        {
            try
            {
                var customer = await _context.CustomerEntity.FirstOrDefaultAsync(c => c.IdCustomer == model.CustomerId);
                var product = await _context.LocalProductEntity.FirstOrDefaultAsync(p => p.IdProduct == model.ProductId);
                var user = await _context.UserEntity.FirstOrDefaultAsync(u => u.IdUser == model.UserId);

                if (customer != null && product != null && user != null)
                {
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

                    return RedirectToAction("Index", "Listado");
                }
                else
                {
                    ModelState.AddModelError("", "No se encontró alguna de las entidades necesarias para guardar la cotización.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al intentar guardar la cotización: {ex.Message}");
            }

            // Recargar listas necesarias para el formulario
            model.CustomerEntity = await _context.CustomerEntity.ToListAsync();
            model.LocalProductEntity = await _context.LocalProductEntity.ToListAsync();
            model.UserEntity = await _context.UserEntity.ToListAsync();

            return View("Cotizador", model);
        }


    }
}
