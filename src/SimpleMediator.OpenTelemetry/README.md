# SimpleMediator.OpenTelemetry

OpenTelemetry integration for SimpleMediator - automatic tracing, metrics, and logging for mediator operations.

## Installation

```bash
dotnet add package SimpleMediator.OpenTelemetry
```

## Features

- **Automatic Tracing**: Integrates with SimpleMediator's built-in `ActivitySource` for distributed tracing
- **Metrics Collection**: Exposes mediator metrics through OpenTelemetry's `Meter` API
- **Runtime Instrumentation**: Includes .NET runtime metrics (GC, ThreadPool, etc.)
- **Easy Configuration**: Simple extension methods for OpenTelemetry builders

## Quick Start

### Basic Configuration

```csharp
using SimpleMediator.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

// Add SimpleMediator
builder.Services.AddSimpleMediator(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});

// Add OpenTelemetry with SimpleMediator instrumentation
builder.Services.AddOpenTelemetry()
    .WithSimpleMediator(new SimpleMediatorOpenTelemetryOptions
    {
        ServiceName = "MyApplication",
        ServiceVersion = "1.0.0"
    })
    .WithTracing(tracing =>
    {
        tracing.AddConsoleExporter(); // or Jaeger, Zipkin, etc.
    })
    .WithMetrics(metrics =>
    {
        metrics.AddConsoleExporter(); // or Prometheus, etc.
    });

var app = builder.Build();
app.Run();
```

### Advanced Configuration

```csharp
builder.Services.AddOpenTelemetry()
    .WithSimpleMediator(new SimpleMediatorOpenTelemetryOptions
    {
        ServiceName = "MyApplication",
        ServiceVersion = "1.0.0"
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddJaegerExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:14268");
            });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddPrometheusExporter();
    });
```

### Using Individual Builders

You can also add SimpleMediator instrumentation to existing OpenTelemetry builder pipelines:

```csharp
// For tracing only
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSimpleMediatorInstrumentation()
               .AddConsoleExporter();
    });

// For metrics only
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddSimpleMediatorInstrumentation()
               .AddConsoleExporter();
    });
```

## What Gets Instrumented?

### Tracing

SimpleMediator automatically creates `Activity` spans for:

- **Commands**: Each command execution creates a span tagged with `request.kind=command`
- **Queries**: Each query execution creates a span tagged with `request.kind=query`
- **Notifications**: Each notification dispatch creates spans for fan-out handlers

All spans include:
- Request type name
- Handler type name
- Execution duration
- Success/failure status
- Error details (if failed)

### Metrics

SimpleMediator exposes the following metrics:

| Metric Name | Type | Description | Labels |
|-------------|------|-------------|--------|
| `simplemediator.request.success` | Counter | Successful requests | `request.kind`, `request.name` |
| `simplemediator.request.failure` | Counter | Failed requests | `request.kind`, `request.name`, `failure.reason` |
| `simplemediator.request.duration` | Histogram | Request execution time (ms) | `request.kind`, `request.name` |

## Configuration Options

### SimpleMediatorOpenTelemetryOptions

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ServiceName` | `string` | `"SimpleMediator"` | Service name for OpenTelemetry resource |
| `ServiceVersion` | `string` | `"1.0.0"` | Service version for OpenTelemetry resource |

## Integration with Observability Platforms

### Jaeger (Distributed Tracing)

```csharp
builder.Services.AddOpenTelemetry()
    .WithSimpleMediator()
    .WithTracing(tracing =>
    {
        tracing.AddJaegerExporter(options =>
        {
            options.AgentHost = "localhost";
            options.AgentPort = 6831;
        });
    });
```

### Prometheus (Metrics)

```csharp
builder.Services.AddOpenTelemetry()
    .WithSimpleMediator()
    .WithMetrics(metrics =>
    {
        metrics.AddPrometheusExporter();
    });

app.UseOpenTelemetryPrometheusScrapingEndpoint(); // Exposes /metrics endpoint
```

### Azure Monitor / Application Insights

```csharp
builder.Services.AddOpenTelemetry()
    .WithSimpleMediator()
    .UseAzureMonitor(options =>
    {
        options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
    });
```

## Best Practices

1. **Set Meaningful Service Names**: Use descriptive service names that reflect your application's purpose
2. **Include Version Information**: Always set `ServiceVersion` for tracking deployments
3. **Use Sampling in Production**: Configure trace sampling to reduce overhead:
   ```csharp
   tracing.SetSampler(new TraceIdRatioBasedSampler(0.1)); // 10% sampling
   ```
4. **Monitor Metric Cardinality**: Be cautious with high-cardinality labels
5. **Correlate Logs with Traces**: Use OpenTelemetry's logging integration for complete observability

## Performance Considerations

- **Minimal Overhead**: SimpleMediator's built-in instrumentation uses `ActivitySource` which is highly optimized
- **Zero Cost When Disabled**: If no listeners are registered, instrumentation has near-zero overhead
- **Async-Friendly**: All instrumentation is async-aware and doesn't block execution

## Examples

See the `/samples` directory in the SimpleMediator repository for complete examples.

## Contributing

Contributions are welcome! Please see the main SimpleMediator repository for contribution guidelines.

## License

This project is licensed under the same license as SimpleMediator. See LICENSE for details.

## Resources

- [SimpleMediator Documentation](https://github.com/yourusername/SimpleMediator)
- [OpenTelemetry .NET Documentation](https://opentelemetry.io/docs/languages/net/)
- [OpenTelemetry Specification](https://opentelemetry.io/docs/specs/otel/)

---

**Sources:**
- [Microsoft.CodeAnalysis.PublicApiAnalyzers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.PublicApiAnalyzers/)
- [.NET 10 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [PublicApiAnalyzers Help](https://github.com/dotnet/roslyn-analyzers/blob/ab7019ee000e20e0b822f6fca7d64eef4e09995d/src/PublicApiAnalyzers/PublicApiAnalyzers.Help.md)
