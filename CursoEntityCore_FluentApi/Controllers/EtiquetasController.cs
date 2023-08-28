using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Controllers
{
    public class EtiquetasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public EtiquetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            var listaEtiquetas = _context.Etiquetas.ToList();

            return View(listaEtiquetas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Etiqueta etiqueta)
        {
            if (!ModelState.IsValid)
            {
                return View(etiqueta);
            }
            await _context.Etiquetas.AddAsync(etiqueta);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var etiqueta = await _context.Etiquetas.FirstOrDefaultAsync(c => c.Etiqueta_Id.Equals(id));
            if (etiqueta is null)
            {
                return RedirectToAction(nameof(Index));

            }

            return View(etiqueta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Etiqueta etiqueta)
        {
            if (!ModelState.IsValid)
            {
                return View(etiqueta);
            }

            _context.Etiquetas.Update(etiqueta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var etiqueta = await _context.Etiquetas.FirstOrDefaultAsync(c => c.Etiqueta_Id.Equals(id));
            if (etiqueta is null)
            {
                return RedirectToAction(nameof(Index));

            }
            _context.Etiquetas.Remove(etiqueta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
