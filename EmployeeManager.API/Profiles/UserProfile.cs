using AutoMapper;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
              .ForMember(
                    dest => dest.PositionDto,
                    opt => opt.MapFrom(src => src.Position)).ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<RewardDto, Reward>();
            CreateMap<Reward, RewardDto>();
        }
    }
}
