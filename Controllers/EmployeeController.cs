using marketplace_services_CSI5112.Models;
using marketplace_services_CSI5112.Services;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_services_CSI5112.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService es)
    {
        this._employeeService = es;
    }

    [HttpGet]
    public List<Employee> Get()
    {
        return _employeeService.GetEmployees();
    }
}