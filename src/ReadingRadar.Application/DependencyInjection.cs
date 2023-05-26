using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReadingRadar.Application.Common.Behaviors;

namespace ReadingRadar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

        return services;
    }
}
