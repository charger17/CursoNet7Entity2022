using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaUsuario = await _context.Usuarios.ToListAsync();

            return View(listaUsuario);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }
            await _context.Usuarios.AddAsync(usuario);
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

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Categoria_Id.Equals(id));
            if (categoria is null)
            {
                return RedirectToAction(nameof(Index));

            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            _context.Categorias.Update(categoria);
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

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Categoria_Id.Equals(id));
            if (categoria is null)
            {
                return RedirectToAction(nameof(Index));

            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BorrarMultiple2()
        {
            IEnumerable<Categoria> categorias = _context.Categorias.OrderByDescending(c => c.Categoria_Id).Take(2);
            _context.Categorias.RemoveRange(categorias);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BorrarMultiple5()
        {
            IEnumerable<Categoria> categorias = _context.Categorias.OrderByDescending(c => c.Categoria_Id).Take(5);
            _context.Categorias.RemoveRange(categorias);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
