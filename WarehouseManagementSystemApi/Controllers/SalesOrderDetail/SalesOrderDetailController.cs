using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.SalesOrderDetail;

[Route("api/[controller]")]
[ApiController]
public class SalesOrderDetailController : ControllerBase
{
    private readonly ISalesOrderDetailService _service;

    public SalesOrderDetailController(
        ISalesOrderDetailService service)
    {
        _service = service;
    }

    // POST: api/SalesOrderDetail
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SalesOrderDetailCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/SalesOrderDetail
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/SalesOrderDetail/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/SalesOrderDetail/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] SalesOrderDetailListDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Sales order detail updated successfully."
        });
    }

    // DELETE: api/SalesOrderDetail/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Sales order detail deleted successfully."
        });
    }
}