using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Vehicle;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Vehicle;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _service;

    public VehicleController(IVehicleService service)
    {
        _service = service;
    }

    // POST: api/Vehicle
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] VehicleCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/Vehicle
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/Vehicle/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/Vehicle/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] VehicleUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Vehicle updated successfully."
        });
    }

    // DELETE: api/Vehicle/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Vehicle deleted successfully."
        });
    }
}