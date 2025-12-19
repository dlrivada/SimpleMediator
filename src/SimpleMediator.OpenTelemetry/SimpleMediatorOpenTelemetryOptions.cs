namespace SimpleMediator.OpenTelemetry;

/// <summary>
/// Configuration options for SimpleMediator OpenTelemetry integration.
/// </summary>
public sealed class SimpleMediatorOpenTelemetryOptions
{
    /// <summary>
    /// Gets or sets the service name for OpenTelemetry.
    /// </summary>
    public string ServiceName { get; set; } = "SimpleMediator";

    /// <summary>
    /// Gets or sets the service version for OpenTelemetry.
    /// </summary>
    public string ServiceVersion { get; set; } = "1.0.0";

    /// <summary>
    /// Gets or sets a value indicating whether to automatically enrich activities with messaging pattern context.
    /// </summary>
    /// <value>
    /// Default is <c>true</c>. When enabled, the <see cref="Behaviors.MessagingEnricherPipelineBehavior{TRequest,TResponse}"/>
    /// will be registered to automatically add Outbox, Inbox, Saga, and Scheduling metadata to OpenTelemetry activities.
    /// </value>
    public bool EnableMessagingEnrichers { get; set; } = true;
}
