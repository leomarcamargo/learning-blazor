using System.Collections.Generic;
using System.Threading.Tasks;
using CursoBlazor.Server.Helpers;
using CursoBlazor.Shared.DTO;
using CursoBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class SalaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SalaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public async Task<ActionResult<List<Sala>>> Get([FromQuery] PaginacaoDTO paginacao)
        {
            var queryable = _db.Sala.AsQueryable();

            await HttpContext.InserirParamentroPaginacaoResposta(queryable, paginacao.QuantidadeRegistro);

            return await queryable.Paginar(paginacao).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> Get(int id)
        {
            var sala = await _db.Sala.FirstOrDefaultAsync(x => x.Id == id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Sala sala)
        {
            _db.Sala.Add(sala);
            await _db.SaveChangesAsync();
            return sala.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Sala sala)
        {
            _db.Attach(sala).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var excluir = await _db.Sala.FirstOrDefaultAsync(x => x.Id == id);
            if (excluir == null)
            {
                return NotFound();
            }

            _db.Sala.Remove(excluir);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}