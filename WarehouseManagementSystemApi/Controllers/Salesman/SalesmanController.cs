using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Salesman;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Salesman;

[Route("api/[controller]")]
[ApiController]
public class SalesmanController : ControllerBase
{
    private readonly ISalesmanService _service;

    public SalesmanController(ISalesmanService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesmanCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesmanUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok(new { Message = "Salesman updated successfully." });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = "Salesman deleted successfully." });
    }
}