using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.VehicleAssignment;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.VehicleAssignment
{
    public class VehicleAssignmentController : Controller
    {
        private readonly IVehicleAssignmentService _vehicleAssignmentService;

        public VehicleAssignmentController(IVehicleAssignmentService vehicleAssignmentService)
        {
            _vehicleAssignmentService = vehicleAssignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _vehicleAssignmentService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new VehicleAssignmentCreateViewModel
            {
                AssignmentDate = DateTime.Today,
                IsActive = true
            };

            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleAssignmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _vehicleAssignmentService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Vehicle assignment created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create vehicle assignment.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create vehicle assignment."));
                }
            }

            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _vehicleAssignmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleAssignmentUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _vehicleAssignmentService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Vehicle assignment updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update vehicle assignment.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update vehicle assignment."));
                }
            }

            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _vehicleAssignmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _vehicleAssignmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Vehicle assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _vehicleAssignmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _vehicleAssignmentService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Vehicle assignment could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Vehicle assignment deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
