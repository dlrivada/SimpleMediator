# SimpleMediator Testing Requirements Mapping

## Purpose

- Provide traceability between product requirements and automated verification layers.
- Highlight the suites and scenarios covering each mediator capability.

## Coverage Matrix

- Requests
  - Command handling lifecycle → unit tests (`SimpleMediator.Tests`)
  - Query behavior invariants → property tests (`ConfigurationProperties`)
  - Pipeline wiring contracts → contract tests (`PipelineBehaviorContracts`)
- Notifications
  - Publish ordering and cancellation behavior → property tests (`NotificationProperties`)
  - Multi-handler registration expectations → contract tests (`HandlerRegistrationContracts`)
- Configuration
  - Service registration lifetimes → contract tests (`ServiceRegistrationContracts`)
  - Assembly scanning cache behavior → unit tests (`SimpleMediator.Tests` pending refactor)
  - Custom configuration edge cases → contract tests (`ConfigurationEdgeCaseContracts`)

## Gaps & Planned Work

- Document mutation testing thresholds and actionable metrics (pending `MUTATION_TESTING.md`).
- Capture performance targets once BenchmarkDotNet suite lands.
- Link each roadmap item to a corresponding scenario identifier for quick lookup.

## Maintenance

- Review matrix at every milestone; update entries after major features or refactors.
- Keep references stable by pointing to test class names or doc sections rather than line numbers.
