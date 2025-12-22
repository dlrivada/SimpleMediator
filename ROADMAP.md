# SimpleMediator Roadmap

**Last Updated**: 2025-12-22
**Version**: Pre-1.0 (breaking changes allowed)
**Future Name**: Encina (to be renamed before 1.0)

---

## Vision

SimpleMediator (future: **Encina**) is a functional mediation library for .NET that enables building modern applications with **Railway Oriented Programming** as the core philosophy.

### Design Principles

- **Functional First**: Pure ROP with `Either<MediatorError, T>` as first-class citizen
- **Explicit over Implicit**: Code should be clear and predictable
- **Performance Conscious**: Zero-allocation hot paths, Expression tree compilation
- **Composable**: Behaviors are small, composable units
- **Pay-for-What-You-Use**: All features are opt-in

---

## Project Status: 90% to Pre-1.0

| Category | Packages | Status |
|----------|----------|--------|
| Core & Validation | 5 | ✅ Production |
| Web Integration | 3 | ✅ Production |
| Database Providers | 12 | ✅ Production |
| Messaging Transports | 13 | ✅ Production |
| Caching | 8 | ✅ Production |
| Job Scheduling | 2 | ✅ Production |
| Resilience | 4 | ✅ Production |
| Event Sourcing | 2 | ✅ Production |
| Observability | 1 | ✅ Production |
| **Developer Tooling** | 0/3 | 📋 Pending |
| **EDA Enhancements** | 0/4 | 📋 Pending |
| **Microservices Enhancements** | 0/4 | 📋 Pending |
| **Modular Monolith** | 0/1 | 📋 Pending |
| **Serverless** | 0/2 | 📋 Pending |
| **DDD Tactical Patterns** | 0/1 | 📋 Pending |
| **TDD Tooling** | 0/1 | 📋 Pending |

### Quality Metrics

| Metric | Current | Target | Status |
|--------|---------|--------|--------|
| Line Coverage | 67.1% | ≥85% | 🟡 Needs work |
| Branch Coverage | 70.9% | ≥80% | 🟡 Needs work |
| Mutation Score | 79.75% | ≥80% | ✅ Achieved |
| Build Warnings | 0 | 0 | ✅ Perfect |
| Tests | 3,803 | ~5,000+ | 🟡 In progress |

### Test Coverage

- **Core Tests**: 265 passing
- **Database Provider Tests**: 1,763 passing (10 providers)
- **Caching Tests**: 367 passing
- **Total**: 3,000+ tests

---

## Completed Features

> Detailed implementation history: [docs/history/2025-12.md](docs/history/2025-12.md)
> Version history: [CHANGELOG.md](CHANGELOG.md)

### Core (5 packages)

- SimpleMediator Core - ROP, pipelines, CQRS
- FluentValidation, DataAnnotations, MiniValidator, GuardClauses

### Web (3 packages)

- AspNetCore - Middleware, authorization, Problem Details
- SignalR - Real-time notifications
- MassTransit - Message bus integration

### Database (12 packages)

- EntityFrameworkCore, MongoDB
- Dapper: SqlServer, PostgreSQL, MySQL, Sqlite, Oracle
- ADO: SqlServer, PostgreSQL, MySQL, Sqlite, Oracle
- Messaging abstractions (Outbox, Inbox, Sagas, Choreography)

### Messaging (12 packages)

- Wolverine, NServiceBus, MassTransit
- RabbitMQ, AzureServiceBus, AmazonSQS, Kafka
- Redis.PubSub, InMemory, NATS, MQTT
- gRPC, GraphQL

### Caching (8 packages)

- Core, Memory, Hybrid
- Redis, Valkey, KeyDB, Dragonfly, Garnet

### Resilience (4 packages)

- Extensions.Resilience, Polly, Refit, Dapr

### Event Sourcing (2 packages)

- Marten, EventStoreDB

### Observability (1 package)

- OpenTelemetry - Distributed tracing and metrics

### Other Features (in Core)

- Stream Requests (IAsyncEnumerable)
- Parallel Notification Dispatch strategies
- Choreography Sagas abstractions (in Messaging)

---

## In Progress

### Test Architecture Refactoring

**Status**: 🔄 In Progress

Restructuring all test projects to use Testcontainers for real database integration testing.

**Completed**:

- ✅ SimpleMediator.TestInfrastructure with shared fixtures
- ✅ Dapper.Sqlite tests refactored (187 tests, 4 projects)
- ✅ Architecture established (1 project per test type)

