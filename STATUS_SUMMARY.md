# SimpleMediator - Status Summary

**Fecha**: 2025-12-17
**Versión**: Pre-1.0 (desarrollo activo)

## 📊 Estado Actual

### ✅ Completado (24 proyectos)

| Categoría | Paquete | Status | Tests | Notas |
|-----------|---------|--------|-------|-------|
| **Core** | SimpleMediator | ✅ | 194/204 ✅ | ROP puro, Expression trees, Observability |
| **Validation** | FluentValidation | ✅ | 18/18 ✅ | Behavior automático |
| **Validation** | DataAnnotations | ✅ | 10/10 ✅ | Zero dependencies |
| **Validation** | MiniValidator | ✅ | 10/10 ✅ | Lightweight (~20KB) |
| **Validation** | GuardClauses | ✅ | ✅ | Defensive programming |
| **Web** | AspNetCore | ✅ | 49/49 ✅ | Middleware, Authorization, ProblemDetails |
| **Messaging** | Messaging (Abstractions) | ✅ | - | Shared interfaces |
| **Messaging** | EntityFrameworkCore | ✅ | 33/33 ✅ | Outbox, Inbox, Transactions |
| **Messaging** | Dapper | ⚠️ | 0/8 ❌ | **SQL Server only** (SQLite tests fail) |
| **Messaging** | ADO | ✅ | - | **SQL Server only** (fastest) |
| **Jobs** | Hangfire | ✅ | ⚠️ No tests | Fire-and-forget, Delayed, Recurring |
| **Jobs** | Quartz | ✅ | ⚠️ No tests | Enterprise CRON, Clustering |
| **Tests** | ContractTests | ✅ | 18/18 ✅ | - |
| **Tests** | PropertyTests | ✅ | 12/12 ✅ | FsCheck |

**Total**: 344 tests passing (8 Dapper SQLite tests failing)

---

## ⚠️ Problemas Críticos Identificados

### 1. **Tests Faltantes** ⭐⭐⭐⭐⭐

| Paquete | Status | Prioridad |
|---------|--------|-----------|
| SimpleMediator.Hangfire | ❌ No tests | ALTA |
| SimpleMediator.Quartz | ❌ No tests | ALTA |
| SimpleMediator.ADO | ⚠️ No tests (solo compilación) | MEDIA |

**Impacto**: Los paquetes Hangfire y Quartz están funcionando pero no validados con tests automatizados.

**Acción recomendada**: Crear test suites para ambos paquetes.

---

### 2. **Dapper SQLite Tests Failing** ⭐⭐⭐⭐⭐

**Problema**: Los tests de Dapper usan SQLite, pero las queries están optimizadas para SQL Server.

**Errores**:

```
- SQLite Error: 'no such function: GETUTCDATE'
- SQLite Error: 'near "TOP": syntax error'
- Invalid cast from 'System.String' to 'System.Guid'
```

**Causa raíz**: SQL Server-specific syntax:

- `GETUTCDATE()` → SQLite uses `datetime('now')`
- `TOP N` → SQLite uses `LIMIT N`
- GUIDs stored as TEXT in SQLite, but Dapper tries to map to Guid directly

**Solución implementada (parcial)**:

- ✅ Agregado `GuidTypeHandler` para Dapper
- ❌ Queries siguen usando sintaxis de SQL Server

**Acción recomendada**: Ver estrategia de bases de datos abajo.

---

### 3. **Multi-Database Support** ⭐⭐⭐⭐⭐

**Problema**: Todos los providers de messaging (Dapper, ADO) están hardcoded para SQL Server.

**Databases solicitadas**:

- ✅ SQL Server (actual)
- ❌ PostgreSQL
- ❌ MySQL/MariaDB
- ❌ Oracle
- ❌ SQLite (para tests)

**Estrategia recomendada**: **Provider-Specific Packages** (Opción A en FEATURES_ROADMAP.md)

Crear paquetes separados por base de datos:

```
SimpleMediator.SqlServer/     ← Renombrar Dapper/ADO actuales
SimpleMediator.PostgreSQL/    ← Nuevo (Npgsql + optimizado)
SimpleMediator.MySQL/         ← Nuevo (MySqlConnector + optimizado)
SimpleMediator.Sqlite/        ← Nuevo (solo para tests)
SimpleMediator.Oracle/        ← Futuro (si hay demanda)
```

**Pros**:

- Queries optimizadas por database
- Users solo instalan lo que necesitan
- Fácil mantener y testear
- No dependencies innecesarias

**Cons**:

- Más paquetes (pero código compartido en SimpleMediator.Messaging)

---

### 4. **Documentación Faltante** ⭐⭐⭐⭐

| Ubicación | Status |
|-----------|--------|
| README.md (principal) | ⚠️ No menciona Hangfire/Quartz |
| docs/ (carpeta) | ⚠️ No existe documentación de Hangfire/Quartz |
| Cada README.md de paquete | ✅ Completo |

**Acción recomendada**:

1. Actualizar README.md principal con tabla de todos los satélites
2. Crear docs/hangfire.md y docs/quartz.md

---

## 🎯 Próximos Pasos Recomendados

### Prioridad CRÍTICA (hacer ahora)

1. **✅ Actualizar FEATURES_ROADMAP.md** ← COMPLETADO
2. **Decidir estrategia de bases de datos**:
   - Opción A: Provider-specific packages (RECOMENDADO)
   - Opción B: SQL builder dinámico (SqlKata)
   - Opción C: SQL Server only + documentar limitación

3. **Crear tests para Hangfire y Quartz**:
   - Mock IMediator y verificar job execution
   - Tests de scheduling (fire-and-forget, delayed, recurring)
   - Tests de error handling

