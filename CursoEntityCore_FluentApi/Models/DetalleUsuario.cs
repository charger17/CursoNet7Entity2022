using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEntityCore.Models
{
    public class DetalleUsuario
    {
        public int DetalleUsuario_Id { get; set; }

        public string Cedula { get; set; }

        public string Deporte { get; set; }

        public string Mascota { get; set; }

        public Usuario Usuarios { get; set; }

    }
}
