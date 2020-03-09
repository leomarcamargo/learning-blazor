using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
