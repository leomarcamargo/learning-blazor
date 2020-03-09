using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Client.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarResposta<T>(responseHttp, JsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJson = JsonSerializer.Serialize(enviar);
            var enviarConteudo = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, enviarConteudo);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJson = JsonSerializer.Serialize(enviar);
            var enviarConteudo = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, enviarConteudo);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJson = JsonSerializer.Serialize(enviar);
            var enviarConteudo = new StringContent(enviarJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, enviarConteudo);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarResposta<TResponse>(responseHttp, JsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var respostaHttp = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !respostaHttp.IsSuccessStatusCode, respostaHttp);
        }

        private async Task<T> DeserializarResposta<T>(HttpResponseMessage httpResponseMessage, JsonSerializerOptions jsonSerializerOptions)
        {
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, jsonSerializerOptions);
        }

        public List<Filme> ObterFilmes()
        {
            return new List<Filme>
            {
                new Filme
                {
                    Titulo = "Spider-Man: Far From Home",
                    DataLancamento = new DateTime(2019, 7, 2),
                    Poster = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_UX182_CR0,0,182,268_AL_.jpg"
                },
                new Filme
                {
                    Titulo = "Moana",
                    DataLancamento = new DateTime(2016, 11, 23),
                    Poster = "https://m.media-amazon.com/images/M/MV5BMjI4MzU5NTExNF5BMl5BanBnXkFtZTgwNzY1MTEwMDI@._V1_UX182_CR0,0,182,268_AL_.jpg"
                },
                new Filme
                {
                    Titulo = "Inception",
                    DataLancamento = new DateTime(2010, 7, 16),
                    Poster = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_UX182_CR0,0,182,268_AL_.jpg"
                }
            };
        }
    }
}
