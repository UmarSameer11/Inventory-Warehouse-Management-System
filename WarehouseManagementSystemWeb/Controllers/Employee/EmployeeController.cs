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

            var employee = await _employeeService.GetDeptDesigForDropdownAsync();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _employeeService.GetDeptDesigForDropdownAsync();
                return View(model);
            }

            var employee = await _employeeService.CreateAsync(model);

            if (employee)
            {
                return RedirectToAction(nameof(Index), new
                {
                    message = "Employee created successfully",
                    type = "success"
                });
            }

            await _employeeService.GetDeptDesigForDropdownAsync();

            return RedirectToAction(nameof(Index), new
            {
                message = "Failed to create employee",
                type = "error"

            });
        }


    }
}
