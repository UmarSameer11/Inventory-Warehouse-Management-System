using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.VehicleAssignment;
[Route("api/[controller]")]
[ApiController]
public class VehicleAssignmentController : ControllerBase
{
    private readonly IVehicleAssignmentService _service;
    public VehicleAssignmentController(IVehicleAssignmentService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] VehicleAssignmentCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleAssignmentUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "VehicleAssignment updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "VehicleAssignment deleted successfully." }); }
}
