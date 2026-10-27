using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.VehicleType;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.VehicleType;

[Route("api/[controller]")]
[ApiController]
public class VehicleTypeController : ControllerBase
{
    private readonly IVehicleTypeService _service;

    public VehicleTypeController(IVehicleTypeService service)
    {
        _service = service;
    }

    // POST: api/VehicleType
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] VehicleTypeCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/VehicleType
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/VehicleType/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/VehicleType/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] VehicleTypeUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Vehicle type updated successfully."
        });
    }

    // DELETE: api/VehicleType/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Vehicle type deleted successfully."
        });
    }
}