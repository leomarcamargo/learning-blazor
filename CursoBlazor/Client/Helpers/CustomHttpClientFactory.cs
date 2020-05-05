using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CursoBlazor.Client.Helpers
{
    public class CustomHttpClientFactory
    {
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _provedorAutenticacao;
        private readonly NavigationManager _navigationManager;
        private const string CampoAutorizacao = "Authorization";

        public CustomHttpClientFactory(HttpClient httpClient,
            IAccessTokenProvider provedorAutenticacao,
            NavigationManager navigationManager)
        {
            this._httpClient = httpClient;
            this._provedorAutenticacao = provedorAutenticacao;
            this._navigationManager = navigationManager;
        }

        public HttpClient ObterHttpClientSemToken()
        {
            if (_httpClient.DefaultRequestHeaders.Contains(CampoAutorizacao))
            {
                _httpClient.DefaultRequestHeaders.Remove(CampoAutorizacao);
            }

            return _httpClient;
        }

        public async Task<HttpClient> ObterHTTPClientComToken()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains(CampoAutorizacao))
            {
                var resultadoToken = await _provedorAutenticacao.RequestAccessToken();

                if (resultadoToken.TryGetToken(out var token))
                {
                    _httpClient.DefaultRequestHeaders.Add(CampoAutorizacao, $"Bearer {token.Value}");
                }
                else
                {
                    _navigationManager.NavigateTo(resultadoToken.RedirectUrl);
                }
            }

            return _httpClient;
        }
    }
}
