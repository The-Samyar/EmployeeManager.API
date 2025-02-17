using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.API.Data.Models
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public double Rate { get; set; }
        public string? Message { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
