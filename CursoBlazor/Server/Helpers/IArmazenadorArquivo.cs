using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoBlazor.Server.Helpers
{
    public interface IArmazenadorArquivo
    {
        Task<string> EditarArquivo(byte[] conteudo, string extensao, string nomeContainer, string diretorioAtual);
        Task RemoverArquivo(string diretorio, string nomeContainer);
        Task<string> SalvarArquivo(byte[] conteudo, string extensao, string nomeContainer);
    }
}
