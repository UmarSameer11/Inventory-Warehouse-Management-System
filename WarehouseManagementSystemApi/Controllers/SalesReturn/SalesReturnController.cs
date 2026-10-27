using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.SalesReturn;

[Route("api/[controller]")]
[ApiController]
public class SalesReturnController : ControllerBase
{
    private readonly ISalesReturnService _service;

    public SalesReturnController(ISalesReturnService service)
    {
        _service = service;
    }

    // POST: api/SalesReturn
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SalesReturnCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/SalesReturn
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/SalesReturn/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // DELETE: api/SalesReturn/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Sales return deleted successfully."
        });
    }
}