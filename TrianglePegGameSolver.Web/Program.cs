using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Analytics;
using Microsoft.Extensions.Configuration;
using TrianglePegGameSolver.Web.Application;

namespace TrianglePegGameSolver.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

        builder.Services.AddApplication();

        var trackingId = builder.Configuration.GetValue<string>("Analytics:GoogleTag");
        builder.Services.AddGoogleAnalytics(trackingId);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.SetMinimumLevel(LogLevel.None);

        builder.Logging.AddSerilog();

        await builder.Build().RunAsync();
    }
}