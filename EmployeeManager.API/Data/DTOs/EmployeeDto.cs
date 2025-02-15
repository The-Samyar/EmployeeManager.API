using DataLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.API.Data.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public DateTime HiringDate { get; set; }
        public int UserId { get; set; }
        public Position Position { get; set; }

    }
}
