using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CursoBlazor.Client.Helpers
{
    public class MostrarMensagem : IMostrarMensagem
    {

        private readonly IJSRuntime _js;

        public MostrarMensagem(IJSRuntime js)
        {
            _js = js;
        }

        public async Task MostrarMensagemErro(string mensagem)
        {
            await Mensagem("Ops", mensagem, "error");
        }

        public async Task MostrarMensagemSucesso(string mensagem)
        {
            await Mensagem("Sucesso", mensagem, "success");
        }

        private async ValueTask Mensagem(string titulo, string mensagem, string tipo)
        {
            await _js.InvokeVoidAsync("Swal.fire", titulo, mensagem, tipo);
        }
    }
}
