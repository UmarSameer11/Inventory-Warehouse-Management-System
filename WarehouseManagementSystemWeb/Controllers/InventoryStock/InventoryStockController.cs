using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.InventoryStock
{
    public class InventoryStockController : Controller
    {
        private readonly IInventoryStockService _inventoryStockService;

        public InventoryStockController(IInventoryStockService inventoryStockService)
        {
            _inventoryStockService = inventoryStockService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stocks = await _inventoryStockService.GetAllAsync();

            return View(stocks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new InventoryStockCreateViewModel();

            await _inventoryStockService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryStockCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _inventoryStockService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Inventory stock created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create inventory stock.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create inventory stock."));
                }
            }

            await _inventoryStockService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _inventoryStockService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Inventory stock not found.";
                return RedirectToAction(nameof(Index));
            }

            await _inventoryStockService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(InventoryStockUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _inventoryStockService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Inventory stock updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update inventory stock.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update inventory stock."));
                }
            }

            await _inventoryStockService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _inventoryStockService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Inventory stock not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _inventoryStockService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Inventory stock not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _inventoryStockService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(ex, "Inventory stock could not be deleted.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Inventory stock deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
