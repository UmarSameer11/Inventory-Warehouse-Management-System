using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Batch;
using WarehouseManagementSystemWeb.Common;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Batch
{
    public class BatchController : Controller
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var batches = await _batchService.GetAllAsync();

            return View(batches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BatchCreateViewModel
            {
                ManufacturingDate = DateTime.Today,
                Products = await _batchService.GetProductDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BatchCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _batchService.CreateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Batch created successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to create batch.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to create batch."));
                }
            }

            model.Products = await _batchService.GetProductDropdownAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _batchService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Batch not found.";
                return RedirectToAction(nameof(Index));
            }

            model.Products = await _batchService.GetProductDropdownAsync(model.ProductId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BatchUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _batchService.UpdateAsync(model);

                    if (response?.Success == true)
                    {
                        TempData["SuccessMessage"] = "Batch updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(string.Empty, response?.Message ?? "Failed to update batch.");
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, ApiErrorMessage.From(ex, "Failed to update batch."));
                }
            }

            model.Products = await _batchService.GetProductDropdownAsync(model.ProductId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _batchService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Batch not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _batchService.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Batch not found.";
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
                await _batchService.DeleteAsync(id);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = ApiErrorMessage.From(
                    ex,
                    "Batch could not be deleted. It may be used by inventory stock records.");

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Batch deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
