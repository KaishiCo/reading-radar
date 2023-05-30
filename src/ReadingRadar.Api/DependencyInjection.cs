using ReadingRadar.Api.Health;

namespace ReadingRadar.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database");
        return services;
    }

}
