using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ReadingRadar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

        return services;
    }
}
