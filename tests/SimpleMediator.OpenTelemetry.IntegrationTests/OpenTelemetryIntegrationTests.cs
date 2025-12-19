using FluentAssertions;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests.Integration;

[Trait("Category", "Integration")]
public class OpenTelemetryIntegrationTests
{
    [Fact]
    public async Task AddSimpleMediatorInstrumentation_Should_Register_ActivitySource()
    {
        var services = new ServiceCollection();
        services.AddSimpleMediator(config => { });
        services.AddSingleton<IRequestHandler<PingQuery, string>, PingHandler>();
        services.AddOpenTelemetry()
            .WithTracing(builder => builder
                .AddSimpleMediatorInstrumentation()
                .AddConsoleExporter());

        var sp = services.BuildServiceProvider();
        var mediator = sp.GetRequiredService<IMediator>();

        var result = await mediator.Send(new PingQuery(), default);
        result.IsRight.Should().BeTrue();
    }

    public record PingQuery : IQuery<string>;

    public class PingHandler : IRequestHandler<PingQuery, string>
    {
        public async Task<Either<MediatorError, string>> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return "Pong";
        }
    }
}
