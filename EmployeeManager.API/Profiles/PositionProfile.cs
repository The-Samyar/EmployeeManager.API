using AutoMapper;
using EmployeeManager.API.Data.DTOs;
using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Profiles
{
    public class PositionProfile: Profile
    {
        public PositionProfile()
        {
            CreateMap<PositionCreateDto, Position>();
            CreateMap<PositionUpdateDto, Position>();
        }
    }
}
