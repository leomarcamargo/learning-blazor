using CursoBlazor.Shared.Entidades;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmePessoa>().HasKey(x => new {x.IdPessoa, x.IdFilme});
            modelBuilder.Entity<GeneroFilme>().HasKey(x => new {x.IdGenero, x.IdFilme});

            //var pessoas = new List<Pessoa>();
            //for (int i = 5; i < 100; i++)
            //{
            //    pessoas.Add(new Pessoa
            //    {
            //        Id = i,
            //        Nome = $"Pessoa {i}",
            //        DataNascimento = DateTime.Today
            //    });
            //}

            //modelBuilder.Entity<Pessoa>().HasData(pessoas);

            base.OnModelCreating(modelBuilder);
        }
    }
}
