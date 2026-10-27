using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.StockAdjustmentDetail;

[Route("api/[controller]")]
[ApiController]
public class StockAdjustmentDetailController : ControllerBase
{
    private readonly IStockAdjustmentDetailService _service;

    public StockAdjustmentDetailController(
        IStockAdjustmentDetailService service)
    {
        _service = service;
    }

    // POST: api/StockAdjustmentDetail
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] StockAdjustmentDetailCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/StockAdjustmentDetail
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/StockAdjustmentDetail/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // DELETE: api/StockAdjustmentDetail/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Stock adjustment detail deleted successfully."
        });
    }
}