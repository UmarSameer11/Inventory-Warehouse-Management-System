using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Dispatch;

[Route("api/[controller]")]
[ApiController]
public class DispatchController : ControllerBase
{
    private readonly IDispatchService _service;

    public DispatchController(IDispatchService service)
    {
        _service = service;
    }

    // POST: api/Dispatch
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] DispatchCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/Dispatch
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/Dispatch/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/Dispatch/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] DispatchUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Dispatch updated successfully."
        });
    }

    // DELETE: api/Dispatch/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Dispatch deleted successfully."
        });
    }
}