using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace CursoBlazor.Client.Auth
{
    public class ProvedorAutenticacaoProva : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimo = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("chave1", "valor1"),
                new Claim(ClaimTypes.Name, "Leomar"),
                //new Claim(ClaimTypes.Role, "admin")
            });
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimo)));
        }
    }
}
