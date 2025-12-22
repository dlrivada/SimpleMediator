# Plan de Renombrado: SimpleMediator → Encina

**Fecha**: 2025-12-22
**Estado**: Borrador para revisión
**Ejecutor Principal**: Claude Code
**Supervisión**: Usuario

---

## Resumen Ejecutivo

Este documento detalla el plan completo para renombrar el proyecto de **SimpleMediator** a **Encina**.

### Alcance Total

| Categoría | Archivos Afectados | Ocurrencias Estimadas |
|-----------|-------------------|-----------------------|
| Código fuente (.cs) | ~880 | ~3,500+ |
| Proyectos (.csproj) | ~186 | ~550 |
| Soluciones (.sln*) | 7 | ~215 |
| Documentación (.md) | ~53 | ~1,100 |
| Workflows (.yml) | 8 | ~30 |
| Configuración (json/props) | ~15 | ~40 |
| **TOTAL** | **~1,150** | **~5,400+** |

---

## FASE 0: Preparación (Manual - Usuario)

### 0.1 Backup del Repositorio

Antes de comenzar, el usuario debe:

```bash
# Crear un backup completo
cd D:\Proyectos
xcopy /E /I SimpleMediator SimpleMediator.backup
```

### 0.2 Verificar Estado Limpio

```bash
cd D:\Proyectos\SimpleMediator
git status  # Debe mostrar "nothing to commit, working tree clean"
```

### 0.3 Crear Rama de Trabajo

```bash
git checkout -b feature/rename-to-encina
```

---

## FASE 1: Renombrar Contenido de Archivos (Claude Code)

Esta fase actualiza el CONTENIDO de los archivos sin renombrar archivos/carpetas.

### 1.1 Archivos de Código Fuente (.cs)

**Orden de ejecución**: Por lotes de ~50 archivos para evitar timeout.

#### 1.1.1 Namespaces

Reemplazar en todos los .cs:

- `namespace SimpleMediator` → `namespace Encina`
- `using SimpleMediator` → `using Encina`

**Patrones específicos** (51 variantes de namespace):

```
SimpleMediator → Encina
SimpleMediator.ADO.MySQL → Encina.ADO.MySQL
SimpleMediator.ADO.Oracle → Encina.ADO.Oracle
SimpleMediator.ADO.PostgreSQL → Encina.ADO.PostgreSQL
SimpleMediator.ADO.Sqlite → Encina.ADO.Sqlite
SimpleMediator.ADO.SqlServer → Encina.ADO.SqlServer
SimpleMediator.AmazonSQS → Encina.AmazonSQS
SimpleMediator.AspNetCore → Encina.AspNetCore
SimpleMediator.AzureServiceBus → Encina.AzureServiceBus
SimpleMediator.Caching → Encina.Caching
SimpleMediator.Caching.Dragonfly → Encina.Caching.Dragonfly
SimpleMediator.Caching.Garnet → Encina.Caching.Garnet
SimpleMediator.Caching.Hybrid → Encina.Caching.Hybrid
SimpleMediator.Caching.KeyDB → Encina.Caching.KeyDB
SimpleMediator.Caching.Memory → Encina.Caching.Memory
SimpleMediator.Caching.Redis → Encina.Caching.Redis
SimpleMediator.Caching.Valkey → Encina.Caching.Valkey
SimpleMediator.Dapper.MySQL → Encina.Dapper.MySQL
SimpleMediator.Dapper.Oracle → Encina.Dapper.Oracle
SimpleMediator.Dapper.PostgreSQL → Encina.Dapper.PostgreSQL
SimpleMediator.Dapper.Sqlite → Encina.Dapper.Sqlite
SimpleMediator.Dapper.SqlServer → Encina.Dapper.SqlServer
SimpleMediator.Dapr → Encina.Dapr
SimpleMediator.DataAnnotations → Encina.DataAnnotations
SimpleMediator.EntityFrameworkCore → Encina.EntityFrameworkCore
SimpleMediator.EventStoreDB → Encina.EventStoreDB
SimpleMediator.Extensions.Resilience → Encina.Extensions.Resilience
SimpleMediator.FluentValidation → Encina.FluentValidation
SimpleMediator.GraphQL → Encina.GraphQL
SimpleMediator.gRPC → Encina.gRPC
SimpleMediator.GuardClauses → Encina.GuardClauses
SimpleMediator.Hangfire → Encina.Hangfire
SimpleMediator.InMemory → Encina.InMemory
SimpleMediator.Kafka → Encina.Kafka
SimpleMediator.Marten → Encina.Marten
SimpleMediator.MassTransit → Encina.MassTransit
SimpleMediator.Messaging → Encina.Messaging
SimpleMediator.MiniValidator → Encina.MiniValidator
SimpleMediator.MongoDB → Encina.MongoDB
SimpleMediator.MQTT → Encina.MQTT
SimpleMediator.NATS → Encina.NATS
SimpleMediator.NServiceBus → Encina.NServiceBus
SimpleMediator.OpenTelemetry → Encina.OpenTelemetry
SimpleMediator.Polly → Encina.Polly
SimpleMediator.Quartz → Encina.Quartz
SimpleMediator.Redis.PubSub → Encina.Redis.PubSub
SimpleMediator.Refit → Encina.Refit
SimpleMediator.SignalR → Encina.SignalR
SimpleMediator.Wolverine → Encina.Wolverine
```

