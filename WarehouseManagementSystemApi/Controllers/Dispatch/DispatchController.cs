using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.Dispatch;
[Route("api/[controller]")]
[ApiController]
public class DispatchController : ControllerBase
{
    private readonly IDispatchService _service;
    public DispatchController(IDispatchService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] DispatchCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DispatchUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "Dispatch updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "Dispatch deleted successfully." }); }
}
