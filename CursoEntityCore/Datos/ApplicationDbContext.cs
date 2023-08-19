using CursoEntityCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityCore.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        /*
            CUANDO CREAR MIGRACIONES
         1. Cuando se crea una nueva tabla o clase (Modelo)
         2. Cuando agregue una nueva propiedad o nueva columna en la tabla
         3. Caundo modifique un valor del campo en la clase (modificar una columna en bd)
         */

        //Escribir modelos
        public virtual DbSet<Categoria> Categorias { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Articulo> Articulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
