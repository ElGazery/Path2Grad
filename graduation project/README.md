# Path2Grad

A graduation project management system built with **.NET 8** following **Clean Architecture** principles. The system connects students, supervisors, teaching assistants, and administrators to manage the entire graduation project lifecycle.

## Architecture

The solution is divided into five projects following Clean Architecture:

```
Path2Grad.sln
├── Path2Grad.Domain        # Core domain entities & enums
├── Path2Grad.Application    # Application services, DTOs, interfaces
├── Path2Grad.Infrastructure # Data access (EF Core), repository implementations
├── Path2Grad.Api            # RESTful Web API (JWT auth, Swagger)
└── Path2Grad                # MVC Web application
```

## Tech Stack

- **.NET 8** — Target framework
- **Entity Framework Core 8** — ORM with SQL Server
- **JWT Bearer Authentication** — Security & authorization
- **Swagger / Swashbuckle** — API documentation
- **Clean Architecture** — Separation of concerns

## Features

- **Student Management** — Registration, profiles, project join requests
- **Supervisor (Doctor) Management** — Supervision assignments, project oversight
- **Teaching Assistant Management** — TA roles and assignments
- **Project Management** — Project bank, requirements, tasks, file uploads, team members
- **Internship Tracking** — Internship records, certificates, work files
- **CV Management** — Upload and manage CVs
- **Career Track Recommendations** — Survey-based career path suggestions
- **Chat Bot** — Conversational assistant for students
- **Authentication & Authorization** — JWT-based secure access

## Projects Overview

### Path2Grad.Domain
Domain entities (`Student`, `Supervisor`, `Project`, `Track`, `Internship`, etc.) and enums (`CareerTrack`).

### Path2Grad.Application
Business logic layer with service interfaces, DTOs, and helper utilities (`CareerTrackMatcher`, `SurveyResponseComparer`, `ImageHelper`).

### Path2Grad.Infrastructure
EF Core `ApplicationDbContext`, repository implementations, and dependency injection registration.

### Path2Grad.Api
REST API controllers exposing endpoints for all entities. Secured with JWT authentication and documented via Swagger.

### Path2Grad
MVC web application with its own controllers and Razor views for the frontend interface.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server instance

### Configuration

Update the connection string in `Path2Grad.Api/appsettings.json`:

```json
"ConnectionStrings": {
    "CS": "Server=your_server; Database=Path2Grad; User Id=your_user; Password=your_password; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;"
}
```

Configure JWT settings:

```json
"JWT": {
    "Key": "YourSuperLongSuperSecretKeyThatIsAtLeast32BytesLong12345",
    "Issuer": "your_issuer_url",
    "Audience": "your_audience_url"
}
```

### Run the API

```bash
cd Path2Grad.Api
dotnet run
```

Swagger UI will be available at `https://localhost:<port>/swagger`.

### Run the MVC app

```bash
cd Path2Grad
dotnet run
```

### Apply Migrations

```bash
dotnet ef database update --project Path2Grad.Infrastructure --startup-project Path2Grad.Api
```
