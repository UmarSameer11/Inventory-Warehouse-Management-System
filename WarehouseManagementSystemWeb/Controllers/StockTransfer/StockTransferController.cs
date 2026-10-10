using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransfer;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.StockTransfer
{
    public class StockTransferController : Controller
    {
        private readonly IStockTransferService _stockTransferService;

        public StockTransferController(IStockTransferService stockTransferService)
        {
            _stockTransferService = stockTransferService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _stockTransferService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StockTransferCreateViewModel
            {
                TransferDate = DateTime.Today
            };

            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockTransferCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockTransferService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock transfer created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create stock transfer.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create stock transfer."));
                }
            }

            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _stockTransferService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer not found.";
                return RedirectToAction(nameof(Index));
            }

            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StockTransferUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockTransferService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock transfer updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update stock transfer.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update stock transfer."));
                }
            }

            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _stockTransferService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _stockTransferService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock transfer not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockTransferService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _stockTransferService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Stock transfer could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Stock transfer deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
