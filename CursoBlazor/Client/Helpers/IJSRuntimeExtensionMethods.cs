using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CursoBlazor.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string msg)
        {
            //await js.InvokeVoidAsync("console.log", "1");
            return await js.InvokeAsync<bool>("confirm", msg);
        }
    }
}
