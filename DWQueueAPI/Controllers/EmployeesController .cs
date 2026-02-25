using DWQueueAPI.Data.Entities;
using DWQueueAPI.Sevices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DWQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIDAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee دot found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Add(Employees employee)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(employee);
                return Ok("Employee added successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] Employees employee)
        {

            if (id != employee.EmployeeID)
                return BadRequest("ID mismatch");

            try
            {
                await _employeeService.UpdateEmployeeAsync(employee);
                return Ok("Employee updated successfully");
                //return Ok("Employee pdated successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok("Employee deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }
    }
}