**Pending**:

- ⏳ Testcontainers fixtures for SQL Server, PostgreSQL, MySQL, Oracle
- ⏳ Remaining provider tests (9 databases × 4 test types)

---

## Pending Features (Pre-1.0)

### Developer Tooling (0% complete)

| Package | Purpose | Priority |
|---------|---------|----------|
| SimpleMediator.Cli | Command-line scaffolding & analysis | ⭐⭐⭐⭐ |
| SimpleMediator.Testing | MediatorFixture fluent API | ⭐⭐⭐⭐ |
| SimpleMediator.OpenApi | Auto-generation from handlers | ⭐⭐⭐ |

### Core Improvements

| Task | Priority | Complexity |
|------|----------|------------|
| Refactor `SimpleMediator.Publish` with guards | ⭐⭐⭐ | Low |
| Optimize delegate caches (minimize reflection) | ⭐⭐⭐ | Medium |
| Replace `object? Details` with `ImmutableDictionary` | ⭐⭐⭐ | Medium |

### Testing Excellence

| Task | Current | Target |
|------|---------|--------|
| Line Coverage | 67.1% | ≥85% |
| Mutation Score | 79.75% | ≥95% |
| Property-based tests | Partial | Complete |
| Load tests | Partial | All providers |

### Event-Driven Architecture Enhancements

| Feature | Package | Priority | Notes |
|---------|---------|----------|-------|
| **Projections/Read Models** | SimpleMediator.Projections | ⭐⭐⭐⭐ | Abstractions for CQRS read side |
| **Event Versioning** | EventStoreDB, Marten | ⭐⭐⭐⭐ | Upcasting, schema evolution |
| **Snapshotting** | EventStoreDB, Marten | ⭐⭐⭐ | For large aggregates |
| **Dead Letter Queue** | Messaging providers | ⭐⭐⭐ | Enhanced DLQ handling |

### Microservices Enhancements

| Feature | Package | Priority | Notes |
|---------|---------|----------|-------|
| **Health Check Abstractions** | Core / AspNetCore | ⭐⭐⭐ | IHealthCheck integration for handler health |
| **Bulkhead Isolation** | Polly | ⭐⭐⭐ | Parallel execution isolation |
| **API Versioning Helpers** | AspNetCore | ⭐⭐ | Contract evolution support |
| **Distributed Lock Abstractions** | SimpleMediator.DistributedLock | ⭐⭐ | IDistributedLock interface |

> **Note**: Service Discovery, Secret Management, and Configuration are delegated to infrastructure (Dapr, Kubernetes, Azure).

### Modular Monolith Support

**Package**: `SimpleMediator.Modules`

Enable true modular monolith architecture with explicit module boundaries, lifecycle management, and controlled inter-module communication.

#### Core Abstractions

| Feature | Priority | Complexity | Notes |
|---------|----------|------------|-------|
| `IModule` interface | ⭐⭐⭐⭐⭐ | Low | Module definition with Name, Assembly, ConfigureServices |
| `IModuleRegistry` | ⭐⭐⭐⭐⭐ | Low | Runtime module discovery and introspection |
| Module lifecycle hooks | ⭐⭐⭐⭐ | Low | OnStartAsync/OnStopAsync for initialization |
| Module-scoped behaviors | ⭐⭐⭐⭐ | Medium | Apply behaviors only to specific modules |
| Event routing with filters | ⭐⭐⭐ | Medium | Selective notification subscription per module |
| Module contracts | ⭐⭐⭐ | High | Compile-time validation of inter-module dependencies |
| Anti-Corruption Layers | ⭐⭐ | High | Translation between module boundaries |

#### Proposed API

```csharp
// Module definition
public interface IModule
{
    string Name { get; }
    Assembly Assembly { get; }
    void ConfigureServices(IServiceCollection services);
    Task OnStartAsync(CancellationToken ct);
    Task OnStopAsync(CancellationToken ct);
}

// Registration
services.AddSimpleMediator()
    .AddModules(modules =>
    {
        modules.Register<OrdersModule>();
        modules.Register<InvoicingModule>();
        modules.Register<ShippingModule>();

        // Explicit contracts between modules
        modules.DefineContract<OrdersModule, InvoicingModule>(contract =>
        {
            contract.Publishes<OrderPlacedEvent>();
            contract.Publishes<OrderCancelledEvent>();
        });
    });

// Event with module scope
[ModuleEvent(SourceModule = "Orders", TargetModules = new[] { "Invoicing", "Shipping" })]
public record OrderPlacedEvent(Guid OrderId) : INotification;
```

