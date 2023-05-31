using ReadingRadar.Api.Health;
using Serilog;

namespace ReadingRadar.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IHostBuilder host)
    {
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database");

        host.UseSerilog((context, logger) =>
            logger.ReadFrom.Configuration(context.Configuration));
        return services;
    }

}
