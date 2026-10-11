using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.DispatchDetail;
[Route("api/[controller]")]
[ApiController]
public class DispatchDetailController : ControllerBase
{
    private readonly IDispatchDetailService _service;
    public DispatchDetailController(IDispatchDetailService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] DispatchDetailCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DispatchDetailListDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "DispatchDetail updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "DispatchDetail deleted successfully." }); }
}
