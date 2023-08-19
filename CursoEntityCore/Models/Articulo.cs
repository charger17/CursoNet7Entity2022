using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    [Table("tbl_Articulo")]
    public class Articulo
    {
        [Key]
        public int Articulo_Id { get; set; }

        [Column("Titulo")]
        [MaxLength(20)]
        public string TituloArticulo { get; set; }

        public string Descripcion { get; set; }

        public string Fecha { get; set; }

        [Display(Name = "Dirección del usuario")]
        public string Direccion { get; set; }

        [NotMapped]
        public int Edad { get; set; }

    }
}
