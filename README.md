# HRPayroll 🏢

A **multi-tenant SaaS HR & Payroll Management System** built with .NET 9 and Clean Architecture. Designed to handle employee management, attendance, leave tracking, and payroll processing for organizations of any size.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Modules](#modules)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Database Schema](#database-schema)
- [Authentication & Authorization](#authentication--authorization)
- [Roadmap](#roadmap)

---

## Overview

HRPayroll is a generic, market-agnostic HR platform that supports multiple companies (tenants) on a single deployment. Each company can manage its own employees, departments, branches, leave policies, salary structures, and monthly payroll runs — all in complete isolation from other tenants.

**Key highlights:**
- Multi-tenant architecture with full data isolation per company
- Flexible RBAC with claims-based authorization
- Generic payroll engine supporting configurable salary components
- Clean Architecture enforcing strict separation of concerns
- Built to be deployed live with a full Angular frontend

---

## Architecture

The solution follows **Clean Architecture** with strict dependency rules:

```
Domain  ←  Application  ←  Infrastructure  ←  API
```

| Layer | Project | Responsibility |
|---|---|---|
| Domain | `HRPayroll.Domain` | Entities, enums, domain exceptions, business invariants |
| Application | `HRPayroll.Application` | CQRS handlers, interfaces, validation, use cases |
| Infrastructure | `HRPayroll.Infrastructure` | EF Core, repositories, identity, email, file storage |
| API | `HRPayroll.API` | Controllers, middleware, JWT, dependency wiring |

**Key patterns used:**
- CQRS + MediatR for request handling
- Repository + Unit of Work for data access
- Result Pattern instead of throwing exceptions across layers
- Specification Pattern for reusable query logic
- Claims-Based Authorization with policy evaluation
- Soft Delete across all entities via `AuditableEntity`

---

## Tech Stack

| Area | Technology |
|---|---|
| Runtime | .NET 9 |
| Language | C# 13 |
| Database | SQL Server |
| ORM | Entity Framework Core 9 (Code First) |
| Mediator | MediatR |
| Validation | FluentValidation |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Authorization | Policy-Based (Claims-Based ABAC) |
| Containerization | Docker |
| CI/CD | GitHub Actions |
| Deployment | Azure (Free Tier) |
| Frontend | Angular (planned) |

---

## Modules

### 👤 Identity & Access
- Company onboarding
- User registration and login (JWT)
- Token refresh and password reset
- Dynamic role management with fine-grained permissions
- Claims-based authorization (ABAC)

### 🏢 Organization
- Multi-branch company structure
- Department management with manager assignment
- Job titles linked to salary grades
- Grade-based min/max salary ranges

### 👥 Employee Management
- Full employee profiles (job info + personal info separated)
- Document management (contracts, IDs, certificates)
- Employment history tracking (hires, promotions, transfers, terminations)
- Emergency contact records

### 📅 Attendance & Leave
- Configurable leave types (Annual, Sick, Unpaid, etc.)
- Time-aware leave policies per grade/department
- Leave balance tracking with carry-forward support
- Leave request workflow (submit → approve/reject)
- Attendance records with check-in/check-out and total hours
- Public holiday awareness for accurate day calculations

### 💰 Payroll
- Configurable salary components (Fixed or % of Basic)
- Reusable salary structures assigned per employee
- One-time bonuses and deductions per month
- Monthly payroll run lifecycle (Draft → Approved → Finalized)
- Detailed payslip generation per employee
- Payroll approval audit trail (who approved, when)

---

## Project Structure

```
HRPayroll.sln
│
├── src/
│   ├── HRPayroll.Domain/
│   │   ├── Common/                    # AuditableEntity, DomainException
│   │   ├── Enums/                     # All domain enums
│   │   ├── Entities/
│   │   │   ├── Identity/              # Company, User, Role, Permission
│   │   │   ├── Organization/          # Branch, Grade, JobTitle, Department
│   │   │   ├── Employee/              # Employee, PersonalInfo, Documents, History
│   │   │   ├── Attendance/            # LeaveType, LeaveRequest, AttendanceRecord
│   │   │   └── Payroll/               # SalaryStructure, PayrollRun, Payslip
│   │   └── Events/                    # Domain events
│   │
│   ├── HRPayroll.Application/
│   │   ├── Common/
│   │   │   ├── Behaviors/             # MediatR pipeline (validation, logging)
│   │   │   ├── Interfaces/            # IRepository, IUnitOfWork, IEmailService
│   │   │   └── Models/                # Result<T>, PagedList<T>
│   │   └── Features/                  # CQRS — one folder per feature
│   │       ├── Auth/
│   │       ├── Employees/
│   │       ├── Departments/
│   │       ├── Leaves/
│   │       └── Payroll/
│   │
│   ├── HRPayroll.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── Configurations/        # EF Core Fluent API configs
│   │   │   ├── Migrations/
│   │   │   ├── Repositories/
│   │   │   └── AppDbContext.cs
│   │   ├── Identity/                  # JWT token service, password hashing
│   │   └── Services/                  # Email, file storage
│   │
│   └── HRPayroll.API/
│       ├── Controllers/
│       ├── Middleware/                # Global exception handling
│       └── Program.cs
│
└── tests/
    ├── HRPayroll.Domain.Tests/
    ├── HRPayroll.Application.Tests/
    └── HRPayroll.API.Tests/
```

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Docker](https://www.docker.com/) (optional)

### Setup

1. **Clone the repository**
```bash
git clone https://github.com/your-username/HRPayroll.git
cd HRPayroll
```

2. **Configure the connection string**

Update `appsettings.Development.json` in `HRPayroll.API`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HRPayrollDb;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "HRPayroll",
    "Audience": "HRPayroll",
    "ExpiryMinutes": 60
  }
}
```

3. **Apply migrations**
```bash
cd src/HRPayroll.API
dotnet ef database update
```

4. **Run the API**
```bash
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger at `https://localhost:5001/swagger`.

---

## Database Schema

The full database schema is designed in [dbdiagram.io](https://dbdiagram.io) and covers 22+ tables across 5 modules with:

- UUID v7 primary keys (time-sortable, index-friendly)
- Soft delete on all tables via `is_deleted`
- Full audit trail (`created_at`, `created_by`, `modified_at`, `modified_by`)
- Unique constraints to prevent duplicate records
- Circular reference handling between `departments` and `employees`

---

## Authentication & Authorization

### Authentication
JWT Bearer tokens issued on login, with refresh token support. Passwords hashed using ASP.NET Core Identity's `PasswordHasher`.

### Authorization
The system uses **Attribute-Based Access Control (ABAC)** built on top of ASP.NET Core's policy-based authorization:

- **Roles** are stored in the database and are configurable per company
- **Permissions** are granular actions (e.g. `leaves:approve`, `payroll:run`)
- At login, all permissions are flattened into **JWT claims**
- Controllers use `[Authorize(Policy = "permission:name")]` for enforcement
- Resource-based checks (e.g. "same company only") use custom `IAuthorizationHandler`

**Default system roles:**

| Role | Key Permissions |
|---|---|
| `SuperAdmin` | Full access including company management |
| `HRManager` | Full HR and payroll access |
| `HROfficer` | Employee view, leave and attendance management |
| `Employee` | Own profile, leave requests, own payslips |

---

## Roadmap

- [x] Domain layer — entities, enums, base classes
- [ ] Application layer — CQRS handlers, interfaces, validation
- [ ] Infrastructure layer — EF Core, repositories, JWT service
- [ ] API layer — controllers, middleware, Swagger
- [ ] Seeding — default roles, permissions, super admin
- [ ] Angular frontend — admin dashboard
- [ ] Docker setup
- [ ] GitHub Actions CI/CD pipeline
- [ ] Azure deployment

---

## License

This project is built as a portfolio piece and is open for reference and learning purposes.
