using Blazor.FileReader;
using CursoBlazor.Client.Helpers;
using CursoBlazor.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CursoBlazor.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ServicoSingleton>();
            services.AddTransient<ServicoTransient>();
            services.AddScoped<IRepository, Repository.Repository>();
            services.AddScoped<IMostrarMensagem, MostrarMensagem>();
            services.AddSingleton<CustomHttpClientFactory>();

            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddApiAuthorization();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
