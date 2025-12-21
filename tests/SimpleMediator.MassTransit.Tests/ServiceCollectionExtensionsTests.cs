using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleMediator.MassTransit;

namespace SimpleMediator.MassTransit.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSimpleMediatorMassTransit_RegistersOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorMassTransit();
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetService<IOptions<SimpleMediatorMassTransitOptions>>();
        options.Should().NotBeNull();
        options!.Value.Should().NotBeNull();
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_WithConfiguration_AppliesOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorMassTransit(options =>
        {
            options.ThrowOnMediatorError = false;
            options.QueueNamePrefix = "custom-prefix";
            options.AutoRegisterRequestConsumers = false;
            options.AutoRegisterNotificationConsumers = false;
        });
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<SimpleMediatorMassTransitOptions>>();
        options.Value.ThrowOnMediatorError.Should().BeFalse();
        options.Value.QueueNamePrefix.Should().Be("custom-prefix");
        options.Value.AutoRegisterRequestConsumers.Should().BeFalse();
        options.Value.AutoRegisterNotificationConsumers.Should().BeFalse();
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_RegistersMessagePublisher()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorMassTransit();
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IMassTransitMessagePublisher));

        // Assert
        descriptor.Should().NotBeNull();
        descriptor!.ImplementationType.Should().Be(typeof(MassTransitMessagePublisher));
        descriptor.Lifetime.Should().Be(ServiceLifetime.Transient);
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_RegistersGenericRequestConsumer()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorMassTransit();
        var descriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(MassTransitRequestConsumer<,>));

        // Assert
        descriptor.Should().NotBeNull();
        descriptor!.Lifetime.Should().Be(ServiceLifetime.Transient);
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_RegistersGenericNotificationConsumer()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorMassTransit();
        var descriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(MassTransitNotificationConsumer<>));

        // Assert
        descriptor.Should().NotBeNull();
        descriptor!.Lifetime.Should().Be(ServiceLifetime.Transient);
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_ReturnsServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddSimpleMediatorMassTransit();

        // Assert
        result.Should().BeSameAs(services);
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_WithNullServices_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            ((IServiceCollection)null!).AddSimpleMediatorMassTransit());
    }

    [Fact]
    public void AddSimpleMediatorMassTransit_WithNullConfigure_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            services.AddSimpleMediatorMassTransit(null!));
    }

    [Fact]
    public void SimpleMediatorMassTransitOptions_HasCorrectDefaults()
    {
        // Arrange & Act
        var options = new SimpleMediatorMassTransitOptions();

        // Assert
        options.ThrowOnMediatorError.Should().BeTrue();
        options.QueueNamePrefix.Should().Be("simplemediator");
        options.AutoRegisterRequestConsumers.Should().BeTrue();
        options.AutoRegisterNotificationConsumers.Should().BeTrue();
    }
}
