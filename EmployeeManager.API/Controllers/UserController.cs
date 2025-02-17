using AutoMapper;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        [HttpGet]
        [Route("get/{username}/")]
        public IActionResult GetUser(string username)
        {
            var user = _userRepository.GetUser(username);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser( UserCreateDto user )
        {
            var mappedUser = _mapper.Map<User>(user);
            _userRepository.CreateUser(mappedUser);
            _userRepository.SaveChanges();

            //return CreatedAtAction(nameof(PointOfInterest), new {cityId = cityId, poiId = mappedPointOfInterest.Id}, mappedPointOfInterest);
            return Ok();
        }

        [HttpPut]
        [Route("update/{username}")]
        public IActionResult PutUser( string username, UserDto user )
        {
            var oldUser = _userRepository.GetUser(user.Username);
            _mapper.Map(user, oldUser );
            _userRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            var user = _userRepository.GetUser(username);
            _userRepository.DeleteUser(user);
            _userRepository.SaveChanges();

            return Ok();
        }
    }
}
