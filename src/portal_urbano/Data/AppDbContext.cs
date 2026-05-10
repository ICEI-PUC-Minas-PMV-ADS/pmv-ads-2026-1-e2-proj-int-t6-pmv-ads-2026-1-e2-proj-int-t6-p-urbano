using Microsoft.EntityFrameworkCore;
using ProjetoUrbano.Models;

namespace ProjetoUrbano.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Gostei> Gostei { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // DENUNCIA
            modelBuilder.Entity<Denuncia>()
                .Property(d => d.Latitude)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<Denuncia>()
                .Property(d => d.Longitude)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<Denuncia>()
                .HasOne(d => d.Usuario)
                .WithMany(u => u.Denuncias)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Denuncia>()
                .HasOne(d => d.Categoria)
                .WithMany(c => c.Denuncias)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade);

            // COMENTARIO
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Denuncia)
                .WithMany(d => d.Comentarios)
                .HasForeignKey(c => c.IdDenuncia)
                .OnDelete(DeleteBehavior.Cascade);

            // REPORTE
            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.Denuncia)
                .WithMany(d => d.Reportes)
                .HasForeignKey(r => r.IdDenuncia)
                .OnDelete(DeleteBehavior.Cascade);

            // GOSTEI (LIKES)
            modelBuilder.Entity<Gostei>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(g => g.UsuarioId)
                .HasPrincipalKey(u => u.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Gostei>()
                .HasOne<Denuncia>()
                .WithMany(d => d.Likes)
                .HasForeignKey(g => g.DenunciaId)
                .HasPrincipalKey(d => d.IdDenuncia)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
