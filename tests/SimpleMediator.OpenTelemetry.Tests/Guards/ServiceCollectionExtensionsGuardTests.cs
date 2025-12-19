using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using SimpleMediator.OpenTelemetry;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests.Guards;

/// <summary>
/// Guard clause tests for <see cref="ServiceCollectionExtensions"/>.
/// </summary>
public sealed class ServiceCollectionExtensionsGuardTests
{
    [Fact]
    public void AddSimpleMediatorOpenTelemetry_WithNullServices_ShouldThrow()
    {
        // Act
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorOpenTelemetry(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("services");
    }

    [Fact]
    public void AddSimpleMediatorOpenTelemetry_WithNullConfigure_ShouldNotThrow()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var act = () => services.AddSimpleMediatorOpenTelemetry(null);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void WithSimpleMediator_WithNullBuilder_ShouldThrow()
    {
        // Act
        var act = () => ServiceCollectionExtensions.WithSimpleMediator(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("builder");
    }

    [Fact]
    public void WithSimpleMediator_WithNullOptions_ShouldNotThrow()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = services.AddOpenTelemetry();

        // Act
        var act = () => builder.WithSimpleMediator(null);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_TracerProvider_WithNullBuilder_ShouldThrow()
    {
        // Act
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorInstrumentation((global::OpenTelemetry.Trace.TracerProviderBuilder)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("builder");
    }

    [Fact]
    public void AddSimpleMediatorInstrumentation_MeterProvider_WithNullBuilder_ShouldThrow()
    {
        // Act
        var act = () => ServiceCollectionExtensions.AddSimpleMediatorInstrumentation((global::OpenTelemetry.Metrics.MeterProviderBuilder)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("builder");
    }
}
