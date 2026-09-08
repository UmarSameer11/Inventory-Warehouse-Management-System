using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.ProductCategory;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.ProductCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnitOfMeasure([FromBody] ProductCategoryCreateDto productCategory)
        {
            await _service.CreateProductCategoryAsync(productCategory);

            return Ok(new
            {
                Message = "ProductCategory successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductCategory()
        {
            var productCategory = await _service.GetProductCategoryListAsync();

            return Ok(productCategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var productCategory = await _service.GetProductCategoryByIdAsync(id);

            return Ok(productCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] ProductCategoryUpdateDto productCategory)
        {
            await _service.UpdateProductCategoryAsync(id, productCategory);

            return Ok(new { Message = "ProductCategory updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            await _service.DeleteProductCategoryAsync(id);

            return Ok(new { Message = "ProductCategory deleted successfully" });
        }
    }
}
