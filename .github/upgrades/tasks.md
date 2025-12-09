# FindWords .NET 10 Upgrade Tasks

## Overview

This document tracks the execution of the FindWords project upgrade from .NET 8.0 to .NET 10.0. All five projects (FindWords.Shared, FindWords.Web, FindWords.Console, FindWords.Test, FindWords.Benchmarks) will be upgraded simultaneously in a single atomic operation, followed by testing and validation.

**Progress**: 1/3 tasks complete (33%) ![0%](https://progress-bar.xyz/33)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2025-12-09 19:26)*
**References**: Plan §Phase 0

- [✓] (1) Verify .NET 10 SDK is installed
- [✓] (2) .NET 10 SDK meets minimum requirements (**Verify**)
- [✓] (3) Update global.json (if present) to specify .NET 10 SDK version
- [✓] (4) global.json configuration is compatible with .NET 10 SDK (**Verify**)

---

### [▶] TASK-002: Atomic framework and dependency upgrade
**References**: Plan §Phase 1, Plan §4 Project-by-Project Plans, Plan §5 Package Update Reference, Plan §6 Breaking Changes Catalog

- [✓] (1) Update TargetFramework to net10.0 in all project files per Plan §4.1-4.5: FindWords.Shared, FindWords.Web, FindWords.Console, FindWords.Test, FindWords.Benchmarks
- [✓] (2) All project files updated to net10.0 (**Verify**)
- [✓] (3) Validate and update any MSBuild imports (Directory.Build.props, Directory.Build.targets, Directory.Packages.props) if present to support net10.0
- [✓] (4) MSBuild imports validated and compatible with net10.0 (**Verify**)
- [ ] (5) Restore all dependencies for the solution
- [ ] (6) All dependencies restored successfully (**Verify**)
- [ ] (7) Review and update UseExceptionHandler configuration in FindWords.Web per Plan §4.2 and §6 (prefer endpoint overloads or exception handler middleware configuration consistent with .NET 10 guidance)
- [ ] (8) Build entire solution to identify compilation errors
- [ ] (9) Fix all compilation errors per Plan §6 Breaking Changes Catalog (address framework breaking changes, package API changes, configuration updates, and UseExceptionHandler behavior change)
- [ ] (10) Rebuild solution to verify all fixes applied
- [ ] (11) Solution builds with 0 errors (**Verify**)
- [ ] (12) Commit changes with message: "TASK-002: Atomic upgrade to .NET 10.0 for all projects"

---

### [ ] TASK-003: Run test suite and validate upgrade
**References**: Plan §Phase 2, Plan §4.4, Plan §7 Testing & Validation Strategy

- [ ] (1) Run all tests in FindWords.Test project
- [ ] (2) Fix any test failures per Plan §6 Breaking Changes Catalog (address test adapter compatibility issues if surfaced)
- [ ] (3) Re-run tests after fixes
- [ ] (4) All tests pass with 0 failures (**Verify**)
- [ ] (5) Commit test fixes with message: "TASK-003: Complete .NET 10.0 upgrade testing and validation"

---




