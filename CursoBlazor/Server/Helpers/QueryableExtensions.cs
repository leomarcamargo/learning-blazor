using System.Linq;
using CursoBlazor.Shared.DTO;

namespace CursoBlazor.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacaoDTO paginacao)
        {
            return queryable.Skip((paginacao.Pagina - 1) * paginacao.QuantidadeRegistro)
                .Take(paginacao.QuantidadeRegistro);
        }
    }
}
