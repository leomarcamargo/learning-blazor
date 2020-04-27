using System;
using System.Collections.Generic;
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
    public class PessoaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IArmazenadorArquivo _armazenadorArquivo;
        private readonly IMapper _mapper;

        public PessoaController(ApplicationDbContext db, IArmazenadorArquivo armazenadorArquivo, IMapper mapper)
        {
            _db = db;
            _armazenadorArquivo = armazenadorArquivo;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<Pessoa>>> Get([FromQuery] PaginacaoDTO paginacao)
        {
            var queryable = _db.Pessoa.AsQueryable();

            await HttpContext.InserirParamentroPaginacaoResposta(queryable, paginacao.QuantidadeRegistro);

            return await _db.Pessoa.Paginar(paginacao).ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
            var pessoa =  await _db.Pessoa.FirstOrDefaultAsync(x => x.Id == id);
            
            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        [HttpGet("pesquisar/{textoPesquisa}")]
        public async Task<List<Pessoa>> Get(string textoPesquisa)
        {
            if (string.IsNullOrWhiteSpace(textoPesquisa))
            {
                return new List<Pessoa>();
            }

            textoPesquisa = textoPesquisa.ToLower();
            return await _db.Pessoa.Where(x => x.Nome.ToLower().Contains(textoPesquisa)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pessoa pessoa)
        {
            if (!string.IsNullOrWhiteSpace(pessoa.Foto))
            {
                var fotoPessoa = Convert.FromBase64String(pessoa.Foto);
                pessoa.Foto = await _armazenadorArquivo.SalvarArquivo(fotoPessoa, "jpg", "pessoa");
            }

            _db.Pessoa.Add(pessoa);
            await _db.SaveChangesAsync();
            return pessoa.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Pessoa pessoa)
        {
            var pessoaCadastrada = await _db.Pessoa.FirstOrDefaultAsync(x => x.Id == pessoa.Id);
            if (pessoaCadastrada == null)
            {
                return NotFound();
            }

            pessoaCadastrada = _mapper.Map(pessoa, pessoaCadastrada);

            if (!string.IsNullOrWhiteSpace(pessoa.Foto))
            {
                var arquivo = Convert.FromBase64String(pessoa.Foto);
                pessoaCadastrada.Foto =
                    await _armazenadorArquivo.EditarArquivo(arquivo, "jpg", "pessoa", pessoaCadastrada.Foto);
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var excluir = await _db.Pessoa.FirstOrDefaultAsync(x => x.Id == id);
            if (excluir == null)
            {
                return NotFound();
            }

            _db.Pessoa.Remove(excluir);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("visualizar/{id}")]
        public async Task<ActionResult<VisualizarPessoaDTO>> Visualizar(int id)
        {
            var pessoa = await _db.Pessoa
                .Include(x => x.FilmePessoa)
                .ThenInclude(x => x.Filme)
                .ThenInclude(x => x.GeneroFilme)
                .ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            var filmes = pessoa.FilmePessoa
                .OrderByDescending(x => x.Filme.DataLancamento)
                .Select(x => x.Filme)
                .ToList();

            var model = new VisualizarPessoaDTO
            {
                Pessoa = pessoa,
                Filmes = filmes
            };

            return model;
        }
    }
}