namespace EmployeeManagementSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department)
    {
        return await _context.Employees
            .Where(e => e.Department == department)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByRoleAsync(string role)
    {
        return await _context.Employees
            .Where(e => e.Role == role)
            .ToListAsync();
    }

    public Task<Employee> AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        return Task.FromResult(employee);
    }

    public Task<Employee> UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.FromResult(employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return false;

        _context.Employees.Remove(employee);
        return true;
    }

    public async Task<(IEnumerable<Employee> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var total = await _context.Employees.CountAsync();
        var items = await _context.Employees
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }
}