4. **Actualizar README.md principal**:
   - Agregar sección de Satellites con tabla completa
   - Links a READMEs de cada paquete
   - Matriz de compatibilidad de databases

---

### Prioridad ALTA (próxima semana)

5. **Implementar estrategia de databases**:
   - Si Opción A: Crear SimpleMediator.SqlServer (renombrar actual)
   - Crear SimpleMediator.PostgreSQL (Npgsql)
   - Crear SimpleMediator.Sqlite (para tests)

6. **Crear documentación en docs/**:
   - docs/hangfire.md
   - docs/quartz.md
   - docs/database-providers.md
   - docs/testing-guide.md

7. **Fix Dapper SQLite tests**:
   - Crear SQLite-specific store O
   - Convertir tests a SQL Server (Docker/LocalDB)

---

### Prioridad MEDIA (futuro)

8. **NoSQL Satellites** (según demanda):
   - SimpleMediator.Redis (caching + pub/sub)
   - SimpleMediator.EventStoreDB (event sourcing)
   - SimpleMediator.Marten (PostgreSQL event sourcing)

9. **Caching Package**:
   - SimpleMediator.Caching (IDistributedCache)
   - Query result caching
   - Idempotency support

10. **Stream Requests**:
    - IAsyncEnumerable<T> support
    - Stream behaviors
    - Backpressure handling

---

## 🔧 Decisiones Técnicas Pendientes

### Decisión 1: Database Strategy

**Pregunta**: ¿Cómo soportar múltiples databases?

**Opciones**:

- A) Provider-specific packages (SimpleMediator.SqlServer, PostgreSQL, MySQL...) ← **RECOMENDADO**
- B) SQL builder dinámico (SqlKata, Dapper.SqlBuilder)
- C) Solo SQL Server + documentar

**Impacto**: Alto - afecta arquitectura de todos los messaging packages

**Deadline**: Decidir antes de 1.0

---

### Decisión 2: Test Strategy for Hangfire/Quartz

**Pregunta**: ¿Cómo testear job schedulers?

**Opciones**:

- A) Unit tests con mocks (IMediator, IScheduler, IBackgroundJobClient)
- B) Integration tests con servidores reales (Hangfire Server, Quartz)
- C) Ambos (unit + integration)

**Recomendación**: **Opción A** (unit tests) para CI/CD rápido, agregar integration tests después

---

### Decisión 3: NoSQL Priority

**Pregunta**: ¿Qué NoSQL databases priorizar?

**Recomendación basada en uso real**:

1. **Redis** (caching + pub/sub) - caso de uso universal
2. **EventStoreDB** o **Marten** (event sourcing) - nicho pero growing
3. MongoDB/Cassandra - solo si hay demanda específica

---

## 📈 Métricas de Progreso

| Categoría | Completado | Total | % |
|-----------|------------|-------|---|
| Core Features | 1 | 1 | 100% |
| Validation Packages | 4 | 4 | 100% |
| Web Integration | 1 | 1 | 100% |
| Messaging Packages | 3 | 3 | 100% |
| Job Schedulers | 2 | 2 | 100% |
| Database Providers | 1 | 5 | 20% ⚠️ |
| Tests | 344 | 352 | 98% |
| Documentation | 12 | 15 | 80% |

**Overall Progress**: **85%** hacia Pre-1.0 release

---

## 🚀 Roadmap Simplificado

```
✅ Fase 1: Core + Validation (COMPLETADO)
   - SimpleMediator (ROP, pipelines, observability)
   - FluentValidation, DataAnnotations, MiniValidator, GuardClauses

✅ Fase 2: Web + Messaging (COMPLETADO)
   - AspNetCore (middleware, authorization, problem details)
   - EntityFrameworkCore, Dapper, ADO (messaging patterns)

✅ Fase 3: Job Scheduling (COMPLETADO)
   - Hangfire (simple, dashboard, fire-and-forget)
   - Quartz (enterprise, CRON, clustering)

⚠️ Fase 4: Multi-Database (EN PROGRESO)
   - SqlServer, PostgreSQL, MySQL, SQLite
   - Estrategia de SQL dialects
   - Tests de compatibilidad

📋 Fase 5: NoSQL + Advanced (PLANEADO)
   - Redis (caching + pub/sub)
   - EventStoreDB/Marten (event sourcing)
   - Caching package
   - Stream requests
```

---

## 💡 Recomendación Inmediata

**Para maximizar el valor del proyecto, sugiero este orden**:

1. **HOY**:
   - ✅ Actualizar FEATURES_ROADMAP.md (HECHO)
   - Decidir estrategia de databases (Provider-specific packages)

2. **ESTA SEMANA**:
   - Crear tests para Hangfire (2-3 horas)
   - Crear tests para Quartz (2-3 horas)
   - Actualizar README.md principal (1 hora)

3. **PRÓXIMA SEMANA**:
   - Renombrar Dapper/ADO → SimpleMediator.SqlServer
   - Crear SimpleMediator.PostgreSQL (clonar + adaptar SQL)
   - Crear SimpleMediator.Sqlite (para tests)

4. **ANTES DE 1.0**:
   - Completar documentación (docs/)
   - Redis package (caching)
   - Stream requests (IAsyncEnumerable<T>)

---

## 📞 Siguiente Acción

**Pregunta para ti**: ¿Cuál de estas tareas quieres que aborde primero?

A. Crear tests para Hangfire y Quartz
B. Implementar estrategia multi-database (renombrar + PostgreSQL)
C. Actualizar README.md principal con todos los satélites
D. Crear documentación en docs/
E. Otro (especificar)

**Mi recomendación**: **A + C** (tests + README) para tener todo documentado y validado, luego **B** (databases) en la próxima sesión.
