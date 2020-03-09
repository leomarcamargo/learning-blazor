using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CursoBlazor.Server.Helpers
{
    public class ArmazenadorArquivoLocal : IArmazenadorArquivo
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArmazenadorArquivoLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> EditarArquivo(byte[] conteudo, string extensao, string nomeContainer, string diretorioAtual)
        {
            if (!string.IsNullOrEmpty(diretorioAtual))
            {
                await RemoverArquivo(diretorioAtual, nomeContainer);
            }

            return await SalvarArquivo(conteudo, extensao, nomeContainer);
        }

        public Task RemoverArquivo(string diretorio, string nomeContainer)
        {
            var nomeArquivo = Path.GetFileName(diretorio);
            var diretorioArquivo = Path.Combine(_webHostEnvironment.WebRootPath, nomeContainer, nomeArquivo);

            if (File.Exists(diretorioArquivo))
            {
                File.Delete(diretorioArquivo);
            }

            return Task.FromResult(0);
        }

        public async Task<string> SalvarArquivo(byte[] conteudo, string extensao, string nomeContainer)
        {
            var nomeArquivo = $"{Guid.NewGuid()}.{extensao}";
            var pasta = Path.Combine(_webHostEnvironment.WebRootPath, nomeContainer);

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            var caminhoSalvo = Path.Combine(pasta, nomeArquivo);
            await File.WriteAllBytesAsync(caminhoSalvo, conteudo);

            var urlAtual =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var urlBancoDados = Path.Combine(urlAtual, nomeContainer, nomeArquivo);
            return urlBancoDados;
        }
    }
}
