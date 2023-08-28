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

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (usuario is null)
            {
                return RedirectToAction(nameof(Index));

            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            _context.Usuarios.Update(usuario);
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

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (usuario is null)
            {
                return RedirectToAction(nameof(Index));

            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios.Include(d => d.DetalleUsuarios).FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (usuario is null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarDetalle(Usuario usuario)
        {

            if (usuario.DetalleUsuarios.DetalleUsuario_Id == 0)
            {
                //Creamos los detalles para ese usuario
                _context.DetalleUsuarios.Add(usuario.DetalleUsuarios);
                await _context.SaveChangesAsync();

                //Despues de crear el detalle el usuario, obtenemos el usuario de la base de datos y le actualizamos el campo "DetalleUsuarioId"
                var usuarioBd = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id.Equals(usuario.Id));
                usuarioBd.DetalleUsuario_Id = usuario.DetalleUsuarios.DetalleUsuario_Id;
                _context.Usuarios.Update(usuarioBd);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
