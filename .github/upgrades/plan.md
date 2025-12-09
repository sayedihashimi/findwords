# .NET 10 Upgrade Plan for FindWords

## Table of Contents
- [1. Executive Summary](#1-executive-summary)
- [2. Migration Strategy](#2-migration-strategy)
- [3. Detailed Dependency Analysis](#3-detailed-dependency-analysis)
- [4. Project-by-Project Plans](#4-project-by-project-plans)
  - [4.1 FindWords.Shared](#41-findwordsshared)
  - [4.2 FindWords.Web](#42-findwordsweb)
  - [4.3 FindWords.Console](#43-findwordsconsole)
  - [4.4 FindWords.Test](#44-findwordstest)
  - [4.5 FindWords.Benchmarks](#45-findwordsbenchmarks)
- [5. Package Update Reference](#5-package-update-reference)
- [6. Breaking Changes Catalog](#6-breaking-changes-catalog)
- [7. Testing & Validation Strategy](#7-testing--validation-strategy)
- [8. Risk Management](#8-risk-management)
- [9. Complexity & Effort Assessment](#9-complexity--effort-assessment)
- [10. Source Control Strategy](#10-source-control-strategy)
- [11. Success Criteria](#11-success-criteria)

---

## 1. Executive Summary

### Selected Strategy
**All-At-Once Strategy** - All projects upgraded simultaneously in single operation.

**Rationale**:
- 5 projects (small solution)
- All currently on .NET 8.0
- Clear, simple dependency structure (single shared library with consumers, one web app)
- No NuGet package incompatibilities; 100% compatibility in assessment
- Only 1 behavioral API change detected (ASP.NET Core `UseExceptionHandler(string)`)

**Scope**:
- Target framework upgrade to `.NET 10.0 (LTS)` for all projects
- Projects: `FindWords.Shared`, `FindWords.Web` (Blazor/ASP.NET Core), `FindWords.Console`, `FindWords.Test`, `FindWords.Benchmarks`

**Key Outcomes**:
- Unified upgrade (no intermediate states)
- Solution builds with 0 errors
- All tests pass

## 2. Migration Strategy

**Approach**: All-At-Once Strategy
- Upgrade all projects simultaneously to `net10.0`
- Update TargetFramework in each project and in any MSBuild imports (e.g., `Directory.Build.props/targets`, `Directory.Packages.props`) if present
- Update any package versions only if required by framework or noted as security vulnerabilities (assessment shows none and all packages are compatible)
- Perform a single coordinated build and error-fix pass
- Execute tests after atomic build succeeds

**Multi-targeting**: If any project is multi-targeted (assessment shows none), append `net10.0` while keeping existing targets.

**Constraints**:
- Ensure correct SDK installed and `global.json` (if present) aligned to SDK supporting .NET 10
- Respect project conditions in csproj/props/targets when changing TargetFramework

## 3. Detailed Dependency Analysis

**Graph Summary**:
- `FindWords.Shared` (library) is a core dependency used by `FindWords.Web`, `FindWords.Console`, `FindWords.Test`, `FindWords.Benchmarks`
- `FindWords.Web` is depended on by `FindWords.Test`
- `FindWords.Benchmarks` depends on `FindWords.Test` and `FindWords.Shared`

**Groupings (single atomic phase)**:
- Libraries: `FindWords.Shared`
- Applications: `FindWords.Web`, `FindWords.Console`
- Tests: `FindWords.Test`
- Tools: `FindWords.Benchmarks`

**Critical Path**:
- Shared → Web → Tests → Benchmarks (context only; upgrade occurs atomically)

## 4. Project-by-Project Plans

### 4.1 FindWords.Shared
**Current State**: net8.0, ClassLibrary, SDK-style, 16 files, 494 LOC, 4 dependants

**Target State**: net10.0

**Migration Steps**:
1. Update `TargetFramework` to `net10.0` in `src/FindWords.Shared/FindWords.Shared.csproj`
2. Validate any conditional compilation symbols remain accurate
3. No package updates required per assessment
4. Expected breaking changes: none flagged
5. Code modifications: address deprecations if surfaced during build
6. Testing: covered via `FindWords.Test`
7. Validation: builds with 0 errors/warnings

### 4.2 FindWords.Web
**Current State**: net8.0, AspNetCore (Blazor app), SDK-style, 32 files, 159 LOC, depends on `FindWords.Shared`

**Target State**: net10.0

**Migration Steps**:
1. Update `TargetFramework` to `net10.0` in `src/FindWords.Web/FindWords.Web.csproj`
2. Review Program/Startup: `UseExceptionHandler(string)` behavior change in ASP.NET Core. Prefer endpoint overloads or exception handler middleware configuration consistent with .NET 10 guidance.
3. Verify Blazor-specific settings (Interactive render mode, WebAssembly/Server) remain compatible with .NET 10
4. No package updates required per assessment; ensure `Microsoft.Azure.SignalR` remains compatible at runtime
5. Code modifications: adjust error handling configuration, DI setup, middleware order as needed
6. Testing: run component/unit tests via `FindWords.Test`; add a smoke test for exception handler behavior
7. Validation: builds successfully; app starts locally without runtime errors

### 4.3 FindWords.Console
**Current State**: net8.0, DotNetCoreApp, SDK-style, 1 file, depends on `FindWords.Shared`

**Target State**: net10.0

**Migration Steps**:
1. Update `TargetFramework` to `net10.0` in `src/FindWords.Console/FindWords.Console.csproj`
2. No package updates required
3. Code modifications: none expected
4. Testing: covered indirectly via library tests
5. Validation: builds with 0 errors

### 4.4 FindWords.Test
**Current State**: net8.0, DotNetCoreApp, SDK-style, depends on `FindWords.Web` and `FindWords.Shared`

**Target State**: net10.0

**Migration Steps**:
1. Update `TargetFramework` to `net10.0` in `src/FindWords.Test/FindWords.Test.csproj`
2. Keep `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`, and `coverlet.collector` as-is unless build indicates updates required for net10.0
3. Ensure test adapters are functioning under net10.0
4. Run all tests after atomic build completes
5. Validation: all tests pass

### 4.5 FindWords.Benchmarks
**Current State**: net8.0, DotNetCoreApp, SDK-style, depends on `FindWords.Test` and `FindWords.Shared`

**Target State**: net10.0

**Migration Steps**:
1. Update `TargetFramework` to `net10.0` in `src/FindWords.Benchmarks/FindWords.Benchmarks.csproj`
2. Keep `BenchmarkDotNet` as-is unless build/runtime indicates updates required
3. Validation: project builds and benchmarks execute locally

## 5. Package Update Reference

### Common Package Updates (affecting multiple projects)
None required by assessment; all packages marked compatible.

### Category-Specific Updates
- Test projects: `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`, `coverlet.collector` — retain current versions; upgrade only if net10 SDK tooling requires.
- Web: `Microsoft.Azure.SignalR` — retain current version; verify runtime compatibility with .NET 10.

### Project-Specific Exceptions
None.

## 6. Breaking Changes Catalog

### Framework
- General .NET 10 compilation/runtime changes — investigate during atomic build; fix surfaced errors.

### ASP.NET Core / Blazor
- `UseExceptionHandler(string)` behavioral change: adjust to recommended pattern (e.g., endpoint overload or centralized exception handling via middleware). Ensure Blazor error UI and exception handling still behaves as intended.

## 7. Testing & Validation Strategy

### Phase 1: Atomic Upgrade (build verification)
- Restore and build entire solution
- Address all compilation errors surfaced by framework upgrade
- Deliverable: solution builds with 0 errors

### Phase 2: Test Validation
- Run all tests in `FindWords.Test`
- Investigate/resolve failures
- Deliverable: all tests pass

### Additional Validation
- Blazor app smoke tests: start app, verify routing, DI, exception handling, and SignalR connectivity

## 8. Risk Management

- Risk level overall: Low
- Specific risks:
  - ASP.NET Core exception handling behavior change
  - Test adapters compatibility with net10
- Mitigation:
  - Follow updated ASP.NET Core guidance for exception handling
  - Update test packages if adapters fail under net10

## 9. Complexity & Effort Assessment

- All projects: Low complexity
- Shared library: Low
- Web (Blazor): Low to Medium due to runtime behavior change
- Tests: Low
- Console & Benchmarks: Low

## 10. Source Control Strategy

- Branching: use `upgrade-to-NET10`
- Commit strategy: Prefer single commit for atomic upgrade if possible (update TargetFrameworks and required fixes in one commit)
- PR: One PR from `upgrade-to-NET10` to `master` with comprehensive description referencing this plan

## 11. Success Criteria

- All projects target `net10.0`
- All package updates applied only as needed; no security vulnerabilities
- Solution builds with 0 errors
- All tests pass
- Blazor app runs without runtime issues

---

## Implementation Timeline

### Phase 0: Preparation
- Validate .NET 10 SDK is installed
- Update `global.json` if present to use .NET 10 SDK

### Phase 1: Atomic Upgrade
Operations (single coordinated batch):
- Update all project files to `net10.0`
- Validate MSBuild imports (`Directory.Build.props/targets`, `Directory.Packages.props`) if present
- Build solution and fix compilation errors
- Deliverable: Solution builds with 0 errors

### Phase 2: Test Validation
Operations:
- Execute all test projects
- Address test failures
- Deliverable: All tests pass
