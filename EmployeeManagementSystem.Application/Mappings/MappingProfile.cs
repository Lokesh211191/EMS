namespace EmployeeManagementSystem.Application.Mappings;

using AutoMapper;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}
