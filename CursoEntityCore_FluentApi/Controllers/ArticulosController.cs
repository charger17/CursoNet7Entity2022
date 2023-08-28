using CursoEntityCore.Datos;
using CursoEntityCore.Models;
using CursoEntityCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {

            //opcion 1 sin datos relacionado (Solo trae el id de la categoria)
            //var listaArticulos = _context.Articulos.ToList();

            //foreach (var item in listaArticulos)
            //{
            //    ////Opcion 2: carga manual se generan muchas cargas sql, no es muy eficiente si necesitamos cargar muchos datos
            //    //item.Categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Categoria_Id.Equals(item.Categoria_Id));

            //    //Opcion 3: Carga explícita (explicit loading)
            //    await _context.Entry(item).Reference(c => c .Categoria).LoadAsync();
            //}

            //opcion4: Carga diligente o eager loading
            var listaArticulos = await _context.Articulos.Include(c => c.Categoria).ToListAsync();


            return View(listaArticulos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();

            articuloCategorias.ListaCategorias = _context.Categorias
                .Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Categoria_Id.ToString()
                })
                .ToList();

            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();

                articuloCategorias.ListaCategorias = _context.Categorias
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Categoria_Id.ToString()
                    })
                    .ToList();

                return View(articuloCategorias);
            }
            await _context.Articulos.AddAsync(articulo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(c => c.Articulo_Id.Equals(id));
            if (articulo is null)
            {
                return NotFound();

            }

            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.Articulo = articulo;
            articuloCategorias.ListaCategorias = _context.Categorias
                .Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Categoria_Id.ToString()
                })
                .ToList();

            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Articulo articulo)
        {
            if (articulo.Articulo_Id is 0)
            {
                ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();

                articuloCategorias.ListaCategorias = _context.Categorias
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Categoria_Id.ToString()
                    })
                    .ToList();

                return View(articuloCategorias);
            }

            if (!ModelState.IsValid)
            {
                ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();

                articuloCategorias.ListaCategorias = _context.Categorias
                    .Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Categoria_Id.ToString()
                    })
                    .ToList();

                return View(articuloCategorias);
            }
            _context.Articulos.Update(articulo);
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

            var articulo = await _context.Articulos.FirstOrDefaultAsync(c => c.Articulo_Id.Equals(id));
            if (articulo is null)
            {
                return RedirectToAction(nameof(Index));

            }
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AdministrarEtiquetas(int id)
        {
            ArticuloEtiquetaVM articuloEtiquetas = new ArticuloEtiquetaVM
            {
                ListaArticuloEtiquetas = _context.ArticuloEtiquetas.Include(e => e.Etiquetas).Include(a => a.Articulos)
                .Where(a => a.Articulo_Id == id),

                ArtsEtiq = new ArticuloEtiqueta()
                {
                    Articulo_Id = id
                },
                Arts = _context.Articulos.FirstOrDefault(a => a.Articulo_Id == id)
            };

            List<int> listaTemporalEtiquetasArticulo = articuloEtiquetas.ListaArticuloEtiquetas.Select(e => e.Etiqueta_Id).ToList();
            //Obtener todas las etiquetas cuyos id's no estén en la listaTemporalEtiquetasArticulo
            //Crear un NOT IN usando LINQ
            var listaTemporal = _context.Etiquetas.Where(e => !listaTemporalEtiquetasArticulo.Contains(e.Etiqueta_Id)).ToList();

            //Crear lista de etiquetas para el dropdown
            articuloEtiquetas.ListaEtiquetas = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.Titulo,
                Value = i.Etiqueta_Id.ToString()
            });

            return View(articuloEtiquetas);
        }

        [HttpPost]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if (articuloEtiquetas.ArtsEtiq.Articulo_Id != 0 && articuloEtiquetas.ArtsEtiq.Etiqueta_Id != 0)
            {
                _context.ArticuloEtiquetas.Add(articuloEtiquetas.ArtsEtiq);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = articuloEtiquetas.ArtsEtiq.Articulo_Id });
        }

        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            int idArticulo = articuloEtiquetas.Arts.Articulo_Id;
            ArticuloEtiqueta articuloEtiqueta = _context.ArticuloEtiquetas.FirstOrDefault(
                    u => u.Etiqueta_Id == idEtiqueta && u.Articulo_Id == idArticulo
                );

            _context.ArticuloEtiquetas.Remove(articuloEtiqueta);
            _context.SaveChanges();

            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = idArticulo });
        }
    }
}
