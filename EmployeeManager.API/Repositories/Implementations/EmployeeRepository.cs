using DataLayer.Models;
using EmployeeManager.API.Contexts;
using EmployeeManager.API.Repositories.Interfaces;

namespace EmployeeManager.API.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.Id.Equals(id));

        }

        public Employee? GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id.Equals(id));
        }


        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

    }
}
