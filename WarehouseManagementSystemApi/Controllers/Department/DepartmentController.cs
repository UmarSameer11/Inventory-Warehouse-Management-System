using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Employee;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateDto department)
        {
            await _service.CreateDepartmentAsync(department);

            return Ok(new
            {
                Message = "Department successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var Departments = await _service.GetDepartmentListAsync();

            return Ok(Departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var Department = await _service.GetDepartmentByIdAsync(id);

            return Ok(Department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto department)
        {
            await _service.UpdateDepartmentAsync(id, department);

            return Ok(new { Message = "Department updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _service.DeleteDepartmentAsync(id);

            return Ok(new { Message = "Department deleted successfully" });
        }
    }
}

