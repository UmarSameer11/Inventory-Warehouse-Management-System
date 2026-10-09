using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.GoodsReceipt;
[Route("api/[controller]")]
[ApiController]
public class GoodsReceiptController : ControllerBase
{
    private readonly IGoodsReceiptService _service;
    public GoodsReceiptController(IGoodsReceiptService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] GoodsReceiptCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "GoodsReceipt deleted successfully." }); }
}
