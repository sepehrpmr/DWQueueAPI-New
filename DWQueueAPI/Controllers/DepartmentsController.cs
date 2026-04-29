using DWQueueAPI.Data.Entities;

using DWQueueAPI.DTOs.DepartmenDTOs;

using DWQueueAPI.Services;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;



namespace DWQueueAPI.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class DepartmentsController : ControllerBase

    {

        private readonly DepartmentService _departmentService;





        public DepartmentsController(DepartmentService departmentService)

        {

            _departmentService = departmentService;

        }





        [HttpGet]

        public async Task<IActionResult> GetAll()

        {

            try

            {

                var departments = await _departmentService.GetAllDepartmentsAsync();

                var response = departments.Select(d => new DepartmentResponseDto

                {

                    DepartmentID = d.DepartmentID,

                    DepartmentName = d.DepartmentName

                }).ToList();

                return Ok(response);

            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }



        }



        [HttpGet("{id}")]

        public async Task<IActionResult> GetByID(int id)

        {

            try

            {

                var department = await _departmentService.GetDepartmentByIDAsync(id);

                if (department == null)

                {

                    return NotFound("Department not found");

                }

                var response = new DepartmentResponseDto

                {

                    DepartmentID = department.DepartmentID,

                    DepartmentName = department.DepartmentName

                };

                return Ok(response);

            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }





        }





        [HttpPost]

        public async Task<IActionResult> Create(CreateDepartmentDto createDepartment)

        {

            try

            {

                Departments department = new Departments

                {

                    DepartmentName = createDepartment.DepartmentName

                };

                await _departmentService.AddDepartmentAsync(department);

                return Ok("Department created successfully");

            }

            catch (Exception ex)

            {

                var innerError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(innerError);

            }

        }



        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update(UpdateDepartmetDto updateDepartment)
        {

            try
            {

                if (updateDepartment.DepartmentID == null)
                    return BadRequest("ID in URL does not match ID in body.");
               

                Departments department = new Departments
                {

                    DepartmentID = updateDepartment.DepartmentID,

                    DepartmentName = updateDepartment.DepartmentName

                };

                await _departmentService.UpdateDepartmentAsync(department);

                return Ok("Department updated successfully");

            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }

        }



        [HttpDelete("{id}")]



        public async Task<IActionResult> Delete(int id)

        {

            try

            {

                await _departmentService.DeleteDepartmentAsync(id);

                return Ok("Department deleted successfully");

            }

            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }

        }



    }



}