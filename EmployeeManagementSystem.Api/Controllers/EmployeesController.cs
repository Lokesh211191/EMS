namespace EmployeeManagementSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Services;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employee {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployees(
        [FromQuery] string? department = null,
        [FromQuery] string? role = null)
    {
        try
        {
            var employees = await _employeeService.SearchEmployeesAsync(department, role);
            return Ok(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching employees");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateEmployeeAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        try
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
                return NotFound("Employee not found");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
