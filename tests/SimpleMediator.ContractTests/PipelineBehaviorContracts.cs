using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shouldly;
using Xunit;

namespace SimpleMediator.ContractTests;

public sealed class PipelineBehaviorContracts
{
    private static readonly Assembly TargetAssembly = typeof(global::SimpleMediator.SimpleMediator).Assembly;

    [Fact]
    public void PipelineBehaviorsImplementSpecializedInterfaces()
    {
        var behaviors = GetPipelineBehaviorTypes();

        behaviors.ShouldNotBeEmpty();
        foreach (var behavior in behaviors)
        {
            var implementsCommand = ImplementsGenericInterface(behavior, typeof(global::SimpleMediator.ICommandPipelineBehavior<,>));
            var implementsQuery = ImplementsGenericInterface(behavior, typeof(global::SimpleMediator.IQueryPipelineBehavior<,>));

            (implementsCommand || implementsQuery)
                .ShouldBeTrue($"Pipeline behavior {behavior.Name} must implement a specialized command/query interface.");
        }
    }

    [Fact]
    public void AssemblyScannerDiscoversAllPipelineBehaviors()
    {
        var expected = new HashSet<Type>
        {
            typeof(global::SimpleMediator.CommandActivityPipelineBehavior<,>),
            typeof(global::SimpleMediator.CommandMetricsPipelineBehavior<,>),
            typeof(global::SimpleMediator.QueryActivityPipelineBehavior<,>),
            typeof(global::SimpleMediator.QueryMetricsPipelineBehavior<,>)
        };

        var result = global::SimpleMediator.MediatorAssemblyScanner.GetRegistrations(TargetAssembly);
        var discovered = result.PipelineRegistrations
            .Where(r => r.ImplementationType.Assembly == TargetAssembly)
            .Select(r => r.ImplementationType.IsGenericType ? r.ImplementationType.GetGenericTypeDefinition() : r.ImplementationType)
            .ToHashSet();

        discovered.ShouldBe(expected, comparer: TypeEqualityComparer.Instance);
    }

    private static IReadOnlyCollection<Type> GetPipelineBehaviorTypes()
    {
        return TargetAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsGenericTypeDefinition)
            .Where(t => ImplementsGenericInterface(t, typeof(global::SimpleMediator.IPipelineBehavior<,>)))
            .ToArray();
    }

    private static bool ImplementsGenericInterface(Type candidate, Type genericInterface)
    {
        return candidate
            .GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface);
    }

    private sealed class TypeEqualityComparer : IEqualityComparer<Type>
    {
        public static readonly TypeEqualityComparer Instance = new();

        public bool Equals(Type? x, Type? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (x.IsGenericTypeDefinition && y.IsGenericTypeDefinition)
            {
                return x == y;
            }

            if (x.IsGenericTypeDefinition || y.IsGenericTypeDefinition)
            {
                return false;
            }

            return x == y;
        }

        public int GetHashCode(Type obj) => obj.GetHashCode();
    }
}
