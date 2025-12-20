# Provider Comparison Benchmarks

This directory contains BenchmarkDotNet benchmarks comparing the performance of different data access providers (ADO.NET, Dapper, and EF Core) for SimpleMediator's messaging patterns.

## Available Benchmarks

### 1. OutboxProviderComparisonBenchmarks

Compares Outbox pattern performance across three providers:

- **ADO.NET**: Raw ADO.NET implementation with SqliteCommand/SqliteDataReader
- **Dapper**: Micro-ORM with parameterized queries
- **EF Core**: Full ORM with change tracking

**Operations Benchmarked:**

- `AddAsync_Single` - Single message insert
- `AddAsync_Batch10` - Batch insert of 10 messages
- `AddAsync_Batch100` - Batch insert of 100 messages
- `GetPendingMessages_Batch10` - Query 10 pending messages from 50 total
- `GetPendingMessages_Batch100` - Query 100 pending messages from 500 total
- `MarkAsProcessedAsync` - Update message as processed
- `MarkAsFailedAsync` - Update message as failed with error

### 2. InboxProviderComparisonBenchmarks

Compares Inbox pattern performance across three providers for idempotent message processing:

**Operations Benchmarked:**

- `AddAsync_Single` - Single message insert for deduplication
- `AddAsync_Batch10` - Batch insert of 10 messages
- `AddAsync_Batch100` - Batch insert of 100 messages
- `GetMessage_DuplicateCheck` - Check if message exists (idempotency check)
- `MarkAsProcessedAsync` - Mark message as processed with response
- `MarkAsFailedAsync` - Mark message as failed with error
- `GetExpiredMessages_Batch10` - Query 10 expired messages for cleanup
- `RemoveExpiredMessages_Batch10` - Delete 10 expired messages in batch

## Running the Benchmarks

### Run All Benchmarks

```bash
cd benchmarks/SimpleMediator.Benchmarks
dotnet run -c Release
```

### Run Specific Benchmark Class

```bash
# Outbox comparison only
dotnet run -c Release --filter *OutboxProviderComparisonBenchmarks*

# Inbox comparison only
dotnet run -c Release --filter *InboxProviderComparisonBenchmarks*
```

### Run Specific Provider

```bash
# ADO.NET only
dotnet run -c Release --filter *OutboxProviderComparisonBenchmarks* --job short --runtimes net10.0 --allCategories ADO

# Dapper only
dotnet run -c Release --filter *OutboxProviderComparisonBenchmarks* --job short --runtimes net10.0 --allCategories Dapper

# EF Core only
dotnet run -c Release --filter *OutboxProviderComparisonBenchmarks* --job short --runtimes net10.0 --allCategories EFCore
```

## Expected Results Format

```
| Method                        | Provider | Mean      | Error    | Ratio | Rank | Allocated |
|-------------------------------|----------|-----------|----------|-------|------|-----------|
| AddAsync_Single               | ADO      |  63.2 μs  | 1.2 μs   | 1.00  | 1    | 2.5 KB    |
| AddAsync_Single               | Dapper   | 100.5 μs  | 2.1 μs   | 1.59  | 2    | 3.8 KB    |
| AddAsync_Single               | EFCore   | 180.3 μs  | 3.5 μs   | 2.85  | 3    | 8.2 KB    |
| GetPendingMessages_Batch10    | ADO      | 120.1 μs  | 2.3 μs   | 1.00  | 1    | 5.1 KB    |
| GetPendingMessages_Batch10    | Dapper   | 195.8 μs  | 4.2 μs   | 1.63  | 2    | 7.3 KB    |
| GetPendingMessages_Batch10    | EFCore   | 340.2 μs  | 6.8 μs   | 2.83  | 3    | 15.8 KB   |
```

## Key Questions Answered

1. **Which provider is fastest for writes?**
   - Expected: ADO.NET (lowest overhead)
   - Dapper adds SQL generation overhead
   - EF Core adds change tracking and SQL generation

2. **Which provider is fastest for reads?**
   - Expected: ADO.NET (manual mapping)
   - Dapper adds object mapping overhead
   - EF Core adds tracking and materialization

3. **Which provider allocates least memory?**
   - Expected: ADO.NET (minimal allocations)
   - Dapper adds some allocations for SQL generation
   - EF Core allocates for change tracking, proxies, etc.

4. **Which provider should I use?**
   - **ADO.NET**: Maximum performance, manual SQL, no abstraction
   - **Dapper**: Good balance of performance and productivity
   - **EF Core**: Best for complex queries, migrations, change tracking

## Architecture Notes

### Why SQLite Only?

We benchmark only SQLite because:

1. **In-Memory Performance**: `:memory:` databases eliminate network latency
2. **Consistent Results**: No external DB server variables
3. **CI/CD Friendly**: No Docker/infrastructure dependencies
4. **Provider Comparison**: Isolates ORM overhead from DB server differences

For real-world database performance (SQL Server, PostgreSQL, MySQL, Oracle):
- Network latency dominates (100-1000x slower than in-memory)
- Provider differences become less significant
- Use load testing instead of micro-benchmarks

### Type Ambiguity Resolution

Each provider (ADO.NET, Dapper, EF Core) defines its own `OutboxMessage`/`InboxMessage` classes.
We use namespace aliases to resolve ambiguity:

```csharp
using ADOOutbox = SimpleMediator.ADO.Sqlite.Outbox;
using DapperOutbox = SimpleMediator.Dapper.Sqlite.Outbox;
using EFOutbox = SimpleMediator.EntityFrameworkCore.Outbox;

// Usage
var message = new DapperOutbox.OutboxMessage { ... };
```

## Interpreting Results

### Ratio Column

Shows relative performance compared to baseline (ADO.NET):
- `1.00` = Baseline (ADO.NET)
- `1.59` = 59% slower than baseline
- `2.85` = 185% slower than baseline

### Rank Column

Ranks providers from fastest (1) to slowest (3) for each operation.

### Allocated Column

Shows heap allocations per operation:
- Lower is better
- Important for high-throughput scenarios
- Gen 0/1/2 collections impact latency

## Performance Optimization Tips

### ADO.NET

- Reuse `SqliteCommand` objects
- Use parameterized queries
- Avoid boxing/unboxing
- Use `Span<T>` for string operations

### Dapper

- Cache SQL strings (already done)
- Use `buffered: false` for large result sets
- Consider `QueryMultiple` for batches
- Use custom type handlers for complex types

### EF Core

- Disable change tracking for read-only queries: `.AsNoTracking()`
- Use compiled queries for repeated operations
- Batch inserts with `AddRange` + single `SaveChanges`
- Consider raw SQL for performance-critical paths

## Related Benchmarks

- `benchmarks/SimpleMediator.Benchmarks/Outbox/OutboxDapperBenchmarks.cs` - Dapper-specific
- `benchmarks/SimpleMediator.Benchmarks/Outbox/OutboxEfCoreBenchmarks.cs` - EF Core-specific
- `benchmarks/SimpleMediator.Benchmarks/Inbox/InboxDapperBenchmarks.cs` - Dapper-specific
- `benchmarks/SimpleMediator.Benchmarks/Inbox/InboxEfCoreBenchmarks.cs` - EF Core-specific

## Contributing

When adding new benchmarks:

1. Follow existing naming conventions
2. Use `[MemoryDiagnoser]` and `[RankColumn]` attributes
3. Add XML documentation explaining what's being tested
4. Clean up data in `[IterationSetup]` for consistency
5. Use realistic data sizes (not trivial, not massive)
6. Test all three providers (ADO, Dapper, EF Core)
