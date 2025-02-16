using EmployeeManager.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.API.Data.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public DateTime HiringDate { get; set; }
        public int UserId { get; set; }
        public Position Position { get; set; }

    }
}
