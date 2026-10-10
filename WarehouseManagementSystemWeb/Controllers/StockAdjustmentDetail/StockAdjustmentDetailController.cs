using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustmentDetail;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.StockAdjustmentDetail
{
    public class StockAdjustmentDetailController : Controller
    {
        private readonly IStockAdjustmentDetailService _stockAdjustmentDetailService;

        public StockAdjustmentDetailController(IStockAdjustmentDetailService stockAdjustmentDetailService)
        {
            _stockAdjustmentDetailService = stockAdjustmentDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _stockAdjustmentDetailService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StockAdjustmentDetailCreateViewModel();

            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockAdjustmentDetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockAdjustmentDetailService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock adjustment detail created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create stock adjustment detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create stock adjustment detail."));
                }
            }

            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _stockAdjustmentDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment detail not found.";
                return RedirectToAction(nameof(Index));
            }

            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StockAdjustmentDetailUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockAdjustmentDetailService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock adjustment detail updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update stock adjustment detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update stock adjustment detail."));
                }
            }

            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _stockAdjustmentDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _stockAdjustmentDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock adjustment detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockAdjustmentDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _stockAdjustmentDetailService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Stock adjustment detail could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Stock adjustment detail deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
