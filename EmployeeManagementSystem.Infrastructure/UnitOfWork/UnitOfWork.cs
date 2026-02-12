namespace EmployeeManagementSystem.Infrastructure.UnitOfWork;

using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IEmployeeRepository? _employeeRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEmployeeRepository Employees
    {
        get
        {
            _employeeRepository ??= new EmployeeRepository(_context);
            return _employeeRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}
