using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Data.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public PositionDto PositionDto { get; set; }
    }
}
