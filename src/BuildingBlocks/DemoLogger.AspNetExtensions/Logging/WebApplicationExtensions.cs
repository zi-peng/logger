using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
namespace DemoLogger.AspNetExtensions.Logging;

public static class WebApplicationBuilderExtension
{
    private const string OutputTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} <{ThreadId}> [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static IHostBuilder UseSerilogConfiguration(this IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: OutputTemplate)
            .WriteTo.Seq("http://localhost:5341")
            .WriteTo.File(Path.Combine("logs", "error-.log"), restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Month,
                outputTemplate: OutputTemplate
            ).WriteTo.File(Path.Combine("logs", "accessLogs-.log"), restrictedToMinimumLevel: LogEventLevel.Information,
                rollingInterval: RollingInterval.Day,
                outputTemplate: OutputTemplate
            ).CreateLogger();
        return builder.UseSerilog();
    }
}