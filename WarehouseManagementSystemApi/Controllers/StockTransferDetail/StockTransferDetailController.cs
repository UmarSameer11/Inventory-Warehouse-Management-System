using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.StockTransferDetail;

[Route("api/[controller]")]
[ApiController]
public class StockTransferDetailController : ControllerBase
{
    private readonly IStockTransferDetailService _service;

    public StockTransferDetailController(
        IStockTransferDetailService service)
    {
        _service = service;
    }

    // POST: api/StockTransferDetail
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] StockTransferDetailCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/StockTransferDetail
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/StockTransferDetail/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/StockTransferDetail/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] StockTransferDetailListDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Stock transfer detail updated successfully."
        });
    }

    // DELETE: api/StockTransferDetail/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Stock transfer detail deleted successfully."
        });
    }
}