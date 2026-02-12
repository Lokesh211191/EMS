namespace EmployeeManagementSystem.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Application.Services;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IEmployeeService employeeService, ILogger<HomeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var stats = await _employeeService.GetDashboardStatsAsync();
            return View(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard");
            return View("Error");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
