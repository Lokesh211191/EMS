namespace EmployeeManagementSystem.Tests;

using Xunit;
using Moq;
using EmployeeManagementSystem.Application.Services;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Core.Entities;
using AutoMapper;
using EmployeeManagementSystem.Application.Mappings;

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = mapperConfig.CreateMapper();
        _employeeService = new EmployeeService(_mockUnitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT", Role = "Developer", Salary = 5000 },
            new() { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "HR", Role = "Manager", Salary = 6000 }
        };

        _mockUnitOfWork.Setup(x => x.Employees.GetAllAsync()).ReturnsAsync(employees);

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateEmployeeAsync_AddsNewEmployee()
    {
        // Arrange
        var dto = new CreateEmployeeDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Department = "IT",
            Role = "Developer",
            HireDate = DateTime.Now,
            Salary = 5000
        };

        var employee = _mapper.Map<Employee>(dto);
        _mockUnitOfWork.Setup(x => x.Employees.AddAsync(It.IsAny<Employee>())).ReturnsAsync(employee);

        // Act
        var result = await _employeeService.CreateEmployeeAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
