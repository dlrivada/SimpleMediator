using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleMediator.Marten;

/// <summary>
/// Extension methods for configuring SimpleMediator with Marten integration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds SimpleMediator Marten integration to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSimpleMediatorMarten(this IServiceCollection services)
    {
        return services.AddSimpleMediatorMarten(_ => { });
    }

    /// <summary>
    /// Adds SimpleMediator Marten integration to the service collection with configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Action to configure Marten options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSimpleMediatorMarten(
        this IServiceCollection services,
        Action<SimpleMediatorMartenOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);

        // Register the open generic aggregate repository
        services.TryAddScoped(typeof(IAggregateRepository<>), typeof(MartenAggregateRepository<>));

        return services;
    }

    /// <summary>
    /// Adds a specific aggregate repository to the service collection.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddAggregateRepository<TAggregate>(this IServiceCollection services)
        where TAggregate : class, IAggregate
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddScoped<IAggregateRepository<TAggregate>, MartenAggregateRepository<TAggregate>>();

        return services;
    }
}