#### 1.1.2 Nombres de Clases

Clases principales a renombrar:

```csharp
// Core
SimpleMediator → Encina (clase principal en Core/SimpleMediator.cs)
SimpleMediatorConfiguration → EncinaConfiguration
SimpleMediatorException → EncinaException

// Extension Methods
AddSimpleMediator() → AddEncina()
UseSimpleMediatorContext() → UseEncinaContext()
AddSimpleMediatorOpenTelemetry() → AddEncinaOpenTelemetry()
AddSimpleMediatorCaching() → AddEncinaCaching()
AddSimpleMediatorDapperSqlServer() → AddEncinaDapperSqlServer()
AddSimpleMediatorDapperPostgreSQL() → AddEncinaDapperPostgreSQL()
AddSimpleMediatorDapperMySQL() → AddEncinaDapperMySQL()
AddSimpleMediatorDapperSqlite() → AddEncinaDapperSqlite()
AddSimpleMediatorDapperOracle() → AddEncinaDapperOracle()
AddSimpleMediatorAdoSqlServer() → AddEncinaAdoSqlServer()
AddSimpleMediatorAdoPostgreSQL() → AddEncinaAdoPostgreSQL()
AddSimpleMediatorAdoMySQL() → AddEncinaAdoMySQL()
AddSimpleMediatorAdoSqlite() → AddEncinaAdoSqlite()
AddSimpleMediatorAdoOracle() → AddEncinaAdoOracle()
AddSimpleMediatorEntityFrameworkCore() → AddEncinaEntityFrameworkCore()
AddSimpleMediatorFluentValidation() → AddEncinaFluentValidation()
AddSimpleMediatorDataAnnotations() → AddEncinaDataAnnotations()
AddSimpleMediatorMiniValidator() → AddEncinaMiniValidator()
AddSimpleMediatorGuardClauses() → AddEncinaGuardClauses()

// Options Classes (18 clases)
SimpleMediatorOpenTelemetryOptions → EncinaOpenTelemetryOptions
SimpleMediatorAspNetCoreOptions → EncinaAspNetCoreOptions
SimpleMediatorWolverineOptions → EncinaWolverineOptions
SimpleMediatorNServiceBusOptions → EncinaNServiceBusOptions
SimpleMediatorRabbitMQOptions → EncinaRabbitMQOptions
SimpleMediatorAzureServiceBusOptions → EncinaAzureServiceBusOptions
SimpleMediatorAmazonSQSOptions → EncinaAmazonSQSOptions
SimpleMediatorKafkaOptions → EncinaKafkaOptions
SimpleMediatorRedisPubSubOptions → EncinaRedisPubSubOptions
SimpleMediatorInMemoryOptions → EncinaInMemoryOptions
SimpleMediatorNATSOptions → EncinaNATSOptions
SimpleMediatorMQTTOptions → EncinaMQTTOptions
SimpleMediatorGrpcOptions → EncinaGrpcOptions
SimpleMediatorGraphQLOptions → EncinaGraphQLOptions
SimpleMediatorMongoDbOptions → EncinaMongoDbOptions
SimpleMediatorMartenOptions → EncinaMartenOptions
SimpleMediatorMassTransitOptions → EncinaMassTransitOptions
```

