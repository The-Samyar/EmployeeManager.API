using DataLayer.Models;

namespace EmployeeManager.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {

        bool EmployeesExists(int id);
        Employee? GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployees();

    }
}
