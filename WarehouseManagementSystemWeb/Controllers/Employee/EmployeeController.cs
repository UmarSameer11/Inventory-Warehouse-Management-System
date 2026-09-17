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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Index));
            }
            var result = await _employeeService.GetDeptDesigForDropdownAsync();

            employee.Departments = result.Departments;
            employee.Designations = result.Designations;
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _employeeService.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = "Employee updated successfully" ?? response.Message;

                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] =
                response?.Message ?? "Failed to update employee.";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Index));
            }
            var result = await _employeeService.GetDeptDesigForDropdownAsync();

            employee.Departments = result.Departments;
            employee.Designations = result.Designations;
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Index));
            }

                var success = await _employeeService.DeleteAsync(id);

            if (!success)
            {
                TempData["Error"] = "Employee could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Employee deleted successfully.";

            return RedirectToAction(nameof(Index));


        }


    }
}
