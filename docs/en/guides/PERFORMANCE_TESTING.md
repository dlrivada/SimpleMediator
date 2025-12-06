# SimpleMediator Performance Testing Guide

## Objectives

- Quantify mediator send/publish overhead using BenchmarkDotNet.
- Surface allocation profiles and latency buckets for common request flows.

## Tooling Plan

- Project scaffold: `benchmarks/SimpleMediator.Benchmarks` (BenchmarkDotNet, net10.0).
- Scenarios: command send with instrumentation, notification publish across multi-handlers.

## Execution

- Restore dependencies: `dotnet restore SimpleMediator.sln`.
- Run suites with `dotnet run -c Release --project benchmarks/SimpleMediator.Benchmarks/SimpleMediator.Benchmarks.csproj`.
- Persist results under `artifacts/performance/<timestamp>/` for historical comparison.

## Follow-Up

- Capture baseline numbers and attach CSV/markdown summaries alongside HTML results.
- Add perf regression thresholds to CI once baseline data exists.
- Document tuning recommendations (DI scope reuse, behavior ordering) based on findings.
