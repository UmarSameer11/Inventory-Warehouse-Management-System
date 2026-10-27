using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.StockAdjustment;

[Route("api/[controller]")]
[ApiController]
public class StockAdjustmentController : ControllerBase
{
    private readonly IStockAdjustmentService _service;

    public StockAdjustmentController(
        IStockAdjustmentService service)
    {
        _service = service;
    }

    // POST: api/StockAdjustment
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] StockAdjustmentCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/StockAdjustment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/StockAdjustment/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // DELETE: api/StockAdjustment/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Stock adjustment deleted successfully."
        });
    }
}