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

        public virtual DbSet<ArticuloEtiqueta> ArticuloEtiquetas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            #region Fluent API Para categoría
            modelBuilder.Entity<Categoria>()
                 .HasKey(c => c.Categoria_Id);
            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .IsRequired();
            modelBuilder.Entity<Categoria>()
                .Property(c => c.FechaCreacion)
                .HasColumnType("date");
            #endregion

            #region //Fluent API para Articulo
            modelBuilder.Entity<Articulo>()
                .HasKey(a => a.Articulo_Id);
            modelBuilder.Entity<Articulo>()
                .Property(a => a.TituloArticulo)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Articulo>()
                .Property(a => a.Descripcion)
                .IsRequired()
                .HasMaxLength(500);
            modelBuilder.Entity<Articulo>()
                .Property(a => a.Fecha)
                .HasColumnType("date");
            #endregion

            #region //Fluent API nombre de tabla y nombre de columna
            modelBuilder.Entity<Articulo>()
                .ToTable("Tbl_Articulo");
            modelBuilder.Entity<Articulo>()
                .Property(a => a.TituloArticulo)
                .HasColumnName("Titulo");
            #endregion

            #region //Fluent API para Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>()
                .Ignore(u => u.Edad);
            #endregion

            #region //Fluent API para DetalleUsuario
            modelBuilder.Entity<DetalleUsuario>()
                .HasKey(d => d.DetalleUsuario_Id);
            modelBuilder.Entity<DetalleUsuario>()
                .Property(d => d.Cedula)
                .IsRequired();
            #endregion

            #region //Fluent API para Etiqueta
            modelBuilder.Entity<Etiqueta>()
                .HasKey(e => e.Etiqueta_Id);
            modelBuilder.Entity<Etiqueta>()
                .Property(e => e.Fecha)
                .HasColumnType("date");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
