
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        // builder.Services.AddSingleton<ProblemDetailsFactory, CA_DDD_ProblemDetailsFactory>();
        services.AddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();

        services.AddMapping();

        return services;
    }
}