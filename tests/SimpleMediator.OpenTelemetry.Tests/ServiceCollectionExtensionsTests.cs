using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests;

/// <summary>
/// Tests for <see cref="ServiceCollectionExtensions"/>.
/// </summary>
public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void WithSimpleMediator_ShouldReturnBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        var result = builder.WithSimpleMediator();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(builder);
    }

    [Fact]
    public void WithSimpleMediator_WithOptions_ShouldReturnBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();
        var options = new SimpleMediatorOpenTelemetryOptions
        {
            ServiceName = "TestService",
            ServiceVersion = "1.2.3"
        };

        // Act
        var result = builder.WithSimpleMediator(options);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(builder);
    }

    [Fact]
    public void WithSimpleMediator_WithNullOptions_ShouldUseDefaults()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        var result = builder.WithSimpleMediator(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(builder);
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_TracerProviderBuilder_ShouldReturnBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var telemetryBuilder = services.AddOpenTelemetry();
        TracerProviderBuilder? tracerBuilder = null;

        telemetryBuilder.WithTracing(builder =>
        {
            tracerBuilder = builder;
        });

        // Act & Assert
        tracerBuilder.Should().NotBeNull();
        var result = tracerBuilder!.AddSimpleMediatorInstrumentation();
        result.Should().NotBeNull();
        result.Should().BeSameAs(tracerBuilder);
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_MeterProviderBuilder_ShouldReturnBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var telemetryBuilder = services.AddOpenTelemetry();
        MeterProviderBuilder? meterBuilder = null;

        telemetryBuilder.WithMetrics(builder =>
        {
            meterBuilder = builder;
        });

        // Act & Assert
        meterBuilder.Should().NotBeNull();
        var result = meterBuilder!.AddSimpleMediatorInstrumentation();
        result.Should().NotBeNull();
        result.Should().BeSameAs(meterBuilder);
    }
}
