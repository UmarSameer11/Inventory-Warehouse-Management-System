using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.PurchaseOrder;
[Route("api/[controller]")]
[ApiController]
public class PurchaseOrderController : ControllerBase
{
    private readonly IPurchaseOrderService _service;
    public PurchaseOrderController(IPurchaseOrderService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] PurchaseOrderCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PurchaseOrderUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "PurchaseOrder updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "PurchaseOrder deleted successfully." }); }
}
