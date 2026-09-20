using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Designation;
using WarehouseManagementSystemWeb.Services.Implementations;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Designation
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designation;
        public DesignationController(IDesignationService designation)
        {
            _designation = designation;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var designation = await _designation.GetAllAsync();

            return View(designation);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DesignationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _designation.CreateAsync(model);
                return View(model);
            }

            var response = await _designation.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = "Designation created successfully." ?? response.Message;

                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to create designation." ?? response.Message;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var designation = await _designation.GetByIdAsync(id);

            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DesignationUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var designation = await _designation.UpdateAsync(model);

            if (designation?.Success == true)
            {
                TempData["SuccessMessage"] = "Designation updated successfully." ?? designation.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to updated designation." ?? designation.Message;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var designation = await _designation.GetByIdAsync(id);

            if (designation == null)
            {
                TempData["ErrorMessage"] = "Designation not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designation = await _designation.GetByIdAsync(id);
            if (designation == null)
            {
                TempData["ErrorMessage"] = "Designation not found.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _designation.DeleteAsync(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Designation could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Designation deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var designation = await _designation.GetByIdAsync(id);
            if (designation == null)
            {
                TempData["ErrorMessage"] = "Designation not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(designation);
        }

    }
}
