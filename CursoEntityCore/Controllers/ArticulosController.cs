using CursoEntityCore.Datos;
using Microsoft.AspNetCore.Mvc;

namespace CursoEntityCore.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticulosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listaArticulos = _context.Articulos.ToList();
            return View(listaArticulos);
        }
    }
}
