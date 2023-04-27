using API;
using Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

    // builder.WebHost.UseUrls("https://localhost:4002");
}

// builder.Host.UseSerilog((context, conf) =>
// conf.WriteTo.Console().WriteTo.File("program-logs/log.txt").Enrich.FromLogContext().MinimumLevel.Information()
// );
var app = builder.Build();

{//Request PipeLine Configurations
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/error");

    app.MapControllers();

    // app.UseSerilogRequestLogging();

    app.Run();
}