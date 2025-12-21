using LanguageExt;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static LanguageExt.Prelude;

namespace SimpleMediator.Marten;

/// <summary>
/// Marten-based implementation of the aggregate repository.
/// </summary>
/// <typeparam name="TAggregate">The aggregate type.</typeparam>
public sealed class MartenAggregateRepository<TAggregate> : IAggregateRepository<TAggregate>
    where TAggregate : class, IAggregate
{
    private readonly IDocumentSession _session;
    private readonly ILogger<MartenAggregateRepository<TAggregate>> _logger;
    private readonly SimpleMediatorMartenOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="MartenAggregateRepository{TAggregate}"/> class.
    /// </summary>
    /// <param name="session">The Marten document session.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="options">The configuration options.</param>
    public MartenAggregateRepository(
        IDocumentSession session,
        ILogger<MartenAggregateRepository<TAggregate>> logger,
        IOptions<SimpleMediatorMartenOptions> options)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);

        _session = session;
        _logger = logger;
        _options = options.Value;
    }

    /// <inheritdoc />
    public async Task<Either<MediatorError, TAggregate>> LoadAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug(
                "Loading aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                id);

            var aggregate = await _session.Events.AggregateStreamAsync<TAggregate>(
                id,
                token: cancellationToken).ConfigureAwait(false);

            if (aggregate is null)
            {
                _logger.LogWarning(
                    "Aggregate {AggregateType} with ID {AggregateId} not found",
                    typeof(TAggregate).Name,
                    id);

                return Left<MediatorError, TAggregate>(
                    MediatorErrors.Create(
                        MartenErrorCodes.AggregateNotFound,
                        $"Aggregate {typeof(TAggregate).Name} with ID {id} was not found."));
            }

            _logger.LogDebug(
                "Loaded aggregate {AggregateType} with ID {AggregateId} at version {Version}",
                typeof(TAggregate).Name,
                id,
                aggregate.Version);

            return Right<MediatorError, TAggregate>(aggregate);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error loading aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                id);

            return Left<MediatorError, TAggregate>(
                MediatorErrors.FromException(
                    MartenErrorCodes.LoadFailed,
                    ex,
                    $"Failed to load aggregate {typeof(TAggregate).Name} with ID {id}."));
        }
    }

    /// <inheritdoc />
    public async Task<Either<MediatorError, TAggregate>> LoadAsync(
        Guid id,
        int version,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug(
                "Loading aggregate {AggregateType} with ID {AggregateId} at version {Version}",
                typeof(TAggregate).Name,
                id,
                version);

            var aggregate = await _session.Events.AggregateStreamAsync<TAggregate>(
                id,
                version: version,
                token: cancellationToken).ConfigureAwait(false);

            if (aggregate is null)
            {
                return Left<MediatorError, TAggregate>(
                    MediatorErrors.Create(
                        MartenErrorCodes.AggregateNotFound,
                        $"Aggregate {typeof(TAggregate).Name} with ID {id} at version {version} was not found."));
            }

            return Right<MediatorError, TAggregate>(aggregate);
        }
        catch (Exception ex)
        {
            return Left<MediatorError, TAggregate>(
                MediatorErrors.FromException(
                    MartenErrorCodes.LoadFailed,
                    ex,
                    $"Failed to load aggregate {typeof(TAggregate).Name} with ID {id} at version {version}."));
        }
    }

    /// <inheritdoc />
    public async Task<Either<MediatorError, Unit>> SaveAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(aggregate);

        try
        {
            var uncommittedEvents = aggregate.UncommittedEvents;
            if (uncommittedEvents.Count == 0)
            {
                _logger.LogDebug(
                    "No uncommitted events for aggregate {AggregateType} with ID {AggregateId}",
                    typeof(TAggregate).Name,
                    aggregate.Id);
                return Right<MediatorError, Unit>(Unit.Default);
            }

            _logger.LogDebug(
                "Saving {EventCount} events for aggregate {AggregateType} with ID {AggregateId}",
                uncommittedEvents.Count,
                typeof(TAggregate).Name,
                aggregate.Id);

            // Append events to the stream
            var expectedVersion = aggregate.Version - uncommittedEvents.Count;
            _session.Events.Append(aggregate.Id, expectedVersion, uncommittedEvents.ToArray());

            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // Clear uncommitted events after successful save
            aggregate.ClearUncommittedEvents();

            _logger.LogInformation(
                "Saved {EventCount} events for aggregate {AggregateType} with ID {AggregateId}",
                uncommittedEvents.Count,
                typeof(TAggregate).Name,
                aggregate.Id);

            return Right<MediatorError, Unit>(Unit.Default);
        }
        catch (Exception ex) when (IsConcurrencyException(ex))
        {
            _logger.LogWarning(
                ex,
                "Concurrency conflict saving aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                aggregate.Id);

            if (_options.ThrowOnConcurrencyConflict)
            {
                throw;
            }

            return Left<MediatorError, Unit>(
                MediatorErrors.FromException(
                    MartenErrorCodes.ConcurrencyConflict,
                    ex,
                    $"Concurrency conflict saving aggregate {typeof(TAggregate).Name} with ID {aggregate.Id}."));
        }
        catch (Exception ex) when (!IsConcurrencyException(ex))
        {
            _logger.LogError(
                ex,
                "Error saving aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                aggregate.Id);

            return Left<MediatorError, Unit>(
                MediatorErrors.FromException(
                    MartenErrorCodes.SaveFailed,
                    ex,
                    $"Failed to save aggregate {typeof(TAggregate).Name} with ID {aggregate.Id}."));
        }
    }

    /// <inheritdoc />
    public async Task<Either<MediatorError, Unit>> CreateAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(aggregate);

        try
        {
            var uncommittedEvents = aggregate.UncommittedEvents;
            if (uncommittedEvents.Count == 0)
            {
                return Left<MediatorError, Unit>(
                    MediatorErrors.Create(
                        MartenErrorCodes.NoEventsToCreate,
                        "Cannot create aggregate without any events."));
            }

            _logger.LogDebug(
                "Creating aggregate {AggregateType} with ID {AggregateId} with {EventCount} events",
                typeof(TAggregate).Name,
                aggregate.Id,
                uncommittedEvents.Count);

            // Start a new stream
            _session.Events.StartStream<TAggregate>(aggregate.Id, uncommittedEvents.ToArray());

            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // Clear uncommitted events after successful save
            aggregate.ClearUncommittedEvents();

            _logger.LogInformation(
                "Created aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                aggregate.Id);

            return Right<MediatorError, Unit>(Unit.Default);
        }
        catch (Exception ex) when (IsStreamCollisionException(ex))
        {
            _logger.LogWarning(
                ex,
                "Stream already exists for aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                aggregate.Id);

            return Left<MediatorError, Unit>(
                MediatorErrors.FromException(
                    MartenErrorCodes.StreamAlreadyExists,
                    ex,
                    $"Stream already exists for aggregate {typeof(TAggregate).Name} with ID {aggregate.Id}."));
        }
        catch (Exception ex) when (!IsStreamCollisionException(ex))
        {
            _logger.LogError(
                ex,
                "Error creating aggregate {AggregateType} with ID {AggregateId}",
                typeof(TAggregate).Name,
                aggregate.Id);

            return Left<MediatorError, Unit>(
                MediatorErrors.FromException(
                    MartenErrorCodes.CreateFailed,
                    ex,
                    $"Failed to create aggregate {typeof(TAggregate).Name} with ID {aggregate.Id}."));
        }
    }

    /// <summary>
    /// Determines if the exception is a concurrency-related exception.
    /// </summary>
    private static bool IsConcurrencyException(Exception ex)
    {
        // Marten v8 uses different exception types
        var typeName = ex.GetType().Name;
        return typeName.Contains("Concurrency", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("UnexpectedMaxEventId", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("EventStreamVersion", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines if the exception is a stream collision exception.
    /// </summary>
    private static bool IsStreamCollisionException(Exception ex)
    {
        var typeName = ex.GetType().Name;
        return typeName.Contains("StreamIdCollision", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("ExistingStream", StringComparison.OrdinalIgnoreCase)
            || typeName.Contains("DuplicateStream", StringComparison.OrdinalIgnoreCase);
    }
}
