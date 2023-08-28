using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Categoria
    {

        public int Categoria_Id { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }

        public List<Articulo> Articulos { get; set; }

    }
}
