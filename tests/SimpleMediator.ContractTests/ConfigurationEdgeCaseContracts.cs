using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace SimpleMediator.ContractTests;

public sealed class ConfigurationEdgeCaseContracts
{
    [Fact]
    public void AddSimpleMediator_WithExplicitAssemblies_RegistersHandlersFromAllSources()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(typeof(global::SimpleMediator.SimpleMediator).Assembly, typeof(ConfigurationEdgeCaseContracts).Assembly);

        services.ShouldContain(d =>
            d.ServiceType == typeof(global::SimpleMediator.IRequestHandler<TestCommand, string>)
            && ImplementationMatches(d, typeof(TestCommandHandler))
            && d.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void AddSimpleMediator_IgnoresDuplicateAssemblies()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(typeof(ConfigurationEdgeCaseContracts).Assembly, typeof(ConfigurationEdgeCaseContracts).Assembly);

        var descriptors = services
            .Where(d => d.ServiceType == typeof(global::SimpleMediator.IRequestHandler<TestCommand, string>))
            .ToList();

        descriptors.Count.ShouldBe(1, "Handlers should not be registered multiple times when assemblies repeat.");
    }

    [Fact]
    public void AddSimpleMediator_WithNoAssemblies_FallsBackToDefaults()
    {
        var services = new ServiceCollection();

        services.AddSimpleMediator(Array.Empty<Assembly>());

        var pipelineDescriptors = services.Where(IsPipelineDescriptor).ToList();
        pipelineDescriptors.Count.ShouldBe(4, "Default pipeline behaviors should remain intact when no assemblies are provided.");
    }

    private static bool ImplementationMatches(ServiceDescriptor descriptor, Type candidate)
    {
        return descriptor.ImplementationType == candidate
               || descriptor.ImplementationInstance?.GetType() == candidate;
    }

    private static bool IsPipelineDescriptor(ServiceDescriptor descriptor)
    {
        return descriptor.ServiceType.IsGenericType
               && descriptor.ServiceType.GetGenericTypeDefinition() == typeof(global::SimpleMediator.IPipelineBehavior<,>);
    }

    private sealed record TestCommand(string Payload) : global::SimpleMediator.ICommand<string>;

    private sealed class TestCommandHandler : global::SimpleMediator.ICommandHandler<TestCommand, string>
    {
        public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.Payload);
        }
    }
}
