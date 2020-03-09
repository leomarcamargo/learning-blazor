using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CursoBlazor.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InserirParamentroPaginacaoResposta<T>(this HttpContext context,
            IQueryable<T> queryable, int quantidadeTotalMostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double quantidade = await queryable.CountAsync();
            var totalPaginas = Math.Ceiling(quantidade / quantidadeTotalMostrar);

            context.Response.Headers.Add("quantidade", quantidade.ToString(CultureInfo.InvariantCulture));
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString(CultureInfo.InvariantCulture));
        }
    }
}
