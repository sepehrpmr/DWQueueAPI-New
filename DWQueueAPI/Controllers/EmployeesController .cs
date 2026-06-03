using DWQueueAPI.Data.Entities;
using DWQueueAPI.DTOs.EmployeeDTOs;
using DWQueueAPI.Mappings;
using DWQueueAPI.Sevices;
using Microsoft.AspNetCore.Mvc;
using DWQueueAPI.Mappings;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DWQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly EmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(EmployeeService employeeService , IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>x`
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{   
                // ۱. گرفتن لیست اصلی از سرویس
                var employees = await _employeeService.GetAllEmployeesAsync();
                var response = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);

                return Ok(response);
            //}
            //catch (Exception ex)
            //{

               // return BadRequest(ex.Message);
            //}
            
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            //try
            //{
                var employee = await _employeeService.GetEmployeeByIDAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                var response = _mapper.Map<EmployeeResponseDto>(employee);


                //var response = new EmployeeResponseDto
                //{
                //    Id = employee.EmployeeID,
                //    Name = employee.Name,
                //    Position = employee.Position
                //};


                return Ok(response);
            //}
            //catch (Exception ex)
            //{
             //   return StatusCode(500, $"Internal server error: {ex.Message}");

            //}
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEmployeeDto createDto)
        {
            //try
            //{

                var employee = _mapper.Map<Employees>(createDto);
                //Employees employee = new Employees
                //{
                //    Name = createDto.Name,
                //    Position = createDto.Position,
                //    HireDate = createDto.HireDate,
                //    DepartmentID = createDto.DepartmentID
                //};
                await _employeeService.AddEmployeeAsync(employee);
                return Ok("Employee added successfully");
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(500, $"Internal server error: {ex.Message}");
            //}

            //catch (Exception ex)
            //{
                // این خط باعث می‌شه ارور اصلی دیتابیس رو بخونیم
                //var realError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
               // return StatusCode(500, $"Database Error: {realError}");
            //}

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest("آی‌دی ارسال شده با مشخصات کارمند همخوانی ندارد.");
            }

            //try
            //{
                var employee = _mapper.Map<Employees>(updateDto);
                await _employeeService.UpdateEmployeeAsync(employee);

                return Ok("اطلاعات کارمند با موفقیت بروزرسانی شد.");
            //}
            //catch (Exception ex)
            //{
             //   return StatusCode(500, $"خطای سیستمی: {ex.Message}");
            //}
        }
        //[HttpPut(nameof(Update))]
        //public async Task<IActionResult> Update([FromBody] UpdateEmployeeDto updateEmployee)
        //{

        //    Employees employees = new Employees
        //    {
        //        EmployeeID = updateEmployee.ID,
        //        Name = updateEmployee.Name,
        //        Position = updateEmployee.Position,
        //        HireDate = updateEmployee.HireDate,
        //        DepartmentID = updateEmployee.DepartmentID
        //    };

        //    try
        //    {
        //        await _employeeService.UpdateEmployeeAsync(employees);
        //        return Ok("Employee updated successfully");
        //        //return Ok("Employee pdated successfully");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }

        //}

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //try
            //{
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok("Employee deleted successfully");
            //}
            //catch (Exception ex)
            //{

                //return StatusCode(500, $"Internal server error: {ex.Message}");
            //}
           
        }
    }
}
