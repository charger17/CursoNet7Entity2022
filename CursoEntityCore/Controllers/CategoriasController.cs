using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoEntityCore.Controllers
{
    public class CategoriasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Categoria> listaCategorias = _context.Categorias.ToList();

            return View(listaCategorias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            await _context.Categorias.AddAsync(categoria);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CrearMultipleOpcion2()
        {
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 2; i++)
            {
                //_context.Categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _context.Categorias.AddRange(categorias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> CrearMultipleOpcion5()
        {
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 5; i++)
            {
                //_context.Categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _context.Categorias.AddRange(categorias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
