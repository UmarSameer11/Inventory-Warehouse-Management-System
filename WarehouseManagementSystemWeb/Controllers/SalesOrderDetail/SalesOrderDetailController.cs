using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesOrderDetail;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.SalesOrderDetail
{
    public class SalesOrderDetailController : Controller
    {
        private readonly ISalesOrderDetailService _salesOrderDetailService;

        public SalesOrderDetailController(ISalesOrderDetailService salesOrderDetailService)
        {
            _salesOrderDetailService = salesOrderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _salesOrderDetailService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SalesOrderDetailCreateViewModel();

            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesOrderDetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesOrderDetailService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Sales order detail created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create sales order detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create sales order detail."));
                }
            }

            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _salesOrderDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales order detail not found.";
                return RedirectToAction(nameof(Index));
            }

            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SalesOrderDetailUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesOrderDetailService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Sales order detail updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update sales order detail.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update sales order detail."));
                }
            }

            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _salesOrderDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales order detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _salesOrderDetailService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Sales order detail not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesOrderDetailService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _salesOrderDetailService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Sales order detail could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Sales order detail deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
