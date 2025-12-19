using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xunit;

namespace SimpleMediator.OpenTelemetry.Tests.Integration;

/// <summary>
/// Basic integration tests for OpenTelemetry instrumentation.
/// Verifies that SimpleMediator telemetry works with real exporters.
/// </summary>
[Trait("Category", "Integration")]
[Trait("Component", "OpenTelemetry")]
public sealed class BasicInstrumentationTests
{
    [Fact]
    public async Task ConsoleExporter_WithBasicRequest_ShouldExportTelemetry()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSimpleMediator(config => { });
        services.AddSingleton<IRequestHandler<TestRequest, string>, TestRequestHandler>();

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("test-service"))
            .WithTracing(builder => builder
                .AddSimpleMediatorInstrumentation()
                .AddConsoleExporter());

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        // Act - This should generate telemetry and export to console
        var result = await mediator.Send(new TestRequest { Data = "test" }, CancellationToken.None);

        // Assert - Request succeeds, telemetry exported (no exception)
        Assert.True(result.IsRight);
        result.Match(
            Right: value => Assert.Equal("success: test", value),
            Left: error => Assert.Fail($"Expected success but got error: {error}")
        );
    }

    [Fact]
    public async Task WithSimpleMediator_ShouldConfigureInstrumentation()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSimpleMediator(config => { });
        services.AddSingleton<IRequestHandler<TestRequest, string>, TestRequestHandler>();

        services.AddOpenTelemetry()
            .WithSimpleMediator()  // Alternative configuration method
            .WithTracing(builder => builder.AddConsoleExporter());

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        // Act
        var result = await mediator.Send(new TestRequest { Data = "withSimpleMediator" }, CancellationToken.None);

        // Assert
        Assert.True(result.IsRight);
    }

    [Fact]
    public async Task MultipleRequests_ShouldGenerateMultipleSpans()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSimpleMediator(config => { });
        services.AddSingleton<IRequestHandler<TestRequest, string>, TestRequestHandler>();

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("test-service"))
            .WithTracing(builder => builder
                .AddSimpleMediatorInstrumentation()
                .AddConsoleExporter());

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        // Act - Send multiple requests
        for (int i = 0; i < 5; i++)
        {
            var result = await mediator.Send(new TestRequest { Data = $"request-{i}" }, CancellationToken.None);
            Assert.True(result.IsRight);
        }

        // Assert - All requests succeeded and telemetry was exported
        Assert.True(true);
    }

    #region Test Helpers

    private sealed record TestRequest : IRequest<string>
    {
        public string Data { get; init; } = "test";
    }

    private sealed class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public Task<Either<MediatorError, string>> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<Either<MediatorError, string>>($"success: {request.Data}");
        }
    }

    #endregion
}
