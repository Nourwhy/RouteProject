using AutoMapper;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;

namespace RouteProject.PL.Mapping
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {


            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
        }
    }
}
