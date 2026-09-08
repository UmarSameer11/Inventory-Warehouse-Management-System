using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Batch;
using WarehouseManagementSystemApi.DTOs.InventoryStock;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Batch
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _service;

        public BatchController(IBatchService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBatch([FromBody] BatchCreateDto batch)
        {
            await _service.CreateBatchAsync(batch);

            return Ok(new
            {
                Message = "Batch successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBatches()
        {
            var batch = await _service.GetBatchListAsync();

            return Ok(batch);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatchById(int id)
        {
            var batch = await _service.GetBatchByIdAsync(id);

            return Ok(batch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBatch(int id, [FromBody] BatchUpdateDto batch)
        {
            await _service.UpdateBatchAsync(id, batch);

            return Ok(new { Message = "Batch updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            await _service.DeleteBatchAsync(id);

            return Ok(new { Message = "Batch deleted successfully" });
        }
    }
}