#### 1.1.3 InternalsVisibleTo

En el .csproj principal, actualizar:

```xml
<InternalsVisibleTo>SimpleMediator.Tests</InternalsVisibleTo>
→
<InternalsVisibleTo>Encina.Tests</InternalsVisibleTo>
```

#### 1.1.4 Strings y XML Doc Comments

Buscar y reemplazar en comentarios y strings:

- `"SimpleMediator"` → `"Encina"`
- `/// SimpleMediator` → `/// Encina`

### 1.2 Archivos de Proyecto (.csproj)

Para cada uno de los ~186 archivos .csproj:

```xml
<!-- PackageId -->
<PackageId>SimpleMediator.AspNetCore</PackageId>
→
<PackageId>Encina.AspNetCore</PackageId>

<!-- RootNamespace (si existe) -->
<RootNamespace>SimpleMediator.AspNetCore</RootNamespace>
→
<RootNamespace>Encina.AspNetCore</RootNamespace>

<!-- AssemblyName (si existe) -->
<AssemblyName>SimpleMediator.AspNetCore</AssemblyName>
→
<AssemblyName>Encina.AspNetCore</AssemblyName>

<!-- RepositoryUrl -->
<RepositoryUrl>https://github.com/dlrivada/SimpleMediator</RepositoryUrl>
→
<RepositoryUrl>https://github.com/dlrivada/Encina</RepositoryUrl>

<!-- ProjectReference -->
<ProjectReference Include="..\SimpleMediator\SimpleMediator.csproj" />
→
<ProjectReference Include="..\Encina\Encina.csproj" />

<!-- InternalsVisibleTo -->
<InternalsVisibleTo Include="SimpleMediator.Tests" />
→
<InternalsVisibleTo Include="Encina.Tests" />
```

### 1.3 Archivos de Solución

#### 1.3.1 SimpleMediator.slnx → Encina.slnx

Actualizar todas las rutas de proyecto dentro del archivo:

```
src\SimpleMediator\SimpleMediator.csproj → src\Encina\Encina.csproj
tests\SimpleMediator.Tests\SimpleMediator.Tests.csproj → tests\Encina.Tests\Encina.Tests.csproj
# ... ~113 ocurrencias
```

#### 1.3.2 Solution Filters (.slnf)

6 archivos a actualizar:

- SimpleMediator.Caching.slnf → Encina.Caching.slnf
- SimpleMediator.Core.slnf → Encina.Core.slnf
- SimpleMediator.Database.slnf → Encina.Database.slnf
- SimpleMediator.Scheduling.slnf → Encina.Scheduling.slnf
- SimpleMediator.Validation.slnf → Encina.Validation.slnf
- SimpleMediator.Web.slnf → Encina.Web.slnf

### 1.4 Documentación (.md)

#### 1.4.1 README.md (raíz)

~47 ocurrencias de "SimpleMediator":

- Título
- Badges URLs
- Ejemplos de código
- Referencias NuGet
- Links

#### 1.4.2 CLAUDE.md

Actualización completa - este es el archivo de instrucciones para IA:

- Todas las referencias a SimpleMediator
- Todos los ejemplos de código
- Nombres de paquetes

#### 1.4.3 ROADMAP.md

~43 ocurrencias - actualizar sección de renaming:

```markdown
**Current Name**: SimpleMediator → **New Name**: Encina
→
**Previous Name**: SimpleMediator → **Current Name**: Encina ✅
```

#### 1.4.4 CHANGELOG.md

~55 ocurrencias - añadir entrada de renaming:

```markdown
## [1.0.0] - 2025-XX-XX

### Changed
- **BREAKING**: Renamed library from SimpleMediator to Encina
- All namespaces changed from `SimpleMediator.*` to `Encina.*`
- All extension methods renamed from `AddSimpleMediator*` to `AddEncina*`
```

#### 1.4.5 Package READMEs (21 archivos en src/)

Cada paquete tiene su README con ejemplos de uso.

#### 1.4.6 DocFX Documentation

- docs/docfx.json: `_appName`, `_appTitle`, `_gitContribute.repo`
- docs/index.md: ~11 ocurrencias
- docs/docs/*.md: varios
- docs/architecture/**/*.md: varios

### 1.5 CI/CD y Workflows

