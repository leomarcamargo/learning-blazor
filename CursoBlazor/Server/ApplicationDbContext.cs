using CursoBlazor.Shared.Entidades;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CursoBlazor.Server
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {

        }

        public DbSet<Filme> Filme { get; set; }
        public DbSet<FilmePessoa> FilmePessoa { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<GeneroFilme> GeneroFilme { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<VotoFilme> VotoFilme { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmePessoa>().HasKey(x => new {x.IdPessoa, x.IdFilme});
            modelBuilder.Entity<GeneroFilme>().HasKey(x => new {x.IdGenero, x.IdFilme});
            modelBuilder.Entity<SalaFilme>().HasKey(x => new {x.IdSala, x.IdFilme});

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
