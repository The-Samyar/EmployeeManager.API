using AutoMapper;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/users/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly EmployeeDbContext _context;
        public UserController(IMapper mapper, IUserRepository userRepository, EmployeeDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username cannot be empty.");
            }

            var user = _userRepository.GetUser(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

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
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Username and Password are required.");
            }

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
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username cannot be empty.");
            }

            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            var oldUser = _userRepository.GetUser(username);
            if (oldUser == null)
            {
                return NotFound("User not found.");
            }

            _mapper.Map(user, oldUser);
            _userRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username cannot be empty.");
            }

            var user = _userRepository.GetUser(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _userRepository.DeleteUser(user);
            _userRepository.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("add-reward/{username}")]
        public IActionResult AddReward(string username, RewardDto rewardDto)
        {
            if (!_userRepository.UserExists(username, null))
            {
                return BadRequest("User not found");
            }

            var user = _userRepository.GetUser(username);

            _context.Rewards.Add(new()
            {
                User = user,
                UserId = user.Id,
                Count = rewardDto.Count,
                Message = rewardDto.Message,
                Rate = user.Position.RewardRate
            });
            _userRepository.SaveChanges();



            return Ok();
        }

    }
}