#### 1.5.1 .github/workflows/ci.yml

```yaml
# Restore
run: dotnet restore SimpleMediator.slnx
→
run: dotnet restore Encina.slnx

# Build
run: dotnet build SimpleMediator.slnx
→
run: dotnet build Encina.slnx

# Test
run: dotnet test SimpleMediator.slnx
→
run: dotnet test Encina.slnx

# Pack
run: dotnet pack src/SimpleMediator/SimpleMediator.csproj
→
run: dotnet pack src/Encina/Encina.csproj

# Artifact name
name: simplemediator-nuget
→
name: encina-nuget
```

#### 1.5.2 .github/workflows/sonarcloud.yml

```yaml
sonarProjectKey: dlrivada_SimpleMediator
→
sonarProjectKey: dlrivada_Encina

sonarProjectName: SimpleMediator
→
sonarProjectName: Encina

dotnetBuildArguments: SimpleMediator.slnx
→
dotnetBuildArguments: Encina.slnx

dotnetTestArguments: SimpleMediator.slnx
→
dotnetTestArguments: Encina.slnx
```

**NOTA**: Requiere acción manual del usuario para crear nuevo proyecto en SonarCloud.

#### 1.5.3 Otros workflows

- benchmarks.yml
- codeql.yml
- docs.yml
- sbom.yml
- dotnet-ci.yml
- conventional-commits.yml

### 1.6 Archivos de Configuración

#### 1.6.1 nuget.config

```xml
<package pattern="SimpleMediator" />
→
<package pattern="Encina" />
```

#### 1.6.2 stryker-config.json

```json
"project": "src/SimpleMediator/SimpleMediator.csproj"
→
"project": "src/Encina/Encina.csproj"

"tests/SimpleMediator.Tests/SimpleMediator.Tests.csproj"
→
"tests/Encina.Tests/Encina.Tests.csproj"
```

#### 1.6.3 docker-compose.yml

```yaml
# Service names/labels containing simplemediator
simplemediator-db → encina-db
```

#### 1.6.4 observability/grafana/dashboards/simplemediator-overview.json

Renombrar archivo y actualizar contenido.

#### 1.6.5 tests/appsettings.Testing.json

Actualizar cualquier referencia.

---

## FASE 2: Renombrar Archivos y Carpetas (Claude Code)

**IMPORTANTE**: Esta fase debe ejecutarse DESPUÉS de la Fase 1.

### 2.1 Orden de Renombrado

El orden es crítico - renombrar de **más profundo a menos profundo**:

1. Archivos .cs dentro de carpetas
2. Archivos .csproj
3. Carpetas de proyectos
4. Archivos de solución
5. Carpeta raíz del proyecto (manual por usuario)

### 2.2 Archivos .cs con "SimpleMediator" en el nombre

```
src/SimpleMediator/Core/SimpleMediator.cs → src/Encina/Core/Encina.cs
src/SimpleMediator/Core/SimpleMediatorConfiguration.cs → src/Encina/Core/EncinaConfiguration.cs
src/SimpleMediator/Core/SimpleMediator.Stream.cs → src/Encina/Core/Encina.Stream.cs
src/SimpleMediator/Dispatchers/SimpleMediator.RequestDispatcher.cs → src/Encina/Dispatchers/Encina.RequestDispatcher.cs
src/SimpleMediator/Dispatchers/SimpleMediator.NotificationDispatcher.cs → src/Encina/Dispatchers/Encina.NotificationDispatcher.cs

# Options classes
src/SimpleMediator.OpenTelemetry/SimpleMediatorOpenTelemetryOptions.cs → src/Encina.OpenTelemetry/EncinaOpenTelemetryOptions.cs
# ... (18 archivos de Options)

# Test files
tests/SimpleMediator.Tests/SimpleMediatorTests.cs → tests/Encina.Tests/EncinaTests.cs
# ... etc
```

### 2.3 Archivos .csproj

Todos los archivos .csproj:

```
src/SimpleMediator/SimpleMediator.csproj → src/Encina/Encina.csproj
src/SimpleMediator.AspNetCore/SimpleMediator.AspNetCore.csproj → src/Encina.AspNetCore/Encina.AspNetCore.csproj
# ... ~186 archivos
```

### 2.4 Carpetas de Proyectos

