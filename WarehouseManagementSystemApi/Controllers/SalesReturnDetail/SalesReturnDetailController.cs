using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.SalesReturnDetail;

[Route("api/[controller]")]
[ApiController]
public class SalesReturnDetailController : ControllerBase
{
    private readonly ISalesReturnDetailService _service;

    public SalesReturnDetailController(
        ISalesReturnDetailService service)
    {
        _service = service;
    }

    // POST: api/SalesReturnDetail
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SalesReturnDetailCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/SalesReturnDetail
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/SalesReturnDetail/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // DELETE: api/SalesReturnDetail/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Sales return detail deleted successfully."
        });
    }
}