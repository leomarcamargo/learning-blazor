using System.Collections.Generic;
using System.Threading.Tasks;
using CursoBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public GeneroController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<List<Genero>>> Get()
        {
            return await _db.Genero.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await _db.Genero.FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Genero genero)
        {
            _db.Genero.Add(genero);
            await _db.SaveChangesAsync();
            return genero.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genero genero)
        {
            _db.Attach(genero).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var excluir = await _db.Genero.FirstOrDefaultAsync(x => x.Id == id);
            if (excluir == null)
            {
                return NotFound();
            }

            _db.Genero.Remove(excluir);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}