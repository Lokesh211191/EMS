namespace EmployeeManagementSystem.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Services;

public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? department = null, string? role = null)
    {
        try
        {
            IEnumerable<EmployeeDto> employees;
            if (!string.IsNullOrEmpty(department) || !string.IsNullOrEmpty(role))
            {
                employees = await _employeeService.SearchEmployeesAsync(department, role);
            }
            else
            {
                employees = await _employeeService.GetAllEmployeesAsync();
            }

            ViewData["Department"] = department;
            ViewData["Role"] = role;
            return View(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading employees");
            return View("Error");
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _employeeService.CreateEmployeeAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            ModelState.AddModelError("", "Error creating employee");
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading employee {Id}", id);
            return View("Error");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateEmployeeDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            await _employeeService.UpdateEmployeeAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {Id}", id);
            ModelState.AddModelError("", "Error updating employee");
            return View(dto);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {Id}", id);
            return View("Error");
        }
    }
}
