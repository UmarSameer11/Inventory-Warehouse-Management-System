using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Customer;
using WarehouseManagementSystemApi.Services.Interfaces;
namespace WarehouseManagementSystemApi.Controllers.Customer;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;
    public CustomerController(ICustomerService service) => _service = service;
    [HttpPost] public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto) => Ok(await _service.CreateAsync(dto));
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto dto) { await _service.UpdateAsync(id, dto); return Ok(new { Message = "Customer updated successfully." }); }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return Ok(new { Message = "Customer deleted successfully." }); }
}
