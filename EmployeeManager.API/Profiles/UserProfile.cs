using AutoMapper;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
