
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.GoodsReceiptDetail;

[Route("api/[controller]")]
[ApiController]
public class GoodsReceiptDetailController : ControllerBase
{
    private readonly IGoodsReceiptDetailService _service;

    public GoodsReceiptDetailController(
        IGoodsReceiptDetailService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] GoodsReceiptDetailCreateDto dto)
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
            Message = "GoodsReceiptDetail deleted successfully."
        });
    }
}