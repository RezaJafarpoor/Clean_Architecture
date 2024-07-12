using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Common;
using Restaurants.Application.UserContext;

namespace Restaurants.Application.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();
        services.AddScoped<IUserContext, UserContext.UserContext>();
        services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        services.AddHttpContextAccessor();
    }
}