#### Current Support (Without Package)

Applications can still use modular patterns today:

- ✅ Assembly-based handler discovery per module
- ✅ `IRequestContext` with TenantId/UserId for isolation
- ✅ Outbox/Inbox/Sagas for reliable inter-module messaging
- ✅ Notifications for event-driven communication

#### Gaps Addressed by This Package

- ❌ → ✅ Explicit module registry and discovery
- ❌ → ✅ Module lifecycle management
- ❌ → ✅ Handler isolation (prevent cross-module collisions)
- ❌ → ✅ Selective event routing (not global broadcast)
- ❌ → ✅ Module boundary enforcement
- ❌ → ✅ Module-scoped pipeline behaviors

### Serverless Integration

First-class support for serverless architectures with Azure Functions and AWS Lambda.

#### Packages

| Package | Priority | Notes |
|---------|----------|-------|
| `SimpleMediator.AzureFunctions` | ⭐⭐⭐⭐ | Azure Functions integration (.NET 10, Flex Consumption) |
| `SimpleMediator.AwsLambda` | ⭐⭐⭐⭐ | AWS Lambda integration (managed instances, containers) |

#### Features

| Feature | Priority | Complexity | Notes |
|---------|----------|------------|-------|
| Function triggers as handlers | ⭐⭐⭐⭐⭐ | Low | HTTP, Timer, Queue, Blob triggers dispatch to mediator |
| Cold start optimization | ⭐⭐⭐⭐ | Medium | Pre-warming, lazy initialization strategies |
| Durable Functions orchestration | ⭐⭐⭐⭐ | Medium | Saga-like workflows with Durable Functions |
| Step Functions integration | ⭐⭐⭐ | Medium | AWS Step Functions state machine support |
| Context propagation | ⭐⭐⭐⭐⭐ | Low | RequestContext from function context/headers |
| OpenTelemetry integration | ⭐⭐⭐⭐ | Low | Distributed tracing across function invocations |

#### Proposed API

```csharp
// Azure Functions
public class OrderFunctions
{
    private readonly IMediator _mediator;

    [Function("CreateOrder")]
    public async Task<IActionResult> CreateOrder(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        var command = await req.ReadFromJsonAsync<CreateOrderCommand>();
        return await _mediator.SendToActionResult(command);
    }

    [Function("ProcessOrderQueue")]
    public async Task ProcessOrder(
        [QueueTrigger("orders")] CreateOrderCommand command,
        FunctionContext context)
    {
        // Context automatically propagated (correlation, tenant, etc.)
        await _mediator.Send(command, context.ToRequestContext());
    }
}

// AWS Lambda
public class OrderHandler : SimpleMediatorLambdaHandler<CreateOrderCommand, OrderResult>
{
    // Automatic serialization, context propagation, error handling
}

// Durable Functions with Sagas
[Function("OrderSagaOrchestrator")]
public async Task<OrderResult> RunOrchestrator(
    [OrchestrationTrigger] TaskOrchestrationContext context)
{
    var saga = new OrderSaga();
    return await _mediator.ExecuteSaga(saga, context);
}
```

#### Current Support (Without Packages)

SimpleMediator works in serverless today but requires manual setup:

- ✅ DI registration in function startup
- ✅ Manual context creation from headers
- ✅ Basic request/response handling

#### Gaps Addressed by These Packages

- ❌ → ✅ Automatic context propagation from function context
- ❌ → ✅ Cold start optimizations (pre-warming behaviors)
- ❌ → ✅ Native trigger-to-handler mapping
- ❌ → ✅ Durable Functions / Step Functions orchestration
- ❌ → ✅ Lambda base classes with automatic serialization
- ❌ → ✅ OpenTelemetry auto-instrumentation for functions

### Domain-Driven Design (DDD) Support

**Package**: `SimpleMediator.DomainModel`

Tactical DDD patterns with first-class ROP integration for building rich domain models.

#### Current Support

| Pattern | Status | Location |
|---------|--------|----------|
| Aggregates | ✅ Strong | `AggregateBase` in Marten/EventStoreDB |
| Domain Events | 🟡 Partial | Via `INotification` + auto-publishing |
| Repositories | ✅ Strong | `IAggregateRepository<T>` with ROP |
| Domain Errors | ✅ Excellent | `MediatorError` + Either monad |
| Value Objects | ❌ Missing | No base class |
| Entities | ❌ Missing | No interface |
| Specifications | ❌ Missing | No support |
| Strongly-Typed IDs | ❌ Missing | No base record |

