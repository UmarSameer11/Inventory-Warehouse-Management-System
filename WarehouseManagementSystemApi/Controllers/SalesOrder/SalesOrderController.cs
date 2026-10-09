using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.SalesOrder;
[Route("api/[controller]")]
[ApiController]
public class SalesOrderController : ControllerBase
{
    private readonly ISalesOrderService _service;
    public SalesOrderController(ISalesOrderService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] SalesOrderCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesOrderUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "SalesOrder updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "SalesOrder deleted successfully." }); }
}
