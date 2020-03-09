using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoBlazor.Server.Helpers;
using CursoBlazor.Shared.DTO;
using CursoBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IArmazenadorArquivo _armazenadorArquivo;
        private readonly IMapper _mapper;

        public FilmeController(ApplicationDbContext db, IArmazenadorArquivo armazenadorArquivo, IMapper mapper)
        {
            _db = db;
            _armazenadorArquivo = armazenadorArquivo;
            _mapper = mapper;
        }

        public async Task<ActionResult<HomePageDTO>> Get()
        {
            const int limite = 6;

            var filmesEmCartaz = await _db.Filme
                .Where(x => x.EmCartaz)
                .Take(limite)
                .OrderByDescending(x => x.DataLancamento)
                .ToListAsync();

            var dataAtual = DateTime.Today;

            var proximosLancamentos = await _db.Filme
                .Where(x => x.DataLancamento > dataAtual)
                .OrderBy(x => x.DataLancamento)
                .Take(limite)
                .ToListAsync();

            return new HomePageDTO
            {
                EmCartaz = filmesEmCartaz,
                ProximosLancamentos = proximosLancamentos
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisualizarFilmeDTO>> Get(int id)
        {
            var filme = await _db.Filme
                .Include(x => x.GeneroFilme)
                .ThenInclude(x => x.Genero)
                .Include(x => x.FilmePessoa)
                .ThenInclude(x => x.Pessoa)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            var mediaVotos = 4;
            var votoUsuario = 5;

            filme.FilmePessoa = filme.FilmePessoa.OrderBy(x => x.Ordem).ToList();

            var model = new VisualizarFilmeDTO
            {
                Filme = filme,
                Generos = filme.GeneroFilme.Select( x=> x.Genero).ToList(),
                Atores = filme.FilmePessoa.Select(x => new Pessoa
                {
                    Id = x.IdPessoa,
                    Nome = x.Pessoa.Nome,
                    Foto = x.Pessoa.Foto,
                    Personagem = x.Personagem
                }).ToList(),
                MediaVotos = mediaVotos, 
                VotoUsuario = votoUsuario,
            };

            return model;
        }

        [HttpGet("atualizar/{id}")]
        public async Task<ActionResult<AtualizarFilmeDTO>> PutGet(int id)
        {
            var filmeActionResult = await Get(id);
            if (filmeActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var filme = filmeActionResult.Value;
            var idsGenerosSelecionados = filme.Generos.Select(x => x.Id).ToList();
            var generosNaoSelecionados = await _db.Genero.Where(x => !idsGenerosSelecionados.Contains(x.Id)).ToListAsync();

            var modelo = new AtualizarFilmeDTO
            {
                Filme = filme.Filme,
                GenerosNaoSelecionados = generosNaoSelecionados,
                GenerosSelecionados = filme.Generos,
                Atores = filme.Atores
            };

            return modelo;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Filme filme)
        {
            if (!string.IsNullOrWhiteSpace(filme.Poster))
            {
                var fotoPoster = Convert.FromBase64String(filme.Poster);
                filme.Poster = await _armazenadorArquivo.SalvarArquivo(fotoPoster, "jpg", "filme");
            }

            if (filme.FilmePessoa != null)
            {
                for (var i = 0; i < filme.FilmePessoa.Count; i++)
                {
                    filme.FilmePessoa[i].Ordem = i + 1;
                }
            }

            _db.Filme.Add(filme);
            await _db.SaveChangesAsync();
            return filme.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Filme filme)
        {
            var filmeCadastrado = await _db.Filme.FirstOrDefaultAsync(x => x.Id == filme.Id);

            if (filmeCadastrado == null)
            {
                return NotFound();
            }

            filmeCadastrado = _mapper.Map(filme, filmeCadastrado);

            if (!string.IsNullOrWhiteSpace(filme.Poster))
            {
                var arquivo = Convert.FromBase64String(filme.Poster);
                filmeCadastrado.Poster = await _armazenadorArquivo.EditarArquivo(arquivo, "jpg", "filme", filmeCadastrado.Poster);
            }

            await _db.Database.ExecuteSqlRawAsync($"DELETE FROM GeneroFilme WHERE IdFilme = {filme.Id}");
            await _db.Database.ExecuteSqlRawAsync($"DELETE FROM FilmePessoa WHERE IdFilme = {filme.Id}");

            if (filme.FilmePessoa != null)
            {
                for (var i = 0; i < filme.FilmePessoa.Count; i++)
                {
                    filme.FilmePessoa[i].Ordem = i + 1;
                }
            }

            filmeCadastrado.FilmePessoa = filme.FilmePessoa;
            filmeCadastrado.GeneroFilme = filme.GeneroFilme;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var excluir = await _db.Filme.FirstOrDefaultAsync(x => x.Id == id);
            if (excluir == null)
            {
                return NotFound();
            }

            _db.Filme.Remove(excluir);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}