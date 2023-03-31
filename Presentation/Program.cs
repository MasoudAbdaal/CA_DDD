using Application.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    //Configuration and Dependency Injection
    builder.Services.AddControllers();
}

var app = builder.Build();

{//Request PipeLine Configurations
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}