using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserRepository context, IConfiguration configuration)
        {
            _userRepository = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public IActionResult Authenticate(UserLoginDto loginData)
        {
            if (!IsValidUser(loginData))
            {
                return Unauthorized();
            }
            var user = _userRepository.GetUser(loginData.Username);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

         

            var claims = new List<Claim>
            {
                         new Claim(ClaimTypes.NameIdentifier,user.Username.ToString()),
                         new Claim(ClaimTypes.Role,user.FirstName.ToString() + user.LastName.ToString())
                        };

        

            var token1 = new JwtSecurityToken(
                         issuer: _configuration["Authentication:Issuer"],
                         audience: _configuration["Authentication:Audience"],
                         claims: claims,
                         expires: DateTime.Now.AddDays(2),
                         signingCredentials: signingCredentials
                );


            var token = new JwtSecurityTokenHandler().WriteToken(token1);

            return Ok(token);
        }

        private bool IsValidUser(UserLoginDto loginData)
        {
            return (_userRepository.UserExists(loginData.Username, loginData.Password));
        }

    }
}
