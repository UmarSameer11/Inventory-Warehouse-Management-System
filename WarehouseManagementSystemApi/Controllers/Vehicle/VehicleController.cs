using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Vehicle;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.Vehicle;
[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _service;
    public VehicleController(IVehicleService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] VehicleCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "Vehicle updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "Vehicle deleted successfully." }); }
}
