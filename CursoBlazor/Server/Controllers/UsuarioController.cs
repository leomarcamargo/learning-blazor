using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoBlazor.Server.Helpers;
using CursoBlazor.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioController(ApplicationDbContext db,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get([FromQuery] PaginacaoDTO paginacao)
        {
            var queryable = _db.Users.AsQueryable();

            await HttpContext.InserirParamentroPaginacaoResposta(queryable, paginacao.QuantidadeRegistro);

            return await queryable.Paginar(paginacao).Select(x => new UsuarioDTO
            {
                IdUsuario = x.Id,
                Email = x.Email
            }).ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolesDTO>>> Get()
        {
            return await _db.Roles
                .Select(x => new RolesDTO
                {
                    IdRole = x.Id,
                    Nome = x.Name 
                }).ToListAsync();
        }

        [HttpPost("adicionarrole")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRoleDTO editar)
        {
            var usuario = await _userManager.FindByIdAsync(editar.IdUsuario);
            await _userManager.AddToRoleAsync(usuario, editar.IdRole);
            return NoContent();
        }

        [HttpPost("removerrole")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRoleDTO editar)
        {
            var usuario = await _userManager.FindByIdAsync(editar.IdUsuario);
            await _userManager.RemoveFromRoleAsync(usuario, editar.IdRole);
            return NoContent();
        }
    }
}