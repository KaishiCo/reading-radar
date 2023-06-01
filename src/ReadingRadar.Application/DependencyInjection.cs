using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingRadar.Application.Common.Behaviors;
using ReadingRadar.Application.Features.Events;

namespace ReadingRadar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblyContaining<IApplicationMarker>())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddValidatorsFromAssemblyContaining<IApplicationMarker>();

        services.AddMessageBroker(config);
        return services;
    }

    private static void AddMessageBroker(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<RadarUpsertedEventConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(config["RabbitMq:ConnectionString"]!), "/", h =>
                {
                    h.Username(config["RabbitMq:Username"]!);
                    h.Password(config["RabbitMq:Password"]!);
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
