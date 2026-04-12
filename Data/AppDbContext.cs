using Microsoft.EntityFrameworkCore;
using ClinicaBlazor.Models;

namespace ClinicaBlazor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<PermisoPerfil> PermisosPerfil { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Perfil>().ToTable("perfiles");
            modelBuilder.Entity<Modulo>().ToTable("modulos");
            modelBuilder.Entity<PermisoPerfil>().ToTable("permisos_perfil");

            modelBuilder.Entity<PermisoPerfil>()
                .HasOne(p => p.Perfil)
                .WithMany()
                .HasForeignKey(p => p.PerfilId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermisoPerfil>()
                .HasOne(p => p.Modulo)
                .WithMany()
                .HasForeignKey(p => p.ModuloId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}