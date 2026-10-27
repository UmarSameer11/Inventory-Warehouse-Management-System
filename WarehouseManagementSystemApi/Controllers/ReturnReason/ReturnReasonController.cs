using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.ReturnReason;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.ReturnReason;

[Route("api/[controller]")]
[ApiController]
public class ReturnReasonController : ControllerBase
{
    private readonly IReturnReasonService _service;

    public ReturnReasonController(IReturnReasonService service)
    {
        _service = service;
    }

    // POST: api/ReturnReason
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ReturnReasonCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    // GET: api/ReturnReason
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // GET: api/ReturnReason/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // PUT: api/ReturnReason/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ReturnReasonUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Return reason updated successfully."
        });
    }

    // DELETE: api/ReturnReason/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Return reason deleted successfully."
        });
    }
}