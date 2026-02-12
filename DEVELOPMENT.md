# Employee Management System Development Guide

## Quick Start

### 1. Prerequisites
- .NET 8 SDK or later
- SQL Server (local or remote)
- Visual Studio Code with C# extension

### 2. Setup Database Connection
Edit `EmployeeManagementSystem.Api\appsettings.json` and `EmployeeManagementSystem.Web\appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YourServerName;Database=EmployeeManagementSystem;Integrated Security=True;TrustServerCertificate=True;"
}
```

### 3. Run Setup Script
**Windows:**
```bash
.\setup.bat
```

**Linux/Mac:**
```bash
chmod +x setup.sh
./setup.sh
```

### 4. Create Database
```bash
dotnet ef database update --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api
```

### 5. Run the Application

**Web Application (MVC):**
```bash
cd EmployeeManagementSystem.Web
dotnet run
# Opens at https://localhost:5001
```

**API:**
```bash
cd EmployeeManagementSystem.Api
dotnet run
# Opens at https://localhost:5000
```

### 6. Run Tests
```bash
dotnet test EmployeeManagementSystem.Tests
```

## Project Organization

### EmployeeManagementSystem.Core
- **Entities**: Domain models (Employee, User)
- **Interfaces**: Repository and UnitOfWork contracts

### EmployeeManagementSystem.Application
- **Services**: Business logic (EmployeeService)
- **DTOs**: Data transfer objects
- **Mappings**: AutoMapper profiles

### EmployeeManagementSystem.Infrastructure
- **Data**: Entity Framework DbContext
- **Repositories**: Repository implementations
- **UnitOfWork**: Transaction management

### EmployeeManagementSystem.Api
- **Controllers**: REST API endpoints
- **Configuration**: DI setup in Program.cs

### EmployeeManagementSystem.Web
- **Controllers**: MVC request handlers
- **Views**: Razor templates (Bootstrap)
- **Static**: CSS/JS assets

### EmployeeManagementSystem.Tests
- **Unit Tests**: Business logic tests
- **Mocks**: Testing utilities

## Common Commands

### Build Solution
```bash
dotnet build
```

### Run Tests with Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

### Create New Migration
```bash
dotnet ef migrations add YourMigrationName --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api
```

### Remove Last Migration
```bash
dotnet ef migrations remove --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api
```

### Update Database
```bash
dotnet ef database update --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api
```

## API Testing with cURL

### Get All Employees
```bash
curl -X GET https://localhost:5000/api/employees
```

### Create Employee
```bash
curl -X POST https://localhost:5000/api/employees \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "department": "IT",
    "role": "Developer",
    "hireDate": "2024-01-15",
    "salary": 5000
  }'
```

### Get Dashboard Stats
```bash
curl -X GET https://localhost:5000/api/statistics/dashboard
```

## Debugging

### Enable Debug Logging
Edit `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug"
    }
  }
}
```

### View Application Logs
Check the `logs/` directory in each project folder.

## Performance Tips

1. **Use Pagination** for large datasets
2. **Add Database Indexes** on frequently searched columns
3. **Implement Caching** for dashboard statistics
4. **Optimize Queries** with EF Core Select()

## Security Considerations

1. **SQL Injection**: ✅ Protected by parameterized queries
2. **CSRF Protection**: ✅ AntiForgerToken in forms
3. **Input Validation**: Add FluentValidation rules
4. **Authentication**: Implement JWT token-based auth
5. **HTTPS**: Always use HTTPS in production

## Extending the Application

### Add New Entity
1. Create Entity in Core layer
2. Add DbSet in ApplicationDbContext
3. Create Migration
4. Create DTO and AutoMapper profile
5. Create Service with CRUD methods
6. Add Repository interface and implementation
7. Create API Controller
8. Create MVC Controller and Views
9. Write Unit Tests

### Add New Feature
Follow the same pattern for consistency and maintainability.

## Troubleshooting

### Connection String Issues
- Verify SQL Server is running
- Check server name and database name
- Confirm integrated security settings

### Migration Issues
- Delete pending migrations and reapply
- Ensure Infrastructure project is startup project for EF commands
- Check __EFMigrationsHistory table

### Dependencies Not Found
```bash
dotnet restore
dotnet clean
dotnet build
```

## Resources
- [Microsoft .NET Documentation](https://docs.microsoft.com/dotnet/)
- [Entity Framework Core Docs](https://docs.microsoft.com/ef/core/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/)

## License
MIT
