using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CursoBlazor.Client.Helpers;
using CursoBlazor.Client.Repository;
using CursoBlazor.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CursoBlazor.Client.Auth
{
    public class ProvedorAutenticacaoJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _httpClient;
        private readonly IRepository _repository;
        private const string TokenKey = "TOKENKEY";
        private const string ExpirationTokenKey = "EXPIRATION   TOKENKEY";
        private AuthenticationState Anonimo => 
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public ProvedorAutenticacaoJWT(IJSRuntime js, HttpClient httpClient, IRepository repository)
        {
            _js = js;
            _httpClient = httpClient;
            _repository = repository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.GetFromLocalStorage(TokenKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                return Anonimo;
            }

            var tempoExpiracao = await _js.GetFromLocalStorage(ExpirationTokenKey);

            if (DateTime.TryParse(tempoExpiracao, out var tempo))
            {
                if (TokenExpirado(tempo))
                {
                    await Limpar();
                    return Anonimo;
                }

                if (DeveRenovarToken(tempo))
                {
                    token = await RenovarToken(token);
                }
            }

            return ConstruirAuthenticationState(token);
        }

        public async Task ConduzirRenovacaoToken()
        {
            var tempoExpiracao = await _js.GetFromLocalStorage(ExpirationTokenKey);

            if (DateTime.TryParse(tempoExpiracao, out var tempo))
            {
                if (TokenExpirado(tempo))
                {
                    await Logout();
                }

                if (DeveRenovarToken(tempo))
                {
                    var token = await _js.GetFromLocalStorage(TokenKey);
                    var novoToken = await RenovarToken(token);

                    var authState = ConstruirAuthenticationState(novoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private async Task<string> RenovarToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var novoTokenResponse = await _repository.Get<UserToken>("api/conta/renovartoken");
            var novoToken = novoTokenResponse.Response;
            await _js.SetInLocalStorage(TokenKey, token);
            await _js.SetInLocalStorage(ExpirationTokenKey, novoToken.Expiration.ToString(CultureInfo.InvariantCulture));

            return novoToken.Token;
        }

        private bool DeveRenovarToken(DateTime tempoExpiracao)
        {
            return tempoExpiracao.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool TokenExpirado(DateTime tempoExpiracao)
        {
            return tempoExpiracao <= DateTime.UtcNow;
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken userToken)
        {
            await _js.SetInLocalStorage(TokenKey, userToken.Token);
            await _js.SetInLocalStorage(ExpirationTokenKey, userToken.Expiration.ToString(CultureInfo.InvariantCulture));
            var authState = ConstruirAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private async Task Limpar()
        {
            await _js.RemoveItem(TokenKey);
            await _js.RemoveItem(ExpirationTokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
