using BlazorDownloadFile;
using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Elecciones.Client.Application;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Elecciones.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationProvider>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ApiClient>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSweetAlert2();
            builder.Services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.MaxDisplayedToasts = 5;
                config.ProgressBarClass = Defaults.Classes.ProgressBarClass;
                config.CloseIconClass = Defaults.Classes.CloseIconClass;
                config.ShowCloseIcon = true;

                config.VisibleStateDuration = 10000;
                config.ShowProgressBar = true;

                config.ShowTransitionDuration = 100;
                config.HideTransitionDuration = 100;
            });
            builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);



            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}