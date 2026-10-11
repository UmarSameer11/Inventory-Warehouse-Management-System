using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.SalesOrderDetail;
[Route("api/[controller]")]
[ApiController]
public class SalesOrderDetailController : ControllerBase
{
    private readonly ISalesOrderDetailService _service;
    public SalesOrderDetailController(ISalesOrderDetailService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] SalesOrderDetailCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesOrderDetailListDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "SalesOrderDetail updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "SalesOrderDetail deleted successfully." }); }
}
