using System.Threading.Tasks;

namespace CursoBlazor.Client.Helpers
{
    public class MostrarMensagem : IMostrarMensagem
    {
        public async Task MostrarMensagemErro(string mensagem)
        {
            await Task.FromResult(0);
        }
    }
}
