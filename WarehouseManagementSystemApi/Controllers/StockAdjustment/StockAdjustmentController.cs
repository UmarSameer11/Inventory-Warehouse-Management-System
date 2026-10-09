using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.StockAdjustment;
[Route("api/[controller]")]
[ApiController]
public class StockAdjustmentController : ControllerBase
{
    private readonly IStockAdjustmentService _service;
    public StockAdjustmentController(IStockAdjustmentService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] StockAdjustmentCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "StockAdjustment deleted successfully." }); }
}
