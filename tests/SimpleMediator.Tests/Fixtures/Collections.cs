using Xunit;

namespace SimpleMediator.Tests.Fixtures;

[CollectionDefinition("PipelineBehaviors", DisableParallelization = true)]
public sealed class PipelineBehaviorsTestGroup : ICollectionFixture<object>
{
}
