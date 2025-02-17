using System.Runtime.CompilerServices;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _context;

        public AuthenticationController(IUserRepository context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost]
        public IActionResult Authenticate(UserLoginDto loginData)
        {
            return Ok();
        }

        private User ValidateCredentials(UserLoginDto loginData)
        {
            var user = _context.GetUser(loginData.Username);
            return 
        }

    }
}
