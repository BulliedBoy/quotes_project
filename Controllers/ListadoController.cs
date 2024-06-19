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
    }
}
