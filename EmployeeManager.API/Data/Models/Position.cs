using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.API.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double RewardRate { get; set; }
    }
}
