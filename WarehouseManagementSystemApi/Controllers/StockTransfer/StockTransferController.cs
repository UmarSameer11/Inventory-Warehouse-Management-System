using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.StockTransfer;
[Route("api/[controller]")]
[ApiController]
public class StockTransferController : ControllerBase
{
    private readonly IStockTransferService _service;
    public StockTransferController(IStockTransferService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] StockTransferCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StockTransferUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "StockTransfer updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "StockTransfer deleted successfully." }); }
}
