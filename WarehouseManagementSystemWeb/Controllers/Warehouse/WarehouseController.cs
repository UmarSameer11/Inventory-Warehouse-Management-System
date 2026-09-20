using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Warehouse;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Warehouse
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouse;

        public WarehouseController(IWarehouseService warehouse)
        {
            _warehouse = warehouse;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _warehouse.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new WarehouseCreateViewModel
            {
                Employees = await _warehouse.GetEmployeeDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WarehouseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Employees = await _warehouse.GetEmployeeDropdownAsync();
                return View(model);
            }

            var response = await _warehouse.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Warehouse created successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to create warehouse.";
            model.Employees = await _warehouse.GetEmployeeDropdownAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _warehouse.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Warehouse not found.";
                return RedirectToAction(nameof(Index));
            }

            model.Employees = await _warehouse.GetEmployeeDropdownAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(WarehouseUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Employees = await _warehouse.GetEmployeeDropdownAsync();
                return View(model);
            }

            var response = await _warehouse.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Warehouse updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to update warehouse.";
            model.Employees = await _warehouse.GetEmployeeDropdownAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _warehouse.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Warehouse not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _warehouse.DeleteAsync(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Warehouse could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Warehouse deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _warehouse.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Warehouse not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}