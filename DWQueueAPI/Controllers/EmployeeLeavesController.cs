using AutoMapper;
using DWQueueAPI.Data.Entities;
using DWQueueAPI.DTOs.EmployeeDTOs;
using DWQueueAPI.DTOs.EmployeeLeavesDTOs;
using DWQueueAPI.Services;
using DWQueueAPI.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DWQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //for jwt token authentication 
    public class EmployeeLeavesController : ControllerBase
    {
        private readonly EmployeeLeaveService _employeeLeaveService;
        private readonly IMapper _mapper;


        public EmployeeLeavesController(EmployeeLeaveService employeeLeaveService, IMapper mapper)
        {
            _employeeLeaveService = employeeLeaveService;
            _mapper = mapper;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeLeavesResponseDto>>> GetAll()
        {
            //try
            //{   
            // 
            var employeeLeaves = await _employeeLeaveService.GetAllLeavesAsync();
            var response = _mapper.Map<IEnumerable<EmployeeLeavesResponseDto>>(employeeLeaves);
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
            var employeeLeave = await _employeeLeaveService.GetLeaveByIdAsync(id);
            if (employeeLeave == null)
            {
                return NotFound("Employee leave not found");
            }
            var response = _mapper.Map<EmployeeLeavesResponseDto>(employeeLeave);
            return Ok(response);
            //}
            //catch (Exception ex)
            //{
            // return BadRequest(ex.Message);
            //}
        }


        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeLeavesDto createDto)
        {
            //try
            //{   
            var employeeLeave = _mapper.Map<EmployeeLeaves>(createDto);
            var createdLeave = await _employeeLeaveService.AddLeaveAsync(employeeLeave);
            var response = _mapper.Map<EmployeeLeavesResponseDto>(createdLeave);
            return Ok(response);
            //}
            //catch (Exception ex)
            //{
            // return BadRequest(ex.Message);
            //}
        }



        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeLeavesDto updateDto)
        {
            //try
            //{   
            if (id != updateDto.LeaveId)
            {
                return BadRequest("ID mismatch");
            }
            var employeeLeave = _mapper.Map<EmployeeLeaves>(updateDto);
            var updatedLeave = await _employeeLeaveService.UpdateLeaveAsync(employeeLeave);
            var response = _mapper.Map<EmployeeLeavesResponseDto>(updatedLeave);
            return Ok(response);
            //}
            //catch (Exception ex)
            //{
            // return BadRequest(ex.Message);
            //}
        }



        [HttpDelete("{id}")]


        public async Task<IActionResult> Delete(int id)
        {
            //try
            //{   
            await _employeeLeaveService.DeleteLeaveAsync(id);
            return Ok("Employee deleted successfully");
            //}
            //catch (Exception ex)
            //{
            // return BadRequest(ex.Message);
            //}
        }
    }

}
