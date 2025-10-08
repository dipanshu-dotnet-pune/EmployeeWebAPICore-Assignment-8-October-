using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using EmployeeApi.Data;
using System.Linq;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(EmployeeData.Employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById([FromRoute] int id)
        {
            var emp = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound("Employee not found");
            return Ok(emp);
        }

        
        [HttpGet("bydept")]
        public IActionResult GetEmployeesByDept([FromQuery] string department)
        {
            var emp = EmployeeData.Employees.Where(e => e.Department.ToLower() == department.ToLower()).ToList();
            return Ok(emp);
        }

        
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee emp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            EmployeeData.Employees.Add(emp);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = emp.Id }, emp);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee emp)
        {
            var existing = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            existing.Name = emp.Name;
            existing.Department = emp.Department;
            existing.MobileNo = emp.MobileNo;
            existing.Email = emp.Email;

            return Ok(existing);
        }

        [HttpPatch("{id}/email")]
        public IActionResult UpdateEmployeeEmail([FromRoute] int id, [FromBody] string newEmail)
        {
            var emp = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            emp.Email = newEmail;
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            var emp = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            EmployeeData.Employees.Remove(emp);
            return NoContent();
        }

        [HttpHead]
        public IActionResult Head()
        {
            return Ok();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
