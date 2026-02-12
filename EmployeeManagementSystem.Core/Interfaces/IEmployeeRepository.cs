namespace EmployeeManagementSystem.Core.Interfaces;

using EmployeeManagementSystem.Core.Entities;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<IEnumerable<Employee>> GetByDepartmentAsync(string department);
    Task<IEnumerable<Employee>> GetByRoleAsync(string role);
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
    Task<(IEnumerable<Employee> Items, int Total)> GetPagedAsync(int page, int pageSize);
}
