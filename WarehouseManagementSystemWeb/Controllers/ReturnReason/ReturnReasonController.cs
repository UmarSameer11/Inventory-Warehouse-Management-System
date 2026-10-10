using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.ReturnReason;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.ReturnReason
{
    public class ReturnReasonController : Controller
    {
        private readonly IReturnReasonService _returnReasonService;

        public ReturnReasonController(IReturnReasonService returnReasonService)
        {
            _returnReasonService = returnReasonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _returnReasonService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ReturnReasonCreateViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReturnReasonCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _returnReasonService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Return reason created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create return reason.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create return reason."));
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _returnReasonService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Return reason not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ReturnReasonUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _returnReasonService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Return reason updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update return reason.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update return reason."));
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _returnReasonService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Return reason not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _returnReasonService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Return reason not found.";
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
                await _returnReasonService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Return reason could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Return reason deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