#### Source (src/) - ~51 carpetas

```
src/SimpleMediator/ → src/Encina/
src/SimpleMediator.ADO.MySQL/ → src/Encina.ADO.MySQL/
src/SimpleMediator.ADO.Oracle/ → src/Encina.ADO.Oracle/
src/SimpleMediator.ADO.PostgreSQL/ → src/Encina.ADO.PostgreSQL/
src/SimpleMediator.ADO.Sqlite/ → src/Encina.ADO.Sqlite/
src/SimpleMediator.ADO.SqlServer/ → src/Encina.ADO.SqlServer/
src/SimpleMediator.AmazonSQS/ → src/Encina.AmazonSQS/
src/SimpleMediator.AspNetCore/ → src/Encina.AspNetCore/
src/SimpleMediator.AzureServiceBus/ → src/Encina.AzureServiceBus/
src/SimpleMediator.Caching/ → src/Encina.Caching/
src/SimpleMediator.Caching.Dragonfly/ → src/Encina.Caching.Dragonfly/
src/SimpleMediator.Caching.Garnet/ → src/Encina.Caching.Garnet/
src/SimpleMediator.Caching.Hybrid/ → src/Encina.Caching.Hybrid/
src/SimpleMediator.Caching.KeyDB/ → src/Encina.Caching.KeyDB/
src/SimpleMediator.Caching.Memory/ → src/Encina.Caching.Memory/
src/SimpleMediator.Caching.Redis/ → src/Encina.Caching.Redis/
src/SimpleMediator.Caching.Valkey/ → src/Encina.Caching.Valkey/
src/SimpleMediator.Dapper.MySQL/ → src/Encina.Dapper.MySQL/
src/SimpleMediator.Dapper.Oracle/ → src/Encina.Dapper.Oracle/
src/SimpleMediator.Dapper.PostgreSQL/ → src/Encina.Dapper.PostgreSQL/
src/SimpleMediator.Dapper.Sqlite/ → src/Encina.Dapper.Sqlite/
src/SimpleMediator.Dapper.SqlServer/ → src/Encina.Dapper.SqlServer/
src/SimpleMediator.Dapr/ → src/Encina.Dapr/
src/SimpleMediator.DataAnnotations/ → src/Encina.DataAnnotations/
src/SimpleMediator.EntityFrameworkCore/ → src/Encina.EntityFrameworkCore/
src/SimpleMediator.EventStoreDB/ → src/Encina.EventStoreDB/
src/SimpleMediator.Extensions.Resilience/ → src/Encina.Extensions.Resilience/
src/SimpleMediator.FluentValidation/ → src/Encina.FluentValidation/
src/SimpleMediator.GraphQL/ → src/Encina.GraphQL/
src/SimpleMediator.gRPC/ → src/Encina.gRPC/
src/SimpleMediator.GuardClauses/ → src/Encina.GuardClauses/
src/SimpleMediator.Hangfire/ → src/Encina.Hangfire/
src/SimpleMediator.InMemory/ → src/Encina.InMemory/
src/SimpleMediator.Kafka/ → src/Encina.Kafka/
src/SimpleMediator.Marten/ → src/Encina.Marten/
src/SimpleMediator.MassTransit/ → src/Encina.MassTransit/
src/SimpleMediator.Messaging/ → src/Encina.Messaging/
src/SimpleMediator.MiniValidator/ → src/Encina.MiniValidator/
src/SimpleMediator.MongoDB/ → src/Encina.MongoDB/
src/SimpleMediator.MQTT/ → src/Encina.MQTT/
src/SimpleMediator.NATS/ → src/Encina.NATS/
src/SimpleMediator.NServiceBus/ → src/Encina.NServiceBus/
src/SimpleMediator.OpenTelemetry/ → src/Encina.OpenTelemetry/
src/SimpleMediator.Polly/ → src/Encina.Polly/
src/SimpleMediator.Quartz/ → src/Encina.Quartz/
src/SimpleMediator.Redis.PubSub/ → src/Encina.Redis.PubSub/
src/SimpleMediator.Refit/ → src/Encina.Refit/
src/SimpleMediator.SignalR/ → src/Encina.SignalR/
src/SimpleMediator.Wolverine/ → src/Encina.Wolverine/
```

#### Tests (tests/) - ~130 carpetas

