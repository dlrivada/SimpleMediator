# ADR-001: Railway Oriented Programming for Error Handling

**Status:** Accepted
**Date:** 2025-12-12
**Deciders:** Architecture Team
**Technical Story:** Establish consistent error handling strategy for mediator pipeline

## Context

The mediator needs a robust, type-safe error handling strategy that:

- Avoids throwing exceptions for expected failures (validation errors, business rule violations, not found, etc.)
- Maintains type safety throughout the pipeline
- Enables composable error handling in behaviors and processors
- Provides clear distinction between technical errors (bugs) and functional failures (expected scenarios)
- Supports functional programming patterns

Traditional exception-based error handling has several drawbacks:

- Exceptions are invisible in method signatures (no compile-time verification)
- Performance overhead for control flow
- Makes composition and chaining difficult
- Unclear whether a failure is expected or unexpected
- Difficult to unit test error paths

## Decision

We adopt **Railway Oriented Programming (ROP)** using `Either<MediatorError, TResponse>` from LanguageExt as the return type for all mediator operations.

### Key Principles

1. **Two Tracks:** Success (Right) and Failure (Left)
2. **Explicit Errors:** All potential failures are visible in the type signature
3. **Short-Circuiting:** First Left value stops pipeline execution
4. **Exception Safety Net:** Unexpected exceptions are caught at dispatcher level and converted to Left

### Implementation

```csharp
// Public API
ValueTask<Either<MediatorError, TResponse>> Send<TResponse>(IRequest<TResponse> request, ...);
ValueTask<Either<MediatorError, Unit>> Publish<TNotification>(TNotification notification, ...);

// Pipeline behaviors
ValueTask<Either<MediatorError, TResponse>> Handle(TRequest request, RequestHandlerCallback<TResponse> next, ...);

// Handlers return plain TResponse
Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
```

### Error Flow

```
Request → [Validation Guards] → [Pre-Processors] → [Behaviors] → [Handler] → [Post-Processors] → Response
            ↓ Left                ↓ Left            ↓ Left        ↓ Left        ↓ Left              ↓ Right
         MediatorError        MediatorError     MediatorError  MediatorError  MediatorError      TResponse
```

Any Left value short-circuits the pipeline and returns immediately.

### MediatorError Structure

```csharp
public sealed class MediatorError
{
    public string Code { get; }           // e.g., "mediator.handler.missing"
    public string Message { get; }         // Human-readable description
    public Option<Exception> Exception { get; } // Original exception if any
    public IReadOnlyDictionary<string, object?> Details { get; } // Context metadata
}
```

## Consequences

### Positive

- **Type Safety:** Errors are part of the type system, enforced at compile time
- **Explicit Flow:** Clear distinction between success and failure paths
- **Composability:** Easy to chain and compose operations
- **Performance:** No exception throwing for expected failures
- **Testability:** Easy to test both success and failure scenarios
- **Observability:** Error codes and metadata enable better monitoring
- **Functional Style:** Aligns with functional programming principles

### Negative

- **Learning Curve:** Developers unfamiliar with functional patterns need to learn Either/Left/Right
- **Verbosity:** Slightly more verbose than try-catch blocks
- **Mixed Paradigm:** Handlers still return `Task<TResponse>` (not Either) to keep handler implementation simple
- **Library Dependency:** Depends on LanguageExt for Either type

### Neutral

- **Exception Handling:** Unexpected exceptions are still caught and converted to Either at the dispatcher boundary
- **Interoperability:** External code can still throw exceptions; they're converted to MediatorError

## Alternatives Considered

### 1. Traditional Exceptions

```csharp
Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
```

**Rejected:** Exceptions are invisible, expensive, and make composition difficult.

### 2. Result<T, E> Pattern (Custom Type)

```csharp
ValueTask<Result<TResponse, MediatorError>> Send<TResponse>(...)
```

**Rejected:** Reinventing the wheel. LanguageExt's Either is well-tested and feature-rich.

### 3. Discriminated Unions (C# 10+)

```csharp
ValueTask<OneOf<TResponse, MediatorError>> Send<TResponse>(...)
```

**Rejected:** OneOf lacks the functional combinators that Either provides (Map, Bind, Match, etc.).

### 4. Nullable Reference Types

```csharp
ValueTask<TResponse?> Send<TResponse>(...)
```

**Rejected:** Cannot distinguish between different error types. No error context.

## Examples

### Success Path

```csharp
var result = await mediator.Send(new GetUserQuery(userId));
result.Match(
    Right: user => Console.WriteLine($"Found: {user.Name}"),
    Left: error => Console.WriteLine($"Error: {error.Message}")
);
```

### Failure Path

```csharp
var result = await mediator.Send(new DeleteUserCommand(invalidId));
// result is Left<MediatorError>("mediator.handler.missing")
```

### Behavior Composition

```csharp
public async ValueTask<Either<MediatorError, TResponse>> Handle(...)
{
    // Can short-circuit with validation errors
    if (!IsValid(request))
        return Left(MediatorError.New("validation.failed"));

    // Continue pipeline
    var response = await next();

    // Can inspect and transform the result
    return response.Map(r => Transform(r));
}
```

## Related Decisions

- ADR-002: Dependency Injection Strategy (affects how errors from DI are handled)
- ADR-003: Caching Strategy (cached delegates still return Either)

## References

- [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/) by Scott Wlaschin
- [LanguageExt Either Documentation](https://github.com/louthy/language-ext/wiki/Either)
- [Functional C# with Language-Ext](https://github.com/louthy/language-ext)

## Notes

This decision was made early in the project and has proven robust. The functional approach with Either provides excellent composability and makes error handling explicit and testable. The learning curve is acceptable given the benefits.

Future consideration: Introduce `MediatorResult<T>` as a wrapper over `Either<MediatorError, T>` with convenience methods for better discoverability, while maintaining Either internally for composition.
