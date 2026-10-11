using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.StockAdjustmentDetail;
[Route("api/[controller]")]
[ApiController]
public class StockAdjustmentDetailController : ControllerBase
{
    private readonly IStockAdjustmentDetailService _service;
    public StockAdjustmentDetailController(IStockAdjustmentDetailService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] StockAdjustmentDetailCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "StockAdjustmentDetail deleted successfully." }); }
}
