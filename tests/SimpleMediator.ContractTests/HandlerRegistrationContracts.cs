using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace SimpleMediator.ContractTests;

public sealed class HandlerRegistrationContracts
{
    [Fact]
    public void RequestHandlersAreRegisteredScopedByDefault()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(typeof(HandlerRegistrationContracts).Assembly);

        services.ShouldContain(d =>
            d.ServiceType == typeof(global::SimpleMediator.IRequestHandler<SampleCommand, string>)
            && ImplementationMatches(d, typeof(SampleCommandHandler))
            && d.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void RequestHandlersHonorConfiguredLifetime()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(cfg =>
        {
            cfg.WithHandlerLifetime(ServiceLifetime.Singleton);
        }, typeof(HandlerRegistrationContracts).Assembly);

        services.ShouldContain(d =>
            d.ServiceType == typeof(global::SimpleMediator.IRequestHandler<SampleCommand, string>)
            && ImplementationMatches(d, typeof(SampleCommandHandler))
            && d.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void RequestHandlersAreRegisteredOnlyOnceAcrossInvocations()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(typeof(HandlerRegistrationContracts).Assembly);
        services.AddSimpleMediator(typeof(HandlerRegistrationContracts).Assembly);

        var descriptors = services
            .Where(d => d.ServiceType == typeof(global::SimpleMediator.IRequestHandler<SampleCommand, string>))
            .ToList();

        descriptors.Count.ShouldBe(1, "Request handlers should not be duplicated when registration runs multiple times.");
    }

    [Fact]
    public void NotificationHandlersAllowMultipleImplementations()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(typeof(HandlerRegistrationContracts).Assembly);

        var descriptors = services
            .Where(d => d.ServiceType == typeof(global::SimpleMediator.INotificationHandler<SampleNotification>))
            .ToList();

        descriptors.Count.ShouldBe(2, "All notification handlers should be preserved during registration.");
        descriptors.ShouldContain(d => ImplementationMatches(d, typeof(SampleNotificationHandlerOne)));
        descriptors.ShouldContain(d => ImplementationMatches(d, typeof(SampleNotificationHandlerTwo)));
    }

    private static bool ImplementationMatches(ServiceDescriptor descriptor, Type candidate)
    {
        return descriptor.ImplementationType == candidate
               || descriptor.ImplementationInstance?.GetType() == candidate;
    }

    private sealed record SampleCommand(string Payload) : global::SimpleMediator.ICommand<string>;

    private sealed class SampleCommandHandler : global::SimpleMediator.ICommandHandler<SampleCommand, string>
    {
        public Task<string> Handle(SampleCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.Payload);
        }
    }

    private sealed record SampleNotification(string Value) : global::SimpleMediator.INotification;

    private sealed class SampleNotificationHandlerOne : global::SimpleMediator.INotificationHandler<SampleNotification>
    {
        public Task Handle(SampleNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    private sealed class SampleNotificationHandlerTwo : global::SimpleMediator.INotificationHandler<SampleNotification>
    {
        public Task Handle(SampleNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