Incluye variantes: .Tests, .IntegrationTests, .ContractTests, .PropertyTests, .LoadTests, .GuardTests

```
tests/SimpleMediator.Tests/ → tests/Encina.Tests/
tests/SimpleMediator.ContractTests/ → tests/Encina.ContractTests/
tests/SimpleMediator.PropertyTests/ → tests/Encina.PropertyTests/
tests/SimpleMediator.GuardClauses.Tests/ → tests/Encina.GuardClauses.Tests/
tests/SimpleMediator.AspNetCore.Tests/ → tests/Encina.AspNetCore.Tests/
# ... ~130 carpetas
```

#### Benchmarks (benchmarks/) - 7 carpetas

```
benchmarks/SimpleMediator.Benchmarks/ → benchmarks/Encina.Benchmarks/
benchmarks/SimpleMediator.AspNetCore.Benchmarks/ → benchmarks/Encina.AspNetCore.Benchmarks/
benchmarks/SimpleMediator.Caching.Benchmarks/ → benchmarks/Encina.Caching.Benchmarks/
benchmarks/SimpleMediator.EntityFrameworkCore.Benchmarks/ → benchmarks/Encina.EntityFrameworkCore.Benchmarks/
benchmarks/SimpleMediator.Extensions.Resilience.Benchmarks/ → benchmarks/Encina.Extensions.Resilience.Benchmarks/
benchmarks/SimpleMediator.Polly.Benchmarks/ → benchmarks/Encina.Polly.Benchmarks/
benchmarks/SimpleMediator.Refit.Benchmarks/ → benchmarks/Encina.Refit.Benchmarks/
```

#### Load Tests (load/) - 2 carpetas

```
load/SimpleMediator.LoadTests/ → load/Encina.LoadTests/
load/SimpleMediator.NBomber/ → load/Encina.NBomber/
```

#### Backup de Paquetes Deprecados (.backup/)

```
.backup/deprecated-packages/SimpleMediator.Dapper/ → .backup/deprecated-packages/Encina.Dapper/
.backup/deprecated-packages/SimpleMediator.ADO/ → .backup/deprecated-packages/Encina.ADO/
```

### 2.5 Archivos de Solución

```
SimpleMediator.slnx → Encina.slnx
SimpleMediator.Caching.slnf → Encina.Caching.slnf
SimpleMediator.Core.slnf → Encina.Core.slnf
SimpleMediator.Database.slnf → Encina.Database.slnf
SimpleMediator.Scheduling.slnf → Encina.Scheduling.slnf
SimpleMediator.Validation.slnf → Encina.Validation.slnf
SimpleMediator.Web.slnf → Encina.Web.slnf
```

### 2.6 Otros Archivos

```
observability/grafana/dashboards/simplemediator-overview.json → observability/grafana/dashboards/encina-overview.json
```

---

## FASE 3: Verificación y Limpieza (Claude Code)

### 3.1 Limpiar Artefactos de Build

```bash
# Eliminar carpetas bin/obj para evitar conflictos
dotnet clean Encina.slnx
# O manualmente
Get-ChildItem -Recurse -Directory -Name "bin","obj" | Remove-Item -Recurse -Force
```

### 3.2 Verificar Build

```bash
dotnet restore Encina.slnx -maxcpucount:1
dotnet build Encina.slnx -maxcpucount:1 --configuration Release
```

### 3.3 Ejecutar Tests

```bash
dotnet test Encina.slnx -maxcpucount:1 --configuration Release
```

### 3.4 Búsqueda Final de Residuos

```bash
# Buscar cualquier referencia restante a "SimpleMediator"
grep -r "SimpleMediator" --include="*.cs" --include="*.csproj" --include="*.md" --include="*.yml" --include="*.json"
```

---

## FASE 4: Acciones Manuales del Usuario

### 4.1 Renombrar Carpeta Raíz del Proyecto

```bash
# Esto debe hacerlo el usuario FUERA del proyecto
cd D:\Proyectos
ren SimpleMediator Encina
```

### 4.2 Actualizar Git Remote (si se renombra repositorio)

```bash
cd D:\Proyectos\Encina
git remote set-url origin https://github.com/dlrivada/Encina.git
```

### 4.3 Renombrar Repositorio en GitHub

