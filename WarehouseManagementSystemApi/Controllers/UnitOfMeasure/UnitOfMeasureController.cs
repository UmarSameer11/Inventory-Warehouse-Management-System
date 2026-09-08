using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.UnitOfMeasure
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly IUnitOfMeasureService _service;

        public UnitOfMeasureController(IUnitOfMeasureService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnitOfMeasure([FromBody] UnitOfMeasureCreateDto unitOfMeasure)
        {
            await _service.CreateUnitOfMeasureAsync(unitOfMeasure);

            return Ok(new
            {
                Message = "UnitOfMeasure successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnitOfMeasures()
        {
            var unitOfMeasures = await _service.GetUnitOfMeasureListAsync();

            return Ok(unitOfMeasures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitOfMeasureById(int id)
        {
            var unitOfMeasure = await _service.GetUnitOfMeasureByIdAsync(id);

            return Ok(unitOfMeasure);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitOfMeasure(int id, [FromBody] UnitOfMeasureUpdateDto unitOfMeasure)
        {
            await _service.UpdateUnitOfMeasureAsync(id, unitOfMeasure);

            return Ok(new { Message = "UnitOfMeasure updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitOfMeasure(int id)
        {
            await _service.DeleteUnitOfMeasureAsync(id);

            return Ok(new { Message = "UnitOfMeasure deleted successfully" });
        }
    }
}
