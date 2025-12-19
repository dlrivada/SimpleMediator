using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SimpleMediator.OpenTelemetry.Behaviors;

namespace SimpleMediator.OpenTelemetry;

/// <summary>
/// Extension methods for configuring SimpleMediator OpenTelemetry integration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds SimpleMediator OpenTelemetry instrumentation to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Optional configuration action.</param>
    /// <returns>The service collection for chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddSimpleMediatorOpenTelemetry(
        this IServiceCollection services,
        Action<SimpleMediatorOpenTelemetryOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var options = new SimpleMediatorOpenTelemetryOptions();
        configure?.Invoke(options);

        services.TryAddSingleton(options);

        // Register messaging enricher behavior if enabled
        if (options.EnableMessagingEnrichers)
        {
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(MessagingEnricherPipelineBehavior<,>));
        }

        return services;
    }

    /// <summary>
    /// Configures OpenTelemetry with SimpleMediator instrumentation.
    /// </summary>
    /// <param name="builder">The OpenTelemetry builder.</param>
    /// <param name="options">The OpenTelemetry options.</param>
    /// <returns>The builder for chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="builder"/> is null.</exception>
    public static OpenTelemetryBuilder WithSimpleMediator(
        this OpenTelemetryBuilder builder,
        SimpleMediatorOpenTelemetryOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        options ??= new SimpleMediatorOpenTelemetryOptions();

        builder.ConfigureResource(resource =>
            resource.AddService(
                serviceName: options.ServiceName,
                serviceVersion: options.ServiceVersion));

        builder.WithTracing(tracing =>
        {
            tracing.AddSource("SimpleMediator");
        });

        builder.WithMetrics(metrics =>
        {
            metrics.AddMeter("SimpleMediator");
            metrics.AddRuntimeInstrumentation();
        });

        return builder;
    }

    /// <summary>
    /// Adds SimpleMediator tracing to the TracerProviderBuilder.
    /// </summary>
    /// <param name="builder">The tracer provider builder.</param>
    /// <returns>The builder for chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="builder"/> is null.</exception>
    public static TracerProviderBuilder AddSimpleMediatorInstrumentation(this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AddSource("SimpleMediator");
    }

    /// <summary>
    /// Adds SimpleMediator metrics to the MeterProviderBuilder.
    /// </summary>
    /// <param name="builder">The meter provider builder.</param>
    /// <returns>The builder for chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="builder"/> is null.</exception>
    public static MeterProviderBuilder AddSimpleMediatorInstrumentation(this MeterProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AddMeter("SimpleMediator");
    }
}
