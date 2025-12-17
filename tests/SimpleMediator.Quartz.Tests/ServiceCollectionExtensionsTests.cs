using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SimpleMediator.Quartz;

namespace SimpleMediator.Quartz.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSimpleMediatorQuartz_RegistersQuartzServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorQuartz();

        // Assert - Verify Quartz services are registered
        var provider = services.BuildServiceProvider();
        var schedulerFactory = provider.GetService<ISchedulerFactory>();
        schedulerFactory.Should().NotBeNull();
    }

    [Fact]
    public void AddSimpleMediatorQuartz_RegistersQuartzHostedService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorQuartz();

        // Assert - Verify hosted service is registered
        services.Should().Contain(sd =>
            sd.ServiceType.Name.Contains("IHostedService"));
    }

    [Fact]
    public void AddSimpleMediatorQuartz_AllowsCustomConfiguration()
    {
        // Arrange
        var services = new ServiceCollection();
        var configurationCalled = false;

        // Act
        services.AddSimpleMediatorQuartz(config =>
        {
            configurationCalled = true;
            // Verify configuration action is called
            // (UseMicrosoftDependencyInjectionJobFactory is now default and obsolete)
        });

        // Assert
        configurationCalled.Should().BeTrue();
    }

    [Fact]
    public void AddSimpleMediatorQuartz_CanBeCalledMultipleTimes()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSimpleMediatorQuartz();
        services.AddSimpleMediatorQuartz(); // Should not throw

        // Assert
        services.Should().NotBeEmpty();
    }

    // Note: ScheduleRequest, ScheduleNotification, AddRequestJob, and AddNotificationJob
    // are extension methods that require IScheduler or IServiceCollectionQuartzConfigurator
    // These would be better tested as integration tests with a running Quartz scheduler
    // For unit tests, we've verified the service registration works correctly

    // Test types
    public record TestRequest(string Data) : IRequest<TestResponse>;
    public record TestResponse(string Result);
    public record TestNotification(string Message) : INotification;
}
