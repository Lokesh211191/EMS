#!/bin/bash

echo "===================================="
echo "Employee Management System Setup"
echo "===================================="

echo ""
echo "Installing NuGet packages..."

echo "Restoring Core project..."
dotnet restore EmployeeManagementSystem.Core

echo "Restoring Application project..."
dotnet restore EmployeeManagementSystem.Application

echo "Restoring Infrastructure project..."
dotnet restore EmployeeManagementSystem.Infrastructure

echo "Restoring API project..."
dotnet restore EmployeeManagementSystem.Api

echo "Restoring Web project..."
dotnet restore EmployeeManagementSystem.Web

echo "Restoring Test project..."
dotnet restore EmployeeManagementSystem.Tests

echo ""
echo "===================================="
echo "Setup Complete!"
echo "===================================="
echo ""
echo "Next Steps:"
echo "1. Update the connection string in appsettings.json"
echo "2. Run: dotnet ef database update --project EmployeeManagementSystem.Infrastructure --startup-project EmployeeManagementSystem.Api"
echo "3. Run the Web project: cd EmployeeManagementSystem.Web && dotnet run"
echo "4. Navigate to https://localhost:5001"
echo ""
