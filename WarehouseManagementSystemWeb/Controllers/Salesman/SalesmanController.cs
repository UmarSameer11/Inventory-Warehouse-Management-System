using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Salesman;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Salesman
{
    public class SalesmanController : Controller
    {
        private readonly ISalesmanService _salesmanService;

        public SalesmanController(ISalesmanService salesmanService)
        {
            _salesmanService = salesmanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _salesmanService.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SalesmanCreateViewModel
            {
                IsActive = true
            };

            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesmanCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesmanService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Salesman created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create salesman.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create salesman."));
                }
            }

            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _salesmanService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Salesman not found.";
                return RedirectToAction(nameof(Index));
            }

            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SalesmanUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _salesmanService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Salesman updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update salesman.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update salesman."));
                }
            }

            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _salesmanService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Salesman not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _salesmanService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Salesman not found.";
                return RedirectToAction(nameof(Index));
            }

            // Lets the page show names instead of raw ids.
            await _salesmanService.PopulateDropdownsAsync(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _salesmanService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Salesman could not be deleted. It may be used by other records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Salesman deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
