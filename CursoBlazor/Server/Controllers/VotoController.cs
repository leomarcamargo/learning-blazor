using System;
using System.Threading.Tasks;
using CursoBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public VotoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Votar(VotoFilme votoFilme)
        {
            var usuario = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var idUsuario = usuario.Id;
            var votoAtual = await _db.VotoFilme
                .FirstOrDefaultAsync(x => x.IdFilme == votoFilme.IdFilme && x.IdUsuario == idUsuario);

            if (votoAtual == null)
            {
                votoFilme.IdUsuario = idUsuario;
                votoFilme.DataVoto = DateTime.Today;
                _db.Add(votoFilme);
                await _db.SaveChangesAsync();
            }
            else
            {
                votoAtual.Voto = votoFilme.Voto;
                await _db.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}