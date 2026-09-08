using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Product;
using WarehouseManagementSystemApi.DTOs.ProductCategory;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
        {
            await _service.CreateProductAsync(product);

            return Ok(new
            {
                Message = "Product successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _service.GetProductListAsync();

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            await _service.UpdateProductAsync(id, product);

            return Ok(new { Message = "Product updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProductAsync(id);

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}
