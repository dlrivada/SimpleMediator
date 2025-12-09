# Analyzer Triage – December 2025

Date: 2025-12-09

## Snapshot Summary

Latest `dotnet build` (with analyzers enabled) produces 239 warnings across the solution. The table below captures the most frequent diagnostics before applying scoped suppressions for test/benchmark code.

| Diagnostic | Count | Scope (Primary) | Notes |
| --- | --- | --- | --- |
| CA1707 | 360 | `tests`, `benchmarks` | Test and benchmark method names use underscores by design. Scoped suppression added in `.editorconfig` for those folders. |
| CA1848 | 30 | `src/SimpleMediator/SimpleMediator.cs` | Suggests switching to `LoggerMessage` delegates; evaluate perf trade-offs vs readability before adopting. |
| CA1859 | 14 | `tests`, `PropertyTests` | Recommends returning concrete collections; low impact, consider refactoring helpers. |
| CA1822 | 14 | `tests` | Static candidates inside inner test types; can be fixed opportunistically. |
| CA1861 | 10 | `tests/SimpleMediatorTests.cs` | Favors cached static arrays in theory data; low priority. |
| CA1716 | 10 | `src/SimpleMediator` | Naming conflicts with language keywords (`Error`, `error`, `next`); requires API review before change. |
| CA2254 | 8 | `src/SimpleMediator/SimpleMediator.cs` | Requires constant logging templates; will be addressed alongside CA1848. |
| CA1806 | 8 | `src/SimpleMediator/Behaviors` | `Match` return value ignored; verify purity and adjust pattern matching. |

## Decisions & Next Actions

1. **Noise reduction** – Disabled CA1707 for `tests/**/*.cs` and `benchmarks/**/*.cs` via `.editorconfig` to keep the analyzer backlog focused on product code.
2. **High-value targets** – Prioritize CA1848/CA2254 combo (logging structure), CA1068 (CancellationToken position) and CA1510/CA1860 (easy code health wins) for the runtime library.
3. **Compatibility review** – CA1711/CA1716 impacts public surface (`Error` type, pipeline parameter names). Schedule API discussion before any renaming or consider explicit suppressions with justification.
4. **Automation** – Keep warnings surfaced as suggestions during cleanup. Re-enable `TreatWarningsAsErrors` once remaining CA diagnostics are either fixed or formally suppressed.

## Follow-Up Checklist

- [ ] Draft RFC for logging refactor (LoggerMessage vs current ILogger usage).
- [ ] Audit pipeline interfaces to confirm options for CA1068 without breaking consumers (e.g., overloads, analyzer suppression).
- [ ] Convert the handful of CA1510/CA1860/CA2249 cases in `src/SimpleMediator`.
- [ ] Evaluate bulk fixes for CA1859/CA1822 in test utilities when convenient.
- [ ] Update `QUALITY_SECURITY_ROADMAP.md` once remediation milestones are locked in.
