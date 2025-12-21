using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleMediator.MassTransit;

/// <summary>
/// Extension methods for configuring SimpleMediator with MassTransit integration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds SimpleMediator MassTransit integration to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSimpleMediatorMassTransit(this IServiceCollection services)
    {
        return services.AddSimpleMediatorMassTransit(_ => { });
    }

    /// <summary>
    /// Adds SimpleMediator MassTransit integration to the service collection with configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Action to configure MassTransit options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSimpleMediatorMassTransit(
        this IServiceCollection services,
        Action<SimpleMediatorMassTransitOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);

        // Register the message publisher
        services.TryAddTransient<IMassTransitMessagePublisher, MassTransitMessagePublisher>();

        // Register generic consumer types for open generic registration
        services.TryAddTransient(typeof(MassTransitRequestConsumer<,>));
        services.TryAddTransient(typeof(MassTransitNotificationConsumer<>));

        return services;
    }
}