#### Proposed Abstractions

| Feature | Priority | Complexity | Notes |
|---------|----------|------------|-------|
| `IDomainEvent` interface | ⭐⭐⭐⭐⭐ | Low | AggregateId, OccurredAtUtc, Version |
| `ValueObject` base record | ⭐⭐⭐⭐⭐ | Low | Immutable, equality by value |
| `Entity<TId>` base class | ⭐⭐⭐⭐ | Low | Identity-based equality |
| `StronglyTypedId<T>` record | ⭐⭐⭐⭐ | Low | Avoid primitive obsession |
| `ISpecification<T>` with ROP | ⭐⭐⭐⭐ | Medium | Composable business rules |
| `IDomainService` marker | ⭐⭐⭐ | Low | Documentation/discovery |
| `EnsureInvariant` in AggregateBase | ⭐⭐⭐⭐ | Low | ROP-based invariant validation |

#### Proposed API

```csharp
// Domain Event with metadata
public interface IDomainEvent : INotification
{
    Guid AggregateId { get; }
    DateTimeOffset OccurredAtUtc { get; }
    int Version { get; }
}

// Value Object (immutable, equality by value)
public abstract record ValueObject
{
    protected abstract IEnumerable<object?> GetAtomicValues();
}

public record Money(decimal Amount, string Currency) : ValueObject
{
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}

// Strongly-Typed ID (avoid primitive obsession)
public abstract record StronglyTypedId<T>(T Value) where T : notnull;

public record OrderId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static OrderId New() => new(Guid.NewGuid());
}

// Specification Pattern with ROP
public interface ISpecification<T>
{
    Either<MediatorError, bool> IsSatisfiedBy(T entity);
    ISpecification<T> And(ISpecification<T> other);
    ISpecification<T> Or(ISpecification<T> other);
    ISpecification<T> Not();
}

public class OrderMustBeShippable : ISpecification<Order>
{
    public Either<MediatorError, bool> IsSatisfiedBy(Order order) =>
        order.Status == OrderStatus.Paid && order.Items.Any()
            ? true
            : MediatorError.New("order.not_shippable");
}

// Invariant validation in aggregates
public abstract class AggregateBase
{
    protected Either<MediatorError, Unit> EnsureInvariant(
        bool condition, string errorCode, string message) =>
        condition ? Unit.Default : MediatorError.New(errorCode, message);

    public Either<MediatorError, Unit> Ship() =>
        EnsureInvariant(Status == OrderStatus.Paid, "order.not_paid", "Cannot ship unpaid order")
            .Map(_ => { RaiseEvent(new OrderShipped(Id)); return Unit.Default; });
}
```

### Test-Driven Development (TDD) Support

**Package**: `SimpleMediator.Testing`

Fluent testing API for handlers, aggregates, and pipelines with first-class ROP assertions.

#### Current Support

| Component | Status | Location |
|-----------|--------|----------|
| Test Infrastructure | ✅ Excellent | `SimpleMediator.TestInfrastructure` |
| Database Fixtures | ✅ Strong | Testcontainers (5 DBs) |
| Test Builders | ✅ Strong | `OutboxMessageBuilder`, etc. |
| Handler Testing | 🟡 Partial | Basic fixtures only |
| Assertion Extensions | 🟡 Incomplete | Planned |
| MediatorFixture | ❌ Missing | In Developer Tooling |

#### Proposed Abstractions

| Feature | Priority | Complexity | Notes |
|---------|----------|------------|-------|
| `MediatorFixture` fluent builder | ⭐⭐⭐⭐⭐ | Medium | Configure handlers, behaviors, fakes |
| `AggregateTestBase<T>` | ⭐⭐⭐⭐⭐ | Medium | Given/When/Then for event-sourced aggregates |
| ROP Assertion Extensions | ⭐⭐⭐⭐⭐ | Low | `ShouldBeSuccess()`, `ShouldBeError()` |
| Aggregate Assertions | ⭐⭐⭐⭐ | Low | `ShouldHaveRaisedEvent<T>()` |
| `FakeRepository<T>` | ⭐⭐⭐⭐ | Low | In-memory aggregate store for tests |
| Handler Test Base | ⭐⭐⭐ | Medium | Simplified handler unit testing |

