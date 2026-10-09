using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Supplier;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Supplier;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _service;

    public SupplierController(ISupplierService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplierCreateDto dto)
    {
        var supplier = await _service.CreateAsync(dto);

        return Ok(supplier);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var suppliers = await _service.GetAllAsync();

        return Ok(suppliers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var supplier = await _service.GetByIdAsync(id);

        return Ok(supplier);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SupplierUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Supplier updated successfully."
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Supplier deleted successfully."
        });
    }
}
