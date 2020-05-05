using System.Collections.Generic;
using System.Threading.Tasks;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Client.Repository
{
    public interface IRepository
    {
        List<Filme> ObterFilmes();
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<T>> Get<T>(string url, bool incluirToken = true);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Delete(string url);
    }
}
