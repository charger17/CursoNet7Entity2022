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
        [Required(ErrorMessage = "El titulo es obligatorio")]
        [MaxLength(20)]
        public string TituloArticulo { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Range(0.1, 5.0)]
        public double Calificiacion { get; set; }

    }
}
