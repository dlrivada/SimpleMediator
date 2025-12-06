# SimpleMediator Performance Testing Guide

## Objectives

- Quantify mediator send/publish overhead using BenchmarkDotNet.
- Surface allocation profiles and latency buckets for common request flows.

## Tooling Plan

- Add a dedicated BenchmarkDotNet project under `benchmarks/` targeting net10.0.
- Define scenarios for command, query, and notification pipelines with varying behavior counts.

## Execution

- Build benchmarks with `dotnet run -c Release --project benchmarks/SimpleMediator.Benchmarks.csproj`.
- Persist results under `artifacts/performance/<timestamp>/` for historical comparison.

## Follow-Up

- Add perf regression thresholds to CI once baseline data exists.
- Document tuning recommendations (DI scope reuse, behavior ordering) based on findings.
