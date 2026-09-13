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
        public async Task<IActionResult> Index()
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

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            var employee = _employeeService.CreateAsync(model);

            return View(employee);
        }

      
    }
}
