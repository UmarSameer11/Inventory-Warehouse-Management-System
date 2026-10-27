using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.VehicleAssignment;

[Route("api/[controller]")]
[ApiController]
public class VehicleAssignmentController : ControllerBase
{
    private readonly IVehicleAssignmentService _service;

    public VehicleAssignmentController(
        IVehicleAssignmentService service)
    {
        _service = service;
    }

    // POST: api/VehicleAssignment
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] VehicleAssignmentCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/VehicleAssignment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/VehicleAssignment/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/VehicleAssignment/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] VehicleAssignmentUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Vehicle assignment updated successfully."
        });
    }

    // DELETE: api/VehicleAssignment/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Vehicle assignment deleted successfully."
        });
    }
}