using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.DispatchDetail;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.DispatchDetail
{
    public class DispatchDetailController : Controller
    {
        private readonly IDispatchDetailService _dispatchDetailService;

        public DispatchDetailController(IDispatchDetailService dispatchDetailService)
        {
            _dispatchDetailService = dispatchDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _dispatchDetailService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new DispatchDetailCreateViewModel();

            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DispatchDetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _dispatchDetailService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Dispatch detail created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create dispatch detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create dispatch detail."));
                }
            }

            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _dispatchDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Dispatch detail not found.";
                return RedirectToAction(nameof(Index));
            }

            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DispatchDetailUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _dispatchDetailService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Dispatch detail updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update dispatch detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update dispatch detail."));
                }
            }

            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _dispatchDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Dispatch detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dispatchDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Dispatch detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _dispatchDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dispatchDetailService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Dispatch detail could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Dispatch detail deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
