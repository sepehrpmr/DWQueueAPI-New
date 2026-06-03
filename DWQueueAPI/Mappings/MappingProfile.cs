using AutoMapper;
using DWQueueAPI.Data.Entities;
using DWQueueAPI.DTOs.DepartmenDTOs;
using DWQueueAPI.DTOs.EmployeeDTOs;
using DWQueueAPI.DTOs.ProjectDTOs;
using DWQueueAPI.DTOs.EmployeeLeavesDTOs;

namespace DWQueueAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employees, EmployeeResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeID));

            CreateMap<CreateEmployeeDto, Employees>();
            CreateMap<UpdateEmployeeDto, Employees>();

            CreateMap<Departments, DepartmentResponseDto>();

            CreateMap<CreateDepartmentDto, Departments>();
            CreateMap<UpdateDepartmetDto, Departments>();


            CreateMap<Projects, ProjectResponseDto>();

            CreateMap<CreateProjectDto, Projects>();
            CreateMap<UpdateProjectDto, Projects>();




            CreateMap<CreateEmployeeLeavesDto, EmployeeLeaves>();
            CreateMap<UpdateEmployeeLeavesDto, EmployeeLeaves>();
            CreateMap<EmployeeLeaves, EmployeeLeavesResponseDto >()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name)); 


        }
    }
}
