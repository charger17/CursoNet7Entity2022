using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        //[RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Por favor ingrese un Email correcto.")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un Email correcto.")]
        public string Email { get; set; }

        [Display(Name = "Dirección del usuario")]
        public string Direccion { get; set; }

        [NotMapped]
        public int Edad { get; set; }

        [ForeignKey("DetalleUsuarios")]
        public int DetalleUsuario_Id { get; set; }

        public DetalleUsuario DetalleUsuarios { get; set; }
    }
}
