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


    [HttpGet("by-number/{purchaseOrderNumber}")]
    public async Task<IActionResult> GetForGrnByNumber(
        string purchaseOrderNumber)
    {
        if (string.IsNullOrWhiteSpace(purchaseOrderNumber))
            return BadRequest(new
            {
                Success = false,
                Message = "Purchase order number is required."
            });

        var purchaseOrder = await _service.GetForGrnByNumberAsync(
            purchaseOrderNumber.Trim());

        if (purchaseOrder == null)
            return NotFound(new
            {
                Success = false,
                Message = "Purchase order not found."
            });

        return Ok(new
        {
            Success = true,
            Message = "Purchase order loaded successfully.",
            Data = purchaseOrder
        });
    }

}
