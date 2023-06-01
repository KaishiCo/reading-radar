using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingRadar.Application.Common.Interfaces.Persistence;
using ReadingRadar.Application.Common.Interfaces.Persistence.Repositories;
using ReadingRadar.Application.Common.Interfaces.Services;
using ReadingRadar.Infra.Persistence;
using ReadingRadar.Infra.Persistence.Repositories;
using ReadingRadar.Infra.Services;

namespace ReadingRadar.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDbInitializer, DbInitializer>()
            .AddSingleton<IDataSeedService, DataSeedService>()
            .AddSingleton<IBookRepository, BookRepository>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IActivityRepository, ActivityRepository>()
            .AddSingleton<ISeriesRepository, SeriesRepository>()
            .AddSingleton<IRadarRepository, RadarRepository>()
            .AddSingleton<IDbConnectionFactory, PgsqlConnectionFactory>(_ =>
                new PgsqlConnectionFactory(config["Database:ConnectionString"]!));

        return services;
    }
}
