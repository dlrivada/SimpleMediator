# SimpleMediator Quality & Testing Roadmap

Last updated: 2025-12-06

## Overview

This roadmap tracks the effort to bring SimpleMediator to the same multi-layered testing maturity as the vacations pilot. It captures scope, current status, and next actions so the work can advance incrementally without losing context.

## Objectives

- Establish comprehensive automated coverage across unit, property-based, contract, integration, mutation, and performance suites.
- Provide reproducible tooling (coverage, benchmarks, mutation) with documented execution steps and quality gates.
- Maintain a live log of progress so each iteration updates status, outcomes, and upcoming work.

## Workstreams & Status

| Area | Scope | Owner | Status | Notes |
|------|-------|-------|--------|-------|
| Foundation | Relocate imported reference docs, update `.gitignore`, create roadmap doc | Copilot | ✅ Done | Zip moved to `.backup/`, roadmap created |
| Coverage Baseline | Collect current `dotnet test` coverage report and archive results | Copilot | ✅ Done | Release run on 2025-12-06 — 89.1% line / 68.4% branch (see `artifacts/coverage/latest`) |
| Unit Expansion | Increase coverage for mediator core, behaviors, metrics | Copilot | ⏳ Planned | Target ≥90% lines for `SimpleMediator` namespace |
| Property Tests | Create `SimpleMediator.PropertyTests` with FsCheck generators | Copilot | ⏳ Planned | Focus on pipeline ordering, handler determinism |
| Contract Tests | Ensure handlers/behaviors honor interfaces across implementations | Copilot | ⏳ Planned | Mirror LSP checks from vacations pilot |
| Mutation Testing | Configure Stryker.NET thresholds and CI integration | Copilot | ⏳ Planned | Thresholds: high 85, low 70, break 65 |
| Performance Benchmarks | Add BenchmarkDotNet project & publish results doc | Copilot | ⏳ Planned | Measure send/publish overhead & allocations |
| Load Harness | Prototype NBomber (or console) throughput tests | Copilot | ⏳ Planned | Document CPU/memory requirements |
| Documentation | Publish guides (`docs/en/guides`) & requirements mapping | Copilot | ⏳ Planned | Align with vacations project style |

Status legend: ✅ Done · 🚧 In progress · ⏳ Planned · ⚠️ Blocked

## Progress Log

- **2025-12-06** — Imported reference documentation moved to `.backup/`; roadmap established.
- **2025-12-06** — Captured Release coverage baseline (lines 89.1%, branches 68.4%); reports stored in `artifacts/coverage/latest`.

## Upcoming Actions

1. Scaffold property test project (`SimpleMediator.PropertyTests`) with initial generators and pipeline invariants.
2. Draft contract test structure ensuring behaviors/decorators remain transparent.
3. Document execution commands in a new `docs/en/guides/TESTING.md` section tailored to SimpleMediator.

---

_Keep this roadmap updated at each milestone: add log entries, adjust statuses, and expand scope as needed._
