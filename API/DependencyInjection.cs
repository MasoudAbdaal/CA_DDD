
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;
using Serilog.Extensions.Hosting;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        // builder.Services.AddSingleton<ProblemDetailsFactory, CA_DDD_ProblemDetailsFactory>();
        services.AddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();

        var log = new LoggerConfiguration()
       .MinimumLevel.Information()
       .Enrich.FromLogContext()
       .WriteTo.File("logs/logs.txt", Serilog.Events.LogEventLevel.Information)
       .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
       .CreateLogger();

        services.AddLogging(cfg =>
        {
            cfg.ClearProviders();
            cfg.AddSerilog(log);
        });

        log.Information("DOOOOOOOOOOOONE");

        services.AddMapping();

        return services;
    }

}