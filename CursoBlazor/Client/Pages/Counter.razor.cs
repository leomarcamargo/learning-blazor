using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CursoBlazor.Client.Pages
{
    public class CounterBase : ComponentBase
    {
        [Inject] protected ServicoSingleton Singleton { get; set; }
        [Inject] protected ServicoTransient Transient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationState { get; set; }



        protected int currentCount = 0;
        private static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            var authState = await AuthenticationState;
            var usuario = authState.User;

            if (usuario.Identity.IsAuthenticated)
            {
                currentCount++;
                currentCountStatic++;
            }
            else
            {
                currentCount--;
                currentCountStatic--;
            }
            
            Singleton.Valor = currentCount;
            Transient.Valor = currentCount;
            
            await JS.InvokeVoidAsync("provaPontoNetStatic");
        }

        protected async Task IncrementCountJavascript()
        {
            await JS.InvokeVoidAsync("provaPontoNetInstancia", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> ObterCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
