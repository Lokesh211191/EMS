namespace EmployeeManagementSystem.Core.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IEmployeeRepository Employees { get; }
    Task<int> SaveChangesAsync();
}
