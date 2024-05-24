using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace quotes_project.Controllers
{
    public class AccountController : Controller
    {
        // Método para validar el usuario y contraseña
        private bool ValidateUser(string username, string password)
        {
            // Reemplazar con la lógica real para validar las credenciales contra una lista de usuarios almacenada en algún lugar (base de datos, archivo, etc.)
            return Users.Any(u => u.Username == username && u.Password == password);
        }

        // Clase de ejemplo para representar un usuario
        public class UserModel
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        // Lista de usuarios (esto es solo un ejemplo, en la práctica deberías obtener los usuarios de una fuente de datos externa)
        private List<UserModel> Users = new List<UserModel>
        {
            new UserModel { Username = "admin", Password = "admin" },
            new UserModel { Username = "user1", Password = "password1" },
            new UserModel { Username = "user2", Password = "password2" }
        };

        // Acción para manejar el POST del formulario de inicio de sesión
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ValidateUser(username, password))
            {
                // Establecer la sesión cuando el usuario inicie sesión exitosamente
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Main", "Home");
            }
            else
            {
                // Si la validación falla, establece un mensaje de error
                TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
                // Redirige a la acción Login del controlador Account
                return RedirectToAction("Index", "Home");
            }
        }

        // Acción para mostrar la página de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para manejar el cierre de sesión
        [HttpPost]
        public IActionResult Logout()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
