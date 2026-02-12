using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Web.Data
{
    public static class DataSeeder
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Check if database has any employees already
                if (await context.Employees.AnyAsync())
                {
                    return; // Database is already populated
                }

                // Create sample employees
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "John Smith",
                        Email = "john.smith@example.com",
                        Department = "Information Technology",
                        Role = "Senior Developer",
                        HireDate = new DateTime(2020, 3, 15),
                        Salary = 95000m
                    },
                    new Employee
                    {
                        Name = "Sarah Johnson",
                        Email = "sarah.johnson@example.com",
                        Department = "Human Resources",
                        Role = "HR Manager",
                        HireDate = new DateTime(2019, 6, 1),
                        Salary = 75000m
                    },
                    new Employee
                    {
                        Name = "Michael Chen",
                        Email = "michael.chen@example.com",
                        Department = "Information Technology",
                        Role = "Developer",
                        HireDate = new DateTime(2021, 1, 10),
                        Salary = 75000m
                    },
                    new Employee
                    {
                        Name = "Emily Davis",
                        Email = "emily.davis@example.com",
                        Department = "Finance",
                        Role = "Financial Analyst",
                        HireDate = new DateTime(2020, 8, 22),
                        Salary = 70000m
                    },
                    new Employee
                    {
                        Name = "Robert Wilson",
                        Email = "robert.wilson@example.com",
                        Department = "Sales",
                        Role = "Sales Manager",
                        HireDate = new DateTime(2018, 5, 14),
                        Salary = 85000m
                    },
                    new Employee
                    {
                        Name = "Lisa Anderson",
                        Email = "lisa.anderson@example.com",
                        Department = "Information Technology",
                        Role = "QA Engineer",
                        HireDate = new DateTime(2021, 9, 5),
                        Salary = 65000m
                    }
                };

                context.Employees.AddRange(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}
