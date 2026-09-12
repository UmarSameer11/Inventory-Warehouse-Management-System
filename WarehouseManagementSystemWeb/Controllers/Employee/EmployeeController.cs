using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            var employees = await _employeeService.GetAllAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employee = new EmployeeCreateViewModel();

            return View(employee);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
