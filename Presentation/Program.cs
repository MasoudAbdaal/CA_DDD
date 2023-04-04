using Application;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication();

    //Configuration and Dependency Injection
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, CA_DDD_ProblemDetaulsFactory>();
}

var app = builder.Build();

{//Request PipeLine Configurations
    app.UseHttpsRedirection();

    app.UseExceptionHandler("/error");

    app.MapControllers();

    app.Run();
}