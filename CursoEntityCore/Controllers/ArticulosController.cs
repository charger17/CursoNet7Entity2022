﻿using CursoEntityCore.Datos;
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
        public IActionResult Index()
        {
            var listaArticulos = _context.Articulos.ToList();
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
    }
}
