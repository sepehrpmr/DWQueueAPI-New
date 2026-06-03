using AutoMapper;
using DWQueueAPI.Data.Entities;
using DWQueueAPI.DTOs.ProjectDTOs;
using DWQueueAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DWQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly IMapper _mapper;


        public ProjectsController(ProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{
                var projects = await _projectService.GetAllProjectsAsync();
                var response = _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
                //var response = projects.Select(p => new ProjectResponseDto
                //{
                //    ProjectID = p.ProjectID,
                //    ProjectName = p.ProjectName,
                //    StartDate = (DateTime)p.StartDate,
                //    EndDate = (DateTime)p.EndDate,
                //    Budget = (decimal)p.Budget
                //}).ToList();
                return Ok(response);
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }


        // GET api/<ValuesController>/5

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            //try
            //{
                var project = await _projectService.GetProjectByIDAsync(id);
                if (project == null)
                {
                    return NotFound("Project not found");
                }
                var response = _mapper.Map<ProjectResponseDto>(project);
                //var response = new ProjectResponseDto
                //{
                //    ProjectID = project.ProjectID,
                //    ProjectName = project.ProjectName,
                //    StartDate = (DateTime)project.StartDate,
                //    EndDate = (DateTime)project.EndDate,
                //    Budget = (decimal)project.Budget
                //};
                return Ok(response);
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}

        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto projectCreateDto)
        {
            //try
            //{
                var project = _mapper.Map<Projects>(projectCreateDto);
                //var project = new Projects
                //{
                //    ProjectName = projectCreateDto.ProjectName,
                //    StartDate = projectCreateDto.StartDate,
                //    EndDate = projectCreateDto.EndDate,
                //    Budget = (decimal)projectCreateDto.Budget
                //};
                await _projectService.AddProjectAsync(project);
                return Ok("Project created successfully");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }



        [HttpPut(nameof(Update))]

        public async Task<IActionResult> Update(UpdateProjectDto projectUpdateDto)
        {
            //try
            //{
                var project = _mapper.Map<Projects>(projectUpdateDto);
                //var project = new Projects
                //{
                //    ProjectID = projectUpdateDto.ProjectID,
                //    ProjectName = projectUpdateDto.ProjectName,
                //    StartDate = projectUpdateDto.StartDate,
                //    EndDate = projectUpdateDto.EndDate,
                //    Budget = (decimal)projectUpdateDto.Budget
                //};
                await _projectService.UpdateProjectAsync(project);
                return Ok("Project updated successfully");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //try
            //{
                await _projectService.DeleteProjectAsync(id);
                return Ok("Project deleted successfully");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(ex.Message);
            //}


        }
    }
}
