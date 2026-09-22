using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Services.Implementations;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _productService.DropdownProductWithUnitAndCategoryAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _productService.DropdownProductWithUnitAndCategoryAsync();
                return View(model);
            }
            var response = await _productService.CreateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = "Product created successfully." ?? response.Message;

                return RedirectToAction(nameof(Index));
            }

              await _productService.DropdownProductWithUnitAndCategoryAsync();

            TempData["ErrorMessage"] = "Failed to create designation." ?? response.Message;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _productService.DropdownProductWithUnitAndCategoryAsync();

            product.ProductCategories = result.ProductCategories;
            product.UnitOfMeasures = result.UnitOfMeasures;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _productService.DropdownProductWithUnitAndCategoryAsync();
                return View(model);
            }
            var response = await _productService.UpdateAsync(model);

            if (response?.Success == true)
            {
                TempData["SuccessMessage"] = "Product updated successfully." ?? response.Message;

                return RedirectToAction(nameof(Index));
            }

            await _productService.DropdownProductWithUnitAndCategoryAsync();

            TempData["ErrorMessage"] = "Failed to updated product." ?? response.Message;

            return View(model);
        }
    }
}
