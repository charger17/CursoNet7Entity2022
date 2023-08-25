using CursoEntityCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CursoEntityCore.ViewModels
{
    public class ArticuloEtiquetaVM
    {
        public ArticuloEtiqueta ArtsEtiq { get; set; }
        public Articulo Arts { get; set; }
        public IEnumerable<ArticuloEtiqueta> ListaArticuloEtiquetas { get; set; }
        public IEnumerable<SelectListItem> ListaEtiquetas { get; set; }

    }
}
