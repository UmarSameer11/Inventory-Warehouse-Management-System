using Microsoft.AspNetCore.Mvc;
using WarehouseManagementSystemApi.DTOs.Employee;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employee)
        {
            await _service.CreateEmployeeAsync(employee);

            return Ok(new
            {
                Message = "Employee successfully added"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _service.GetEmployeeListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _service.GetEmployeeByIdAsync(id);

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employee)
        {
            await _service.UpdateEmployeeAsync(id, employee);

            return Ok(new { Message = "Employee updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _service.DeleteEmployeeAsync(id);

            return Ok(new { Message = "Employee deleted successfully" });
        }
    }
}
