using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.GoodsReceipt;

[Route("api/[controller]")]
[ApiController]
public class GoodsReceiptController : ControllerBase
{
    private readonly IGoodsReceiptService _service;

    public GoodsReceiptController(IGoodsReceiptService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] GoodsReceiptCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "GoodsReceipt deleted successfully."
        });
    }

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