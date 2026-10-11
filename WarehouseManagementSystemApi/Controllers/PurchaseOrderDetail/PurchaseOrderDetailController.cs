using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.PurchaseOrderDetail;

[Route("api/[controller]")]
[ApiController]
public class PurchaseOrderDetailController : ControllerBase
{
    private readonly IPurchaseOrderDetailService _service;

    public PurchaseOrderDetailController(
        IPurchaseOrderDetailService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PurchaseOrderDetailCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] PurchaseOrderDetailListDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "PurchaseOrderDetail updated successfully."
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "PurchaseOrderDetail deleted successfully."
        });
    }
}
