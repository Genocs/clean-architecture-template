---
applyTo: '**'
---

# Copilot Instructions

## 1. Coding Standards
- Follow standard C# conventions.
- **Comments:** Strictly "Why" not "What." Do not explain syntax; explain business context and architectural decisions.
- **Self-Documenting Code:** Prioritize meaningful variable/method names over comments.

## 2. Tech Stack & Architecture
### Backend (C# .NET)
- **DI Container:** Microsoft.Extensions.DependencyInjection.
- **Data Access:** 
  - **MongoDB Connectors:** Specifically for MongoDB interactions.
  - **Repository Pattern:** Implemented for data access abstraction.
- **Validation:** FluentValidation.
- **Testing:** xUnit, Reqnroll.

### Infrastructure
- **DB:** MongoDB or SQL Server (depending on the use case).
- **Caching:** Redis.
