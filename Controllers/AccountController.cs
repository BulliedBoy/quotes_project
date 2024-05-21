using Microsoft.AspNetCore.Mvc;

namespace quotes_project.Controllers
{
    public class AccountController : Controller
    {
        // Método para validar el usuario y contraseña
        private bool ValidateUser(string username, string password)
        {
            // Ejemplo de validación estática; reemplazar con lógica real
            return username == "admin" && password == "password";
        }

        // Acción para manejar el POST del formulario de inicio de sesión
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ValidateUser(username, password))
            {
                // Si la validación es correcta, redirige a la página Main
                return RedirectToAction("Main", "Home");
            }
            else
            {
                // Si la validación falla, muestra un mensaje de error
                ViewData["ErrorMessage"] = "Usuario/Contraseña incorrecto.";
                return View();
            }
        }

        // Acción para mostrar la página de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
