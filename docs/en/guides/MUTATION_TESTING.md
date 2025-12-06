# SimpleMediator Mutation Testing Guide

## Goals

- Establish Stryker.NET as the baseline mutation engine for the mediator library.
- Track actionable metrics (high 85, low 70, break 65) aligned with roadmap thresholds.

## Prerequisites

- .NET 10 SDK installed.
- Restore dependencies with `dotnet restore SimpleMediator.sln` prior to running Stryker.

## Running The Suite

- Execute `dotnet stryker` from the repository root; the `stryker-config.json` file supplies project and test references.
- Prefer Release builds to mirror CI behavior (`dotnet stryker --config-file stryker-config.json --solution SimpleMediator.sln`).

## Reporting

- HTML report: `artifacts/mutation/index.html` (configure via Stryker reporters).
- Console summary highlights surviving mutants; treat anything above the break threshold as a failure condition.

## Next Steps

- Integrate Stryker runs into CI once execution time is acceptable.
- Add targeted ignore patterns once hotspots are identified.
