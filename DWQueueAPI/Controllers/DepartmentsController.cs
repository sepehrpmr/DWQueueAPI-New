using System.Threading;
using AutoMapper;
using DWQueueAPI.Data.Entities;
using DWQueueAPI.DTOs.DepartmenDTOs;
using DWQueueAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;



namespace DWQueueAPI.Controllers

{
    [Route("api/[controller]")]

    [ApiController]

    public class DepartmentsController : ControllerBase

    {
        private readonly DepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(DepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            //try
            //{
                var departments = await _departmentService.GetAllDepartmentsAsync(cancellationToken);
                var response = _mapper.Map<IEnumerable<DepartmentResponseDto>>(departments);

                //var response = departments.Select(d => new DepartmentResponseDto

                //{

                //    DepartmentID = d.DepartmentID,

                //    DepartmentName = d.DepartmentName

                //}).ToList();

                return Ok(response);
            //}
            //catch (Exception ex)
            //{
              //  return BadRequest(ex.Message);
            //}
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetByID(int id , CancellationToken cancellationToken)
        {
            //try
            //{
                var department = await _departmentService.GetDepartmentByIDAsync(id , cancellationToken);
                var response = _mapper.Map<DepartmentResponseDto>(department);

                if (department == null)
                    return NotFound("Department not found");
                
                //var response = new DepartmentResponseDto
                //{
                //    DepartmentID = department.DepartmentID,
                //    DepartmentName = department.DepartmentName
                //};

                return Ok(response);
            //}

            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }





        [HttpPost]

        public async Task<IActionResult> Create(CreateDepartmentDto createDepartment , CancellationToken cancellationToken)
        {
            //try
            //{
                var department = _mapper.Map<Departments>(createDepartment);
                //Departments department = new Departments
                //{
                //    DepartmentName = createDepartment.DepartmentName
                //};

                await _departmentService.AddDepartmentAsync(department, cancellationToken);
                return Ok("Department created successfully");
            //}
            //catch (Exception ex)
            //{
                //var innerError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                //return BadRequest(innerError);

            //}

        }



        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(UpdateDepartmetDto updateDepartment, CancellationToken cancellationToken)
        {
            //try
            //{
                if (updateDepartment.DepartmentID == null)
                    return BadRequest("ID in URL does not match ID in body.");
                //Departments department = new Departments
                //{
                //    DepartmentID = updateDepartment.DepartmentID,
                //    DepartmentName = updateDepartment.DepartmentName
                //};

                var department = _mapper.Map<Departments>(updateDepartment);
                await _departmentService.UpdateDepartmentAsync(department, cancellationToken);
                return Ok("Department updated successfully");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            //try
            //{
                await _departmentService.DeleteDepartmentAsync(id, cancellationToken);
                return Ok("Department deleted successfully");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }
    }



}