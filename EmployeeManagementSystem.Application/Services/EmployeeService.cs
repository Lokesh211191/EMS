namespace EmployeeManagementSystem.Application.Services;

using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;
using AutoMapper;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string? department = null, string? role = null);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
    Task<EmployeeDto> UpdateEmployeeAsync(UpdateEmployeeDto dto);
    Task<bool> DeleteEmployeeAsync(int id);
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
    }

    public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string? department = null, string? role = null)
    {
        IEnumerable<Employee> results;

        if (!string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(role))
        {
            var byDept = await _unitOfWork.Employees.GetByDepartmentAsync(department);
            var byRole = await _unitOfWork.Employees.GetByRoleAsync(role);
            results = byDept.Intersect(byRole);
        }
        else if (!string.IsNullOrEmpty(department))
        {
            results = await _unitOfWork.Employees.GetByDepartmentAsync(department);
        }
        else if (!string.IsNullOrEmpty(role))
        {
            results = await _unitOfWork.Employees.GetByRoleAsync(role);
        }
        else
        {
            results = await _unitOfWork.Employees.GetAllAsync();
        }

        return _mapper.Map<IEnumerable<EmployeeDto>>(results);
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        var result = await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(result);
    }

    public async Task<EmployeeDto> UpdateEmployeeAsync(UpdateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        employee.UpdatedAt = DateTime.UtcNow;
        var result = await _unitOfWork.Employees.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(result);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var result = await _unitOfWork.Employees.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        var employeeList = employees.ToList();

        return new DashboardStatsDto
        {
            TotalEmployees = employeeList.Count,
            TotalDepartments = employeeList.Select(e => e.Department).Distinct().Count(),
            AverageSalary = employeeList.Count > 0 ? employeeList.Average(e => e.Salary) : 0,
            TotalPayroll = employeeList.Sum(e => e.Salary)
        };
    }
}