1. Ir a <https://github.com/dlrivada/SimpleMediator>
2. Settings → Repository name
3. Cambiar a "Encina"
4. Confirmar el cambio

**NOTA**: GitHub redirigirá automáticamente las URLs antiguas.

### 4.4 Actualizar SonarCloud

1. Ir a <https://sonarcloud.io/projects>
2. Crear nuevo proyecto "dlrivada_Encina"
3. Configurar el nuevo project key en el workflow
4. (Opcional) Archivar el proyecto antiguo

### 4.5 Registrar Nombres de Paquetes NuGet

**IMPORTANTE**: Antes de publicar, verificar disponibilidad de los nombres:

Paquetes a registrar en nuget.org:

- Encina
- Encina.AspNetCore
- Encina.Caching
- Encina.Caching.Memory
- Encina.Caching.Hybrid
- Encina.Caching.Redis
- (... ~51 paquetes)

### 4.6 Actualizar .claude/CLAUDE.md Path

El archivo `.claude/CLAUDE.md` seguirá funcionando, pero actualizar la ruta en cualquier referencia externa.

---

## FASE 5: Commit y Push (Claude Code)

### 5.1 Crear Commit

```bash
git add -A
git commit -m "$(cat <<'EOF'
feat: rename library from SimpleMediator to Encina

BREAKING CHANGE: Complete library rename

- All namespaces changed from `SimpleMediator.*` to `Encina.*`
- All extension methods renamed from `AddSimpleMediator*` to `AddEncina*`
- All class names with SimpleMediator prefix renamed to Encina prefix
- Solution files renamed
- Project folders renamed
- Documentation updated
- CI/CD workflows updated

Why "Encina"?
Spanish word for holm oak - symbolizing strength, resilience, and longevity.

EOF
)"
```

### 5.2 Push (solo después de revisión del usuario)

```bash
git push -u origin feature/rename-to-encina
```

---

## Cronograma de Ejecución

| Fase | Descripción | Ejecutor | Tiempo Estimado |
|------|-------------|----------|-----------------|
| 0 | Preparación | Usuario | 5 min |
| 1 | Actualizar contenido | Claude Code | 2-3 horas |
| 2 | Renombrar archivos/carpetas | Claude Code | 1-2 horas |
| 3 | Verificación | Claude Code | 30 min |
| 4 | Acciones manuales | Usuario | 30 min |
| 5 | Commit y push | Claude Code + Usuario | 10 min |

**Total estimado**: 4-6 horas de ejecución

---

## Riesgos y Mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
|--------|-------------|---------|------------|
| Referencias rotas en .csproj | Alta | Alto | Verificar build después de cada lote |
| Namespaces mal reemplazados | Media | Alto | Usar reemplazos exactos, no regex ambiguos |
| Archivos olvidados | Media | Medio | Búsqueda exhaustiva con grep al final |
| MSBuild crash | Alta | Medio | Usar `-maxcpucount:1` siempre |
| Git conflicts | Baja | Bajo | Trabajar en rama limpia |

---

## Checklist Final

### Pre-ejecución

- [ ] Backup creado
- [ ] Rama de trabajo creada
- [ ] Working tree limpio

### Post-ejecución

- [ ] Build compila sin errores
- [ ] Todos los tests pasan
- [ ] No quedan referencias a "SimpleMediator" (excepto histórico)
- [ ] Documentación actualizada
- [ ] Workflows actualizados
- [ ] Carpeta raíz renombrada
- [ ] Repositorio GitHub renombrado
- [ ] SonarCloud actualizado (si aplica)
- [ ] Commit creado con mensaje descriptivo

---

## Notas para el Usuario

1. **Este plan está diseñado para ser ejecutado en una sola sesión** - interrumpir a mitad puede dejar el proyecto en estado inconsistente.

2. **El renombrado de la carpeta raíz y el repositorio GitHub deben hacerse manualmente** - Claude Code no tiene permisos para estas operaciones.

3. **Los nombres de paquetes NuGet deben verificarse antes de publicar** - si algún nombre ya está tomado, necesitaremos ajustar.

4. **SonarCloud necesita un nuevo proyecto** - el project key incluye el nombre del repositorio.

5. **El backup es CRÍTICO** - si algo sale mal, podemos restaurar.

---

**¿Listo para revisar y aprobar este plan?**
