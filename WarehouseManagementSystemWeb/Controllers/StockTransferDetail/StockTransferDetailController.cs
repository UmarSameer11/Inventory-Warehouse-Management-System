using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransferDetail;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.StockTransferDetail
{
    public class StockTransferDetailController : Controller
    {
        private readonly IStockTransferDetailService _stockTransferDetailService;

        public StockTransferDetailController(IStockTransferDetailService stockTransferDetailService)
        {
            _stockTransferDetailService = stockTransferDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _stockTransferDetailService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StockTransferDetailCreateViewModel();

            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockTransferDetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockTransferDetailService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock transfer detail created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create stock transfer detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create stock transfer detail."));
                }
            }

            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _stockTransferDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer detail not found.";
                return RedirectToAction(nameof(Index));
            }

            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StockTransferDetailUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockTransferDetailService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock transfer detail updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update stock transfer detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update stock transfer detail."));
                }
            }

            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _stockTransferDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _stockTransferDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockTransferDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _stockTransferDetailService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Stock transfer detail could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Stock transfer detail deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
