using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Designation;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Designation
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _service;

        public DesignationController(IDesignationService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDesignation([FromBody] DesignationCreateDto designation)
        {
            await _service.CreateDesignationAsync(designation);

            return Ok(new
            {
                Message = "Designation successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDesignations()
        {
            var designations = await _service.GetDesignationListAsync();

            return Ok(designations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            var designation = await _service.GetDesignationByIdAsync(id);

            return Ok(designation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DesignationUpdateDto designation)
        {
            await _service.UpdateDesignationAsync(id, designation);

            return Ok(new { Message = "Designation updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            await _service.DeleteDesignationAsync(id);

            return Ok(new { Message = "Designation deleted successfully" });
        }
    }
}
