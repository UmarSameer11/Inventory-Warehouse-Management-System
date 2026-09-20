using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.UnitOfMeasure;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.UnitOfMeasure
{
    public class UnitOfMeasureController : Controller
    {
        private readonly IUnitOfMeasureService _service;

        public UnitOfMeasureController(IUnitOfMeasureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _service.GetAllAsync());

        [HttpGet]
        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitOfMeasureCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _service.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Unit of Measure created successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to create unit of measure.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Unit of Measure not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UnitOfMeasureUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _service.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Unit of Measure updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to update unit of measure.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Unit of Measure not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Unit of Measure could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Unit of Measure deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Unit of Measure not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}