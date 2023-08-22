using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            //ConsultaInicial
            //List<Categoria> listaCategorias = _context.Categorias.ToList();

            //Consulta filtrando por fecha
            //var fecha = new DateTime(2023, 08, 05);
            //List<Categoria> listaCategorias = _context.Categorias.Where(f => f.FechaCreacion >= fecha).OrderByDescending(f => f.FechaCreacion).ToList();

            //Seleccionar columnas especificas
            //List<Categoria> listaCategorias = _context.Categorias.Where(x => x.Nombre.Contains("Test 5")).Select(n => n).ToList();

            //GroupBy
            //var listaCategorias = _context.Categorias.GroupBy(c => new {c.Activo}).Select(c => new { c.Key, Count = c.Count() }).ToList();

            //take y skip
            var listaCategorias = _context.Categorias.Skip(3).Take(2).ToList();

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

        [HttpGet]
        public IActionResult VistaCrearMultipleOpcionFormulario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearMultipleOpcionFormulario()
        {
            string categoriasForm = Request.Form["Nombre"];
            var listaCategorias = from val in categoriasForm.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) select (val);

            List<Categoria> categorias = new List<Categoria>();

            foreach (var item in listaCategorias)
            {
                categorias.Add(new Categoria
                {
                    Nombre = item
                });
            }

            _context.Categorias.AddRange(categorias);
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
