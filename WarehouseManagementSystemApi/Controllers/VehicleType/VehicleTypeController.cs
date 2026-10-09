using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.VehicleType;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.VehicleType;
[Route("api/[controller]")]
[ApiController]
public class VehicleTypeController : ControllerBase
{
    private readonly IVehicleTypeService _service;
    public VehicleTypeController(IVehicleTypeService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] VehicleTypeCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleTypeUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "VehicleType updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "VehicleType deleted successfully." }); }
}