#### Proposed API

```csharp
// MediatorFixture - Fluent test setup
var mediator = new MediatorFixture()
    .WithHandler<CreateOrderHandler>()
    .WithBehavior<ValidationBehavior<CreateOrderCommand, OrderId>>()
    .WithFakeRepository(existingOrder, anotherOrder)
    .Build();

var result = await mediator.Send(new CreateOrderCommand(...));
result.ShouldBeSuccess();

// AggregateTestBase - Given/When/Then for Event Sourcing
public class OrderAggregateTests : AggregateTestBase<Order>
{
    [Fact]
    public void Ship_WhenPaid_RaisesOrderShippedEvent()
    {
        Given(new OrderCreated(orderId), new OrderPaid(orderId));
        When(order => order.Ship());
        Then<OrderShipped>(e => e.OrderId.Should().Be(orderId));
    }

    [Fact]
    public void Ship_WhenNotPaid_ReturnsError()
    {
        Given(new OrderCreated(orderId));
        When(order => order.Ship());
        ThenError("order.not_paid");
    }
}

// ROP Assertion Extensions
public static class MediatorAssertions
{
    public static T ShouldBeSuccess<T>(this Either<MediatorError, T> result);
    public static MediatorError ShouldBeError<T>(this Either<MediatorError, T> result);
    public static void ShouldBeError<T>(this Either<MediatorError, T> result, string code);
}

// Aggregate Assertions
public static class AggregateAssertions
{
    public static void ShouldHaveRaisedEvent<TEvent>(this IAggregate aggregate);
    public static void ShouldHaveRaisedEvent<TEvent>(this IAggregate aggregate, Action<TEvent> assertions);
    public static void ShouldHaveNoUncommittedEvents(this IAggregate aggregate);
}

// Usage
var result = await mediator.Send(command);
result.ShouldBeSuccess();

order.ShouldHaveRaisedEvent<OrderCreated>(e =>
    e.CustomerId.Should().Be(customerId));
```

#### Gaps Addressed by This Package

- ❌ → ✅ Fluent mediator setup for isolated tests
- ❌ → ✅ Given/When/Then syntax for aggregate testing
- ❌ → ✅ Type-safe ROP assertions
- ❌ → ✅ Aggregate event assertions
- ❌ → ✅ In-memory repository fakes
- ❌ → ✅ Reduced boilerplate in test code

### Additional Providers

| Package | Priority | Notes |
|---------|----------|-------|
| SimpleMediator.ODBC | ⭐⭐⭐ | Legacy databases |

---

## Strategic Initiatives (Just Before 1.0)

### Renaming: Encina

**Current Name**: SimpleMediator → **New Name**: Encina

**Why Encina?** Spanish word for holm oak - symbolizing strength, resilience, and longevity.

**Checklist**:

- [ ] Rename GitHub repository
- [ ] Update all namespaces
- [ ] Register new NuGet packages
- [ ] Update documentation

**Timeline**: Complete before 1.0 release

---

## Quality & Security

### Implemented

- ✅ CodeQL scanning on every PR
- ✅ SBOM generation workflow
- ✅ Dependabot enabled
- ✅ TreatWarningsAsErrors=true
- ✅ PublicAPI Analyzers
- ✅ LoggerMessage source generators (CA1848 compliance)

### Planned

- [ ] SLSA Level 2 compliance
- [ ] SonarCloud integration
- [ ] Supply chain security (Sigstore/cosign)

---

## Not Implementing

| Feature | Reason |
|---------|--------|
| Generic Variance | Goes against "explicit over implicit" |
| MediatorResult<T> Wrapper | Either<L,R> from LanguageExt is sufficient |
| Source Generators for Registration | Current Expression trees are fast enough |

See ADR-004 and ADR-005 for detailed rationale.

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

### Pre-1.0 Policy

Any feature can be added/modified/removed without restrictions.

### Post-1.0 Policy

Breaking changes only in major versions.

---

## References

### Inspiration

- [MediatR](https://github.com/jbogard/MediatR)
- [Wolverine](https://wolverine.netlify.app/)
- [LanguageExt](https://github.com/louthy/language-ext)

### Concepts

- [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)

---

**Maintained by**: @dlrivada
**History**: See [docs/history/](docs/history/) for detailed implementation records
**Changelog**: See [CHANGELOG.md](CHANGELOG.md) for version history
