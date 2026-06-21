using Microsoft.EntityFrameworkCore;
using portal_urbano.Models;

namespace portal_urbano.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.Property(u => u.Avisos).HasDefaultValue(0);
                entity.Property(u => u.Banido).HasDefaultValue(false);
                entity.Property(u => u.CriadoEm)
                    .HasColumnType("datetime(6)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(u => u.PasswordResetToken).HasMaxLength(128);
                entity.Property(u => u.PasswordResetExpires).HasColumnType("datetime(6)");
                entity.Property(u => u.Cidade).HasMaxLength(100);
                entity.Property(u => u.Bairro).HasMaxLength(100);
                entity.Property(u => u.Uf).HasMaxLength(2);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");
                entity.Property(c => c.Icone).HasColumnType("text");
            });

            modelBuilder.Entity<Denuncia>(entity =>
            {
                entity.ToTable("denuncias");
                entity.Property(d => d.Latitude).HasColumnType("decimal(10,6)");
                entity.Property(d => d.Longitude).HasColumnType("decimal(10,6)");
                entity.Property(d => d.StatusAnonimo).HasDefaultValue(0);
                entity.Property(d => d.Rua).HasMaxLength(255);
                entity.Property(d => d.Bairro).HasMaxLength(255);
                entity.Property(d => d.Cidade).HasMaxLength(255);
                entity.Property(d => d.Uf).HasMaxLength(255);
                entity.Property(d => d.Cep).HasMaxLength(255);
                entity.Property(d => d.Complemento).HasMaxLength(255);

                entity.HasOne(d => d.Usuario)
                    .WithMany(u => u.Denuncias)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Categoria)
                    .WithMany(c => c.Denuncias)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("comentarios");

                entity.HasOne(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey(c => c.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Denuncia)
                    .WithMany(d => d.Comentarios)
                    .HasForeignKey(c => c.IdDenuncia)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.ToTable("reportes");

                entity.HasIndex(r => new { r.IdDenuncia, r.IdUsuario }).IsUnique();

                entity.HasOne(r => r.Usuario)
                    .WithMany()
                    .HasForeignKey(r => r.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Denuncia)
                    .WithMany(d => d.Reportes)
                    .HasForeignKey(r => r.IdDenuncia)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Gostei>(entity =>
            {
                entity.ToTable("gostei");
entity.Property(g => g.LikeId).HasColumnType("int");
entity.Property(g => g.UsuarioId).HasColumnType("int");
entity.Property(g => g.DenunciaId).HasColumnType("int");
                entity.Property(g => g.CriadoEm)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Denuncia>()
                    .WithMany(d => d.Likes)
                    .HasForeignKey(g => g.DenunciaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            SeedCategorias(modelBuilder);
        }

        private static void SeedCategorias(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    IdCategoria = 1,
                    Nome = "Infraestrutura",
                    Descricao = "Buracos, asfalto e problemas em vias",
                    Icone = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"></path><polyline points=\"9 22 9 12 15 12 15 22\"></polyline></svg>"
                },
                new Categoria
                {
                    IdCategoria = 2,
                    Nome = "Iluminação Pública",
                    Descricao = "Postes sem luz, lâmpadas queimadas",
                    Icone = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><circle cx=\"12\" cy=\"12\" r=\"5\"></circle><path d=\"M12 2v2M12 20v2M4.93 4.93l1.41 1.41M17.66 17.66l1.41 1.41M2 12h2M20 12h2M6.34 17.66l-1.41 1.41M19.07 4.93l-1.41 1.41\"></path></svg>"
                },
                new Categoria
                {
                    IdCategoria = 3,
                    Nome = "Limpeza",
                    Descricao = "Lixo acumulado, mato alto, entulho",
                    Icone = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M3 6h18M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2M10 11v6M14 11v6\"></path></svg>"
                },
                new Categoria
                {
                    IdCategoria = 4,
                    Nome = "Segurança",
                    Descricao = "Vandalismo, furtos, locais perigosos",
                    Icone = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><rect x=\"3\" y=\"11\" width=\"18\" height=\"11\" rx=\"2\" ry=\"2\"></rect><path d=\"M7 11V7a5 5 0 0 1 10 0v4\"></path></svg>"
                },
                new Categoria
                {
                    IdCategoria = 5,
                    Nome = "Saneamento",
                    Descricao = "Vazamentos de água e esgoto entupido",
                    Icone = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M12 22a7 7 0 0 0 7-7c0-2-1-3.9-3-5.5s-3.5-4-4-6.5c-.5 2.5-2 4.9-4 6.5C6 11.1 5 13 5 15a7 7 0 0 0 7 7z\"></path></svg>"
                });
        }
    }
}
