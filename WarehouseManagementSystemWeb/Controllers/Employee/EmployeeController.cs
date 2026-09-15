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

            var response = await _employeeService.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message;

                return RedirectToAction(nameof(Index));
            }

            await _employeeService.GetDeptDesigForDropdownAsync();

            TempData["ErrorMessage"] =
                response?.Message ?? "Failed to create employee.";

            return View(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _employeeService.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] =
                    response.Message ?? "Employee updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] =
                response?.Message ?? "Failed to update employee.";

            return View(model);
        }

    }
}
