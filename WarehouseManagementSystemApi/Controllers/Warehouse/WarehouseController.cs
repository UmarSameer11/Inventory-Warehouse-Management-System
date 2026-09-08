using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Warehouse;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Warehouse
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseCreateDto warehouse)
        {
            await _service.CreateWarehouseAsync(warehouse);

            return Ok(new
            {
                Message = "Warehouse successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _service.GetWarehouseListAsync();

            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _service.GetWarehouseByIdAsync(id);

            return Ok(warehouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] WarehouseUpdateDto warehouse)
        {
            await _service.UpdateWarehouseAsync(id, warehouse);

            return Ok(new { Message = "Warehouse updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            await _service.DeleteWarehouseAsync(id);

            return Ok(new { Message = "Warehouse deleted successfully" });
        }
    }
}
