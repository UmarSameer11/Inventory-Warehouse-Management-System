using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.GoodsReceipt
{
    public class GoodsReceiptController : Controller
    {
        private readonly IGoodsReceiptService _goodsReceiptService;

        public GoodsReceiptController(IGoodsReceiptService goodsReceiptService)
        {
            _goodsReceiptService = goodsReceiptService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _goodsReceiptService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new GoodsReceiptCreateViewModel
            {
                ReceiptDate = DateTime.Today
            };

            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoodsReceiptCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _goodsReceiptService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Goods receipt created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create goods receipt.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create goods receipt."));
                }
            }

            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _goodsReceiptService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt not found.";
                return RedirectToAction(nameof(Index));
            }

            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GoodsReceiptUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _goodsReceiptService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Goods receipt updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update goods receipt.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update goods receipt."));
                }
            }

            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _goodsReceiptService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _goodsReceiptService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _goodsReceiptService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _goodsReceiptService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Goods receipt could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Goods receipt deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
