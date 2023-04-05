using Serilog;
using System.Diagnostics;

namespace MangaFatihi.WebApi.Utilities.Extensions
{
    public static class LoggerConfig
    {
        public static WebApplicationBuilder UseCustomSerilogConfig(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateBootstrapLogger();

            Log.Information("Logging Start!");

            builder.Logging.ClearProviders();

            builder.Host.UseSerilog();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg)); Serilog.Debugging.SelfLog.Enable(Console.Error);

            return builder;
        }

    }
}
