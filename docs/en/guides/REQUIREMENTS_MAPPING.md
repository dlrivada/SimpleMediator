# SimpleMediator Testing Requirements Mapping

## Purpose

- Provide traceability between product requirements and automated verification layers.
- Highlight the suites and scenarios covering each mediator capability.

## Coverage Matrix

- Requests
  - `REQ-REQ-LIFECYCLE`: Command handling lifecycle → unit tests (`SimpleMediator.Tests`)
  - `REQ-REQ-QUERY`: Query behavior invariants → property tests (`ConfigurationProperties`)
  - `REQ-REQ-PIPELINE`: Pipeline wiring contracts → contract tests (`PipelineBehaviorContracts`)
- Notifications
  - `REQ-NOT-ORDER`: Publish ordering and cancellation behavior → property tests (`NotificationProperties`)
  - `REQ-NOT-MULTI`: Multi-handler registration expectations → contract tests (`HandlerRegistrationContracts`)
- Configuration
  - `REQ-CONF-LIFETIME`: Service registration lifetimes → contract tests (`ServiceRegistrationContracts`)
  - `REQ-CONF-SCAN`: Assembly scanning cache behavior → unit tests (`SimpleMediator.Tests` pending refactor)
  - `REQ-CONF-EDGE`: Custom configuration edge cases → contract tests (`ConfigurationEdgeCaseContracts`)

## Gaps & Planned Work

- Document mutation testing thresholds and actionable metrics (pending `MUTATION_TESTING.md`).
- Capture performance targets once BenchmarkDotNet suite lands.
- Link each roadmap item to a corresponding scenario identifier for quick lookup.

## Maintenance

- Review matrix at every milestone; update entries after major features or refactors.
- Keep references stable by pointing to test class names or doc sections rather than line numbers.
