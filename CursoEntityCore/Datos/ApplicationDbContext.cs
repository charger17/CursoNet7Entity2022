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

        public virtual DbSet<DetalleUsuario> DetalleUsuarios { get; set; }

        public virtual DbSet<Etiqueta> Etiquetas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticuloEtiqueta>()
                .HasKey(a => new
                {
                    a.Etiqueta_Id,
                    a.Articulo_Id
                });

            //Siembrea de datos se hace en este método
            var categoria5 = new Categoria 
            {
                Categoria_Id = 33,
                Nombre = "Categoria 5",
                FechaCreacion = new DateTime(2023,08,21),
                Activo = true
            };
            var categoria6 = new Categoria
            {
                Categoria_Id = 34,
                Nombre = "Categoria 6",
                FechaCreacion = new DateTime(2023, 08, 21),
                Activo = false
            };

            modelBuilder.Entity<Categoria>().HasData(new Categoria[] { categoria5, categoria6 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
