---
applyTo: '**'
---

# Copilot Instructions

## 1. Language & Terminology
- **Technical Terms:** English.
- **Domain/Business Logic:** Full description in English (specifically in comments and documentation).
- **Tone:** Professional, clear, and concise.

## 2. Coding Standards
- Follow standard C# conventions.
- **Comments:** Strictly "Why" not "What." Do not explain syntax; explain business context and architectural decisions.
- **Self-Documenting Code:** Prioritize meaningful variable/method names over comments.

## 3. Tech Stack & Architecture
### Backend (C# .NET)
- **DI Container:** Microsoft.Extensions.DependencyInjection.
- **Data Access:** 
  - **MongoDB Connectors:** Specifically for MongoDB interactions.
  - **Repository Pattern:** Implemented for data access abstraction.
- **Validation:** FluentValidation.
- **Testing:** xUnit, Reqnroll.

### Frontend (Angular)
- **Core:** Angular + TypeScript.
- **State/Async:** RxJS (Prioritize observables over promises).

### Infrastructure
- **DB:** MongoDB.
- **Caching:** Redis.
