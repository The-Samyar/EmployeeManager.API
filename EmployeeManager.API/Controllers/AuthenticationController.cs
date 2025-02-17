using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserRepository context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public IActionResult Authenticate(UserLoginDto loginData)
        {
            if (!IsValidUser(loginData))
            {
                return Unauthorized();
            }
            var user = _context.GetUser(loginData.Username);
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new List<Claim>();
            tokenClaims.Add(new Claim("Id", user.Id.ToString()));
            tokenClaims.Add(new Claim("Username", user.Username.ToString()));
            tokenClaims.Add(new Claim("FullName", user.FirstName.ToString() + user.LastName.ToString()));

            var tokenConfig = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                tokenClaims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenConfig);

            return Ok(token);
        }

        private bool IsValidUser(UserLoginDto loginData)
        {
            return (_context.UserExists(loginData.Username, loginData.Password));
        }

    }
}
