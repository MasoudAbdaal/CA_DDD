var builder = WebApplication.CreateBuilder(args);
{
    //Configuration and Dependency Injection
    builder.Services.AddControllers();
}

var app = builder.Build();

{//Request PipeLine Configurations
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}