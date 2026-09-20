using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.ProductCategory;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.ProductCategory
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService service)
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
        public async Task<IActionResult> Create(ProductCategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _service.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Product Category created successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to create product category.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Product Category not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductCategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _service.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = response.Message ?? "Product Category updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = response?.Message ?? "Failed to update product category.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Product Category not found.";
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
                TempData["ErrorMessage"] = "Product Category could not be deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Product Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _service.GetByIdAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Product Category not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}