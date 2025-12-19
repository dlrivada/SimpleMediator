using FluentAssertions;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests;

/// <summary>
/// Tests for <see cref="SimpleMediatorOpenTelemetryOptions"/>.
/// </summary>
public sealed class SimpleMediatorOpenTelemetryOptionsTests
{
    [Fact]
    public void Constructor_ShouldSetDefaultValues()
    {
        // Act
        var options = new SimpleMediatorOpenTelemetryOptions();

        // Assert
        options.ServiceName.Should().Be("SimpleMediator");
        options.ServiceVersion.Should().Be("1.0.0");
    }

    [Fact]
    public void ServiceName_ShouldBeSettable()
    {
        // Arrange
        var options = new SimpleMediatorOpenTelemetryOptions();
        const string serviceName = "MyCustomService";

        // Act
        options.ServiceName = serviceName;

        // Assert
        options.ServiceName.Should().Be(serviceName);
    }

    [Fact]
    public void ServiceVersion_ShouldBeSettable()
    {
        // Arrange
        var options = new SimpleMediatorOpenTelemetryOptions();
        const string version = "2.0.0";

        // Act
        options.ServiceVersion = version;

        // Assert
        options.ServiceVersion.Should().Be(version);
    }
}
