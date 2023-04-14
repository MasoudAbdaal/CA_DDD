using API;
using Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();
}

var app = builder.Build();

{//Request PipeLine Configurations
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/error");

    app.MapControllers();

    app.Run();
}