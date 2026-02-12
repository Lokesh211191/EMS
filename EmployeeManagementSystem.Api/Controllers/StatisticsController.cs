namespace EmployeeManagementSystem.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Services;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<StatisticsController> _logger;

    public StatisticsController(IEmployeeService employeeService, ILogger<StatisticsController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
    {
        try
        {
            var stats = await _employeeService.GetDashboardStatsAsync();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving dashboard statistics");
            return StatusCode(500, "Internal server error");
        }
    }
}
