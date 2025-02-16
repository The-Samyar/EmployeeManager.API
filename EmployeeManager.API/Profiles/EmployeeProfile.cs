using AutoMapper;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Data.Dtos;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeManager.API.Profiles
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
