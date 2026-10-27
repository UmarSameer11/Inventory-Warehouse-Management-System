using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.DispatchDetail;

[Route("api/[controller]")]
[ApiController]
public class DispatchDetailController : ControllerBase
{
    private readonly IDispatchDetailService _service;

    public DispatchDetailController(IDispatchDetailService service)
    {
        _service = service;
    }

    // POST: api/DispatchDetail
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] DispatchDetailCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/DispatchDetail
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/DispatchDetail/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/DispatchDetail/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] DispatchDetailListDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Dispatch detail updated successfully."
        });
    }

    // DELETE: api/DispatchDetail/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Dispatch detail deleted successfully."
        });
    }
}
