using ManageEmployeApp.Modle;
using ManageEmployeApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        public EmployeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeServices.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById()
        {
            var employees = await _employeeServices.GetAllEmployeesAsync();
            return Ok(employees);
        }

       
        [HttpGet("filter")]
        public async Task<IActionResult> GetEmployeesByFilter(
            [FromQuery] string? designation = null,
            [FromQuery] string? name = null)
        {
            var employees = await _employeeServices.GetAllEmployeesAsync();

            if (!string.IsNullOrWhiteSpace(designation))
            {
                employees = employees
                    .Where(e => !string.IsNullOrWhiteSpace(e.Designation) &&
                                e.Designation.Equals(designation, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                employees = employees
                    .Where(e => !string.IsNullOrWhiteSpace(e.Name) &&
                                e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var addedEmployee = await _employeeServices.AddEmployeeAsync(employee);
            return Ok(addedEmployee);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var updatedEmployee = await _employeeServices.UpdateEmployeeAsync(employee);
            return Ok(updatedEmployee);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeServices.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
