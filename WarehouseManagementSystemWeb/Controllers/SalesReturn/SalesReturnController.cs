using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesReturn;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.SalesReturn
{
    public class SalesReturnController : Controller
    {
        private readonly ISalesReturnService _salesReturnService;

        public SalesReturnController(ISalesReturnService salesReturnService)
        {
            _salesReturnService = salesReturnService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _salesReturnService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SalesReturnCreateViewModel
            {
                ReturnDate = DateTime.Today
            };

            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesReturnCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesReturnService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Sales return created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create sales return.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create sales return."));
                }
            }

            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _salesReturnService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales return not found.";
                return RedirectToAction(nameof(Index));
            }

            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SalesReturnUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesReturnService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Sales return updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update sales return.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update sales return."));
                }
            }

            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _salesReturnService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales return not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _salesReturnService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales return not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesReturnService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _salesReturnService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Sales return could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Sales return deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
