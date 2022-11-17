using SepCityCMS.Client.Helpers;
using SepCityCMS.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;

namespace SepCityCMS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services
                .AddScoped<IAlertService, AlertService>()
                .AddScoped<ILocalStorageService, LocalStorageService>();

            builder.Services.AddScoped(x => {
                return new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            });

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}