using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.InventoryStock;
using WarehouseManagementSystemApi.DTOs.Warehouse;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.InventoryStock
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryStockController : ControllerBase
    {
        private readonly IInventoryStockService _service;

        public InventoryStockController(IInventoryStockService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateInventoryStock([FromBody] InventoryStockCreateDto inventoryStock)
        {
            await _service.CreateInventoryStockAsync(inventoryStock);

            return Ok(new
            {
                Message = "Inventory Stock successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventoryStocks()
        {
            var inventoryStocks = await _service.GetInventoryStockListAsync();

            return Ok(inventoryStocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryStockById(int id)
        {
            var inventoryStock = await _service.GetInventoryStockByIdAsync(id);

            return Ok(inventoryStock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryStock(int id, [FromBody] InventoryStockUpdateDto inventoryStock)
        {
            await _service.UpdateInventoryStockAsync(id, inventoryStock);

            return Ok(new { Message = "Inventory Stock updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryStock(int id)
        {
            await _service.DeleteInventoryStockAsync(id);

            return Ok(new { Message = "Inventory Stock deleted successfully" });
        }
    }
}
