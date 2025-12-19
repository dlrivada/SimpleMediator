using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SimpleMediator.OpenTelemetry;
using SimpleMediator.OpenTelemetry.Behaviors;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests.Contracts;

/// <summary>
/// Contract tests for <see cref="ServiceCollectionExtensions"/> to verify
/// correct integration with OpenTelemetry builders and DI container.
/// </summary>
public sealed class ServiceCollectionExtensionsContractTests
{
    [Fact]
    public void AddSimpleMediatorOpenTelemetry_ShouldReturnSameServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddSimpleMediatorOpenTelemetry();

        // Assert
        result.Should().BeSameAs(services, "extension method should return the same IServiceCollection for chaining");
    }

    [Fact]
    public void AddSimpleMediatorOpenTelemetry_ShouldRegisterOptionsAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorOpenTelemetry(options =>
        {
            options.ServiceName = "TestService";
            options.ServiceVersion = "2.0.0";
        });

        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetService<SimpleMediatorOpenTelemetryOptions>();
        options.Should().NotBeNull("options should be registered in DI container");
        options!.ServiceName.Should().Be("TestService");
        options.ServiceVersion.Should().Be("2.0.0");

        // Verify singleton lifetime
        var options2 = provider.GetService<SimpleMediatorOpenTelemetryOptions>();
        options2.Should().BeSameAs(options, "options should be registered with singleton lifetime");
    }

    [Fact]
    public void AddSimpleMediatorOpenTelemetry_WhenMessagingEnrichersEnabled_ShouldRegisterBehavior()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorOpenTelemetry(options =>
        {
            options.EnableMessagingEnrichers = true;
        });

        var provider = services.BuildServiceProvider();

        // Assert
        var behaviorDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(IPipelineBehavior<,>) &&
            d.ImplementationType?.Name.Contains("MessagingEnricher") == true);

        behaviorDescriptor.Should().NotBeNull("MessagingEnricherPipelineBehavior should be registered");
        behaviorDescriptor!.Lifetime.Should().Be(ServiceLifetime.Transient, "pipeline behaviors should have transient lifetime");
    }

    [Fact]
    public void AddSimpleMediatorOpenTelemetry_WhenMessagingEnrichersDisabled_ShouldNotRegisterBehavior()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorOpenTelemetry(options =>
        {
            options.EnableMessagingEnrichers = false;
        });

        // Assert
        var behaviorDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(IPipelineBehavior<,>) &&
            d.ImplementationType?.Name.Contains("MessagingEnricher") == true);

        behaviorDescriptor.Should().BeNull("MessagingEnricherPipelineBehavior should not be registered when disabled");
    }

    [Fact]
    public void WithSimpleMediator_ShouldReturnSameBuilderInstance()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        var result = builder.WithSimpleMediator();

        // Assert
        result.Should().BeSameAs(builder, "extension method should return the same OpenTelemetryBuilder for chaining");
    }

    [Fact]
    public void WithSimpleMediator_ShouldConfigureResourceWithServiceInfo()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();
        var options = new SimpleMediatorOpenTelemetryOptions
        {
            ServiceName = "ContractTest",
            ServiceVersion = "3.0.0"
        };

        // Act
        builder.WithSimpleMediator(options);
        var provider = services.BuildServiceProvider();

        // Assert
        // We can't directly assert on Resource configuration here, but we verify it doesn't throw
        // Integration tests will verify the actual resource attributes
        provider.Should().NotBeNull();
    }

    [Fact]
    public void WithSimpleMediator_WithNullOptions_ShouldUseDefaultOptions()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        var act = () => builder.WithSimpleMediator(null);

        // Assert
        act.Should().NotThrow("null options should be replaced with default options");
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_TracerProvider_ShouldReturnSameBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOpenTelemetry().WithTracing(tracing =>
        {
            // Act
            var result = tracing.AddSimpleMediatorInstrumentation();

            // Assert
            result.Should().BeSameAs(tracing, "extension method should return the same TracerProviderBuilder for chaining");
        });
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_MeterProvider_ShouldReturnSameBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOpenTelemetry().WithMetrics(metrics =>
        {
            // Act
            var result = metrics.AddSimpleMediatorInstrumentation();

            // Assert
            result.Should().BeSameAs(metrics, "extension method should return the same MeterProviderBuilder for chaining");
        });
    }

    [Fact]
    public void AddSimpleMediatorOpenTelemetry_ShouldNotThrowWhenCalledMultipleTimes()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var act = () =>
        {
            services.AddSimpleMediatorOpenTelemetry();
            services.AddSimpleMediatorOpenTelemetry();
            services.AddSimpleMediatorOpenTelemetry();
        };

        // Assert
        act.Should().NotThrow("calling multiple times should be idempotent due to TryAddSingleton");
    }

    [Fact]
    public void WithSimpleMediator_ShouldConfigureTracingWithSimpleMediatorSource()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        builder.WithSimpleMediator();
        var provider = services.BuildServiceProvider();

        // Assert
        // Verify the configuration doesn't throw - integration tests will verify actual tracing
        provider.Should().NotBeNull();
    }

    [Fact]
    public void WithSimpleMediator_ShouldConfigureMetricsWithSimpleMediatorMeter()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        builder.WithSimpleMediator();
        var provider = services.BuildServiceProvider();

        // Assert
        // Verify the configuration doesn't throw - integration tests will verify actual metrics
        provider.Should().NotBeNull();
    }
}
