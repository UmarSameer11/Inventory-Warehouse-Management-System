using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.SalesReturnDetail;
[Route("api/[controller]")]
[ApiController]
public class SalesReturnDetailController : ControllerBase
{
    private readonly ISalesReturnDetailService _service;
    public SalesReturnDetailController(ISalesReturnDetailService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] SalesReturnDetailCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "SalesReturnDetail deleted successfully." }); }
}
