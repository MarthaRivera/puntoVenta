namespace CapaDatos
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modelos : DbContext
    {
        public Modelos()
            : base("name=Modelos")
        {
        }

        public virtual DbSet<Catalogo> Catalogo { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogo>()
                .Property(e => e.Nombre_Producto)
                .IsUnicode(false);

            modelBuilder.Entity<Catalogo>()
                .Property(e => e.Precio_Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalogo>()
                .Property(e => e.Precio_Venta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalogo>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Nombre_Categoria)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Catalogo)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);
        }
    }
}
