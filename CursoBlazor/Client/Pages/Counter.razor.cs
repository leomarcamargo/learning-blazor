using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CursoBlazor.Client.Pages
{
    public class CounterBase : ComponentBase
    {
        [Inject] protected ServicoSingleton Singleton { get; set; }
        [Inject] protected ServicoTransient Transient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }

        
        protected int currentCount = 0;
        private static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            currentCount++;
            Singleton.Valor = currentCount;
            Transient.Valor = currentCount;
            currentCountStatic++;
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
