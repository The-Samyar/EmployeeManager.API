using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Data.Dtos
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime HiringDate { get; set; }
        public int PositionId { get; set; }



    }
}
