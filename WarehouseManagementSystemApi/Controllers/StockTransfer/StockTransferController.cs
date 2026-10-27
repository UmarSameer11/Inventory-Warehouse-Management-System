using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.StockTransfer;

[Route("api/[controller]")]
[ApiController]
public class StockTransferController : ControllerBase
{
    private readonly IStockTransferService _service;

    public StockTransferController(IStockTransferService service)
    {
        _service = service;
    }

    // POST: api/StockTransfer
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] StockTransferCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/StockTransfer
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/StockTransfer/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/StockTransfer/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] StockTransferUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Stock transfer updated successfully."
        });
    }

    // DELETE: api/StockTransfer/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Stock transfer deleted successfully."
        });
    }
}