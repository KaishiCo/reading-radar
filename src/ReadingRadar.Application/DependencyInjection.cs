using Microsoft.Extensions.DependencyInjection;

namespace ReadingRadar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

        return services;
    }
}
