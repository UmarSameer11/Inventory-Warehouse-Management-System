using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustment;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.StockAdjustment
{
    public class StockAdjustmentController : Controller
    {
        private readonly IStockAdjustmentService _stockAdjustmentService;

        public StockAdjustmentController(IStockAdjustmentService stockAdjustmentService)
        {
            _stockAdjustmentService = stockAdjustmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _stockAdjustmentService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StockAdjustmentCreateViewModel
            {
                AdjustmentDate = DateTime.Today
            };

            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockAdjustmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockAdjustmentService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock adjustment created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create stock adjustment.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create stock adjustment."));
                }
            }

            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _stockAdjustmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment not found.";
                return RedirectToAction(nameof(Index));
            }

            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StockAdjustmentUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockAdjustmentService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock adjustment updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update stock adjustment.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update stock adjustment."));
                }
            }

            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _stockAdjustmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _stockAdjustmentService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockAdjustmentService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _stockAdjustmentService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Stock adjustment could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Stock adjustment deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
