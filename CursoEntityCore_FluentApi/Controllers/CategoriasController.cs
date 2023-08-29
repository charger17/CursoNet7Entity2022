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
            List<Categoria> listaCategorias = _context.Categorias.ToList();

            //Consulta filtrando por fecha
            //var fecha = new DateTime(2023, 08, 05);
            //List<Categoria> listaCategorias = _context.Categorias.Where(f => f.FechaCreacion >= fecha).OrderByDescending(f => f.FechaCreacion).ToList();

            //Seleccionar columnas especificas
            //List<Categoria> listaCategorias = _context.Categorias.Where(x => x.Nombre.Contains("Test 5")).Select(n => n).ToList();

            //GroupBy
            //var listaCategorias = _context.Categorias.GroupBy(c => new {c.Activo}).Select(c => new { c.Key, Count = c.Count() }).ToList();

            //take y skip
            //var listaCategorias = _context.Categorias.Skip(3).Take(2).ToList();

            //consultas sql convencionales
            //var listaCategorias = _context.Categorias.FromSqlRaw(@"SELECT * FROM Categorias WHERE Nombre LIKE '%Categoria%'").ToList();

            //interpolacion de string
            ////var id = "25";
            ////var nombre = "Categoria 1";
            ////var listaCategorias = _context.Categorias.FromSqlRaw(@"SELECT * FROM Categorias WHERE Categoria_Id = {0} OR Nombre = {1}", id, nombre).ToList();

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

        [HttpGet]
        public void EjecucionDiferida()
        {
            //1. Cuando hace una iteraciòn sobre ellos
            var categorias = _context.Categorias;

            foreach(var categoria in categorias)
            {
                var nombreCat = "";
                nombreCat = categoria.Nombre;
            }

            //2. Cuando se llama a cualquiera de los métodos: ToDictionary, ToList, ToArray
            var categorias2 = _context.Categorias.ToList();
            foreach (var categoria in categorias2)
            {
                var nombreCat = "";
                nombreCat = categoria.Nombre;
            }

            //3. Cuando se llama cualquier método que retorna un solo objeto:
            //First, single, count, max, entre otros
            var categorias3 = _context.Categorias;

            var totalCategorias = categorias3.Count();
            
            var totalCategorias2 = _context.Categorias.Count();

            var test = "";

        }

        public void TestIEnumerable()
        {
            //1. Código con IEnumerable
            IEnumerable<Categoria> listaCategorias = _context.Categorias;
            var categoriasActivas = listaCategorias.Where(a => a.Activo.Equals(true)).ToList();
        }

        public void TestIQuerable()
        {
            //1. Código con IQuerable
            //IQuerable hereda de IEnumerable
            //Todo lo que se puede hacer IEnumerable se puede hacer con IQuerable 
            IQueryable<Categoria> listaCategorias = _context.Categorias;
            var categoriasActivas = listaCategorias.Where(a => a.Activo.Equals(true)).ToList();
        }

        public void TestUpdate()
        {
            var datoUsuario = _context.Usuarios.Include(d => d.DetalleUsuarios).FirstOrDefault(d => d.Id.Equals(2));
            datoUsuario.DetalleUsuarios.Deporte = "Natacion";
            _context.Update(datoUsuario);
            _context.SaveChanges();
        }

        public void TestAttach()
        {
            var datoUsuario = _context.Usuarios.Include(d => d.DetalleUsuarios).FirstOrDefault(d => d.Id.Equals(2));
            datoUsuario.DetalleUsuarios.Deporte = "Ciclismo";
            _context.Attach(datoUsuario);
            _context.SaveChanges();
        }

        //Crear el método para llamar a la vista
        public void ObtenerCategoriaDesdeVistaSQL()
        {
            var usarVista1 = _context.CategoriaDesdeVistas.ToList();

            var usarVista2 = _context.CategoriaDesdeVistas.FirstOrDefault();

            var usarVista3 = _context.CategoriaDesdeVistas.Where(c => c.Activo.Equals(true));
        }

        //Crear el método para llamar a al sp
        public void ConsultasFromSQL()
        {
            //Consulta directa menos segura
            var usuario = _context.Usuarios.FromSqlRaw("SELECT * FROM Usuarios").ToList();

            //Consulta con parametros
            var idUsuario = 2;
            var usuario2 = _context.Usuarios.FromSqlRaw("SELECT * FROM Usuarios WHERE Id = {0}", idUsuario).ToList();
            var usuario3 = _context.Usuarios.FromSqlInterpolated($"SELECT * FROM Usuarios WHERE Id = {idUsuario}" ).ToList();
            var UsuarioPorProdcedimiento = _context.Usuarios.FromSqlInterpolated($"EXEC SpObtenerUsuarioId {idUsuario}").ToList();

        }

    }
}
