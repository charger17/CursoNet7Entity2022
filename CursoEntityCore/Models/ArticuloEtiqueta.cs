using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class ArticuloEtiqueta
    {
        [ForeignKey("Articulos")]
        public int Articulo_Id { get; set; }

        [ForeignKey("Etiquetas")]
        public int Etiqueta_Id { get; set; }

        public Articulo Articulos { get; set; }

        public Etiqueta Etiquetas { get; set; }
    }
}
