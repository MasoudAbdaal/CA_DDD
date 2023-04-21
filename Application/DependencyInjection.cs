using System.Reflection;
using Application.Authentication.Commands;
using Application.Authentication.Common;
using Application.Common.Bahavior;
using Application.Common.Interfaces.Authentication;
using Application.Services.Authentication.Commands;
using Application.Services.Authentication.Queries;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}