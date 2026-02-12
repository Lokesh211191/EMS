# Employee Management System

## Overview
A comprehensive .NET Core 8 MVC application for managing employee records with RESTful API support.

## Architecture
- **Layered Architecture**: Core, Application, Infrastructure, API, Web, and Tests layers
- **Design Patterns**: Repository Pattern, Unit of Work, Dependency Injection
- **Database**: Entity Framework Core with SQL Server

## Features
- ✅ CRUD operations for employees
- ✅ Search and filter employees by department and role
- ✅ Dashboard with employee statistics
- ✅ RESTful API endpoints
- ✅ Responsive Bootstrap UI
- ✅ Comprehensive error handling
- ✅ Unit testing

## Project Structure
```
EmployeeManagementSystem/
├── Core/              # Domain entities and interfaces
├── Application/       # Business logic and services
├── Infrastructure/    # Data access and EF Core
├── Api/              # REST API controllers
├── Web/              # MVC UI controllers and views
└── Tests/            # Unit tests
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (or configure your own database)
- Visual Studio Code or Visual Studio

### Database Setup
Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Your connection string here"
}
```

### Run Migrations
```bash
dotnet ef database update --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api
```

### Running the Application

**Web Application:**
```bash
cd EmployeeManagementSystem.Web
dotnet run
```
Navigate to `https://localhost:5001`

**API:**
```bash
cd EmployeeManagementSystem.Api
dotnet run
```
API runs on `https://localhost:5000`

### Running Tests
```bash
cd EmployeeManagementSystem.Tests
dotnet test
```

## API Endpoints

### Employees
- `GET /api/employees` - Get all employees
- `GET /api/employees/{id}` - Get employee by ID
- `GET /api/employees/search?department=IT&role=Developer` - Search employees
- `POST /api/employees` - Create new employee
- `PUT /api/employees/{id}` - Update employee
- `DELETE /api/employees/{id}` - Delete employee

### Statistics
- `GET /api/statistics/dashboard` - Get dashboard statistics

## Technologies Used
- **Framework**: ASP.NET Core 8
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Frontend**: Bootstrap 5
- **Logging**: Serilog
- **Testing**: xUnit, Moq

## Best Practices
- Separation of concerns with layered architecture
- Dependency injection for loose coupling
- Repository pattern for data access
- Unit of Work pattern for transaction management
- DTOs for API contracts
- Async/await throughout
- Comprehensive logging
- Unit and integration test coverage

## Design Documents
- [Real-Time Chat Feature](DESIGN_REALTIME_CHAT.md) - Technical design for WebSocket-based chat with E2E encryption

## License
MIT
