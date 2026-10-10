using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.StockMovement;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.StockMovement
{
    public class StockMovementController : Controller
    {
        private readonly IStockMovementService _stockMovementService;

        public StockMovementController(IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _stockMovementService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StockMovementCreateViewModel
            {
                MovementDate = DateTime.Today
            };

            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockMovementCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockMovementService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock movement created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create stock movement.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create stock movement."));
                }
            }

            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var model = await _stockMovementService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock movement not found.";
                return RedirectToAction(nameof(Index));
            }

            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StockMovementUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _stockMovementService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Stock movement updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update stock movement.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update stock movement."));
                }
            }

            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var model = await _stockMovementService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock movement not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _stockMovementService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Stock movement not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _stockMovementService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _stockMovementService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Stock movement could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Stock movement deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
