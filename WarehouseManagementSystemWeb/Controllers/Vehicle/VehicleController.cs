using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Vehicle;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Vehicle
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _vehicleService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new VehicleCreateViewModel
            {
                IsActive = true
            };

            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _vehicleService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Vehicle created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create vehicle.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create vehicle."));
                }
            }

            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _vehicleService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction(nameof(Index));
            }

            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _vehicleService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Vehicle updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update vehicle.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update vehicle."));
                }
            }

            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _vehicleService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _vehicleService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _vehicleService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _vehicleService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Vehicle could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Vehicle deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
