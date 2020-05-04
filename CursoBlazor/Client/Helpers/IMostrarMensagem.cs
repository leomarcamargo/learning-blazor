using System.Threading.Tasks;

namespace CursoBlazor.Client.Helpers
{
    public interface IMostrarMensagem
    {
        Task MostrarMensagemErro(string mensagem);
        Task MostrarMensagemSucesso(string mensagem);
    }
}
