using CursoBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Filme> Filme { get; set; }
        public DbSet<FilmePessoa> FilmePessoa { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<GeneroFilme> GeneroFilme { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<VotoFilme> VotoFilme { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmePessoa>().HasKey(x => new {x.IdPessoa, x.IdFilme});
            modelBuilder.Entity<GeneroFilme>().HasKey(x => new {x.IdGenero, x.IdFilme});

            var roleAdmin = new IdentityRole
            {
                Id = "e50e27ce-4792-47fd-87b0-606250021ce0",
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(modelBuilder);
        }
    }
}
