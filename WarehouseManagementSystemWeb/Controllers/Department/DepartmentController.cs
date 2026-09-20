using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Department;
using WarehouseManagementSystemWeb.Services.Implementations;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Department
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _department;
        public DepartmentController(IDepartmentService department)
        {
            _department = department;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var depart = await _department.GetAllAsync();

            return View(depart);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _department.CreateAsync(model);
                return View(model);
            }

            var response  = await _department.CreateAsync(model);

            if(response?.Success == true)
            {
                TempData["SuccessMessage"] = "Department updated successfully." ?? response.Message;

                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to create department." ?? response.Message;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var depart = await _department.GetByIdAsync(id);

            return View(depart);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dept = await _department.UpdateAsync(model);

            if(dept?.Success == true)
            {
                TempData["SuccessMessage"] = "Department updated successfully." ?? dept.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to updated department." ?? dept.Message;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _department.GetByIdAsync(id);

            if (dept == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dept = await _department.GetByIdAsync(id);
            if (dept == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _department.DeleteAsync(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Department could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Department deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var dept = await _department.GetByIdAsync(id);
            if (dept == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }
           
            return View(dept);
        }

    }
}
