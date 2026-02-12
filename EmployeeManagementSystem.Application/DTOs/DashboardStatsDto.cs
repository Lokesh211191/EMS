namespace EmployeeManagementSystem.Application.DTOs;

public class DashboardStatsDto
{
    public int TotalEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal TotalPayroll { get; set; }
}
