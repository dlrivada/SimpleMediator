# Changelog

All notable changes to SimpleMediator will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- LoggerMessage source generators across all packages for CA1848 compliance (2025-12-21)
- SignalR integration package with MediatorHub base class (2025-12-21)
- MongoDB provider for messaging patterns (2025-12-21)
- EventStoreDB integration for event sourcing (2025-12-21)
- Choreography-based saga abstractions (event-driven) (2025-12-21)

### Changed

- All logging now uses high-performance `LoggerMessage` delegates instead of `ILogger.LogXxx()`

---

## [0.9.0] - 2025-12-21

### Added

#### Messaging Transports (12 packages)

- SimpleMediator.Wolverine - WolverineFx 5.7.1 integration
- SimpleMediator.NServiceBus - NServiceBus 9.2.8 integration
- SimpleMediator.RabbitMQ - RabbitMQ.Client 7.2.0 integration
- SimpleMediator.AzureServiceBus - Azure Service Bus 7.20.1 integration
- SimpleMediator.AmazonSQS - AWS SQS/SNS 4.0.2.3 integration
- SimpleMediator.Kafka - Confluent.Kafka 2.12.0 integration
- SimpleMediator.Redis.PubSub - StackExchange.Redis pub/sub
- SimpleMediator.InMemory - System.Threading.Channels message bus
- SimpleMediator.NATS - NATS.Net 2.6.11 with JetStream support
- SimpleMediator.MQTT - MQTTnet 5.0.1 integration
- SimpleMediator.gRPC - Grpc.AspNetCore 2.71.0 mediator service
- SimpleMediator.GraphQL - HotChocolate 15.1.11 bridge

#### Caching (8 packages)

- SimpleMediator.Caching - Core abstractions (ICacheProvider, ICacheKeyGenerator)
- SimpleMediator.Caching.Memory - IMemoryCache provider (109 tests)
- SimpleMediator.Caching.Hybrid - Microsoft HybridCache provider (.NET 9+)
- SimpleMediator.Caching.Redis - StackExchange.Redis provider
- SimpleMediator.Caching.Garnet - Microsoft Garnet provider
- SimpleMediator.Caching.Valkey - Valkey provider (Redis fork)
- SimpleMediator.Caching.Dragonfly - Dragonfly provider
- SimpleMediator.Caching.KeyDB - KeyDB provider

#### Resilience (3 packages)

- SimpleMediator.Extensions.Resilience - Microsoft standard resilience patterns
- SimpleMediator.Refit - Type-safe REST API clients integration
- SimpleMediator.Dapr - Service mesh integration (invocation, pub/sub, state, bindings, secrets)

---

## [0.8.0] - 2025-12-19

### Added

#### Database Providers (10 packages)

- SimpleMediator.Dapper.SqlServer - SQL Server optimized
- SimpleMediator.Dapper.PostgreSQL - PostgreSQL with Npgsql
- SimpleMediator.Dapper.MySQL - MySQL/MariaDB with MySqlConnector
- SimpleMediator.Dapper.Sqlite - SQLite for testing
- SimpleMediator.Dapper.Oracle - Oracle with ManagedDataAccess
- SimpleMediator.ADO.SqlServer - Raw ADO.NET (fastest)
- SimpleMediator.ADO.PostgreSQL - PostgreSQL optimized
- SimpleMediator.ADO.MySQL - MySQL/MariaDB optimized
- SimpleMediator.ADO.Sqlite - SQLite optimized
- SimpleMediator.ADO.Oracle - Oracle optimized

### Changed

- Established Testcontainers-based test architecture
- Created SimpleMediator.TestInfrastructure for shared test fixtures

---

## [0.7.0] - 2025-12-18

### Added

- SimpleMediator.Hangfire - Background job scheduling with Hangfire
- SimpleMediator.Quartz - Enterprise CRON scheduling with Quartz.NET

---

## [0.6.0] - 2025-12-17

### Added

- SimpleMediator.AspNetCore - Middleware, authorization, Problem Details
- SimpleMediator.Messaging - Shared abstractions for Outbox, Inbox, Sagas
- SimpleMediator.EntityFrameworkCore - EF Core implementation of messaging patterns

---

## [0.5.0] - 2025-12-15

### Added

- SimpleMediator.GuardClauses - Defensive programming with Ardalis.GuardClauses

---

## [0.4.0] - 2025-12-14

### Added

- SimpleMediator.MiniValidator - Lightweight validation (~20KB)

---

## [0.3.0] - 2025-12-13

### Added

- SimpleMediator.DataAnnotations - Zero-dependency validation with .NET attributes

---

## [0.2.0] - 2025-12-12

### Added

- SimpleMediator.FluentValidation - FluentValidation integration

---

## [0.1.0] - 2025-12-10

### Added

- SimpleMediator Core - Pure Railway Oriented Programming with `Either<MediatorError, T>`
- Request/Notification dispatch with Expression tree compilation
- Pipeline pattern (Behaviors, PreProcessors, PostProcessors)
- IRequestContext for ambient context
- Observability with ActivitySource and Metrics
- CQRS markers (ICommand, IQuery)
- PublicAPI Analyzers compliance

---

[Unreleased]: https://github.com/dlrivada/SimpleMediator/compare/v0.9.0...HEAD
[0.9.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.8.0...v0.9.0
[0.8.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.7.0...v0.8.0
[0.7.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.6.0...v0.7.0
[0.6.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.5.0...v0.6.0
[0.5.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.4.0...v0.5.0
[0.4.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.3.0...v0.4.0
[0.3.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/dlrivada/SimpleMediator/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/dlrivada/SimpleMediator/releases/tag/v0.1.0
