using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.GoodsReceiptDetail
{
    public class GoodsReceiptDetailController : Controller
    {
        private readonly IGoodsReceiptDetailService _goodsReceiptDetailService;

        public GoodsReceiptDetailController(IGoodsReceiptDetailService goodsReceiptDetailService)
        {
            _goodsReceiptDetailService = goodsReceiptDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _goodsReceiptDetailService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new GoodsReceiptDetailCreateViewModel();

            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoodsReceiptDetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _goodsReceiptDetailService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Goods receipt detail created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create goods receipt detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create goods receipt detail."));
                }
            }

            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _goodsReceiptDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt detail not found.";
                return RedirectToAction(nameof(Index));
            }

            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GoodsReceiptDetailUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _goodsReceiptDetailService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Goods receipt detail updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update goods receipt detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update goods receipt detail."));
                }
            }

            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _goodsReceiptDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _goodsReceiptDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Goods receipt detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _goodsReceiptDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _goodsReceiptDetailService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Goods receipt detail could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Goods receipt detail deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
