using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool UserExists(string username);
        bool UserExists(string username, string password);
        User? GetUser(string username);
        ICollection<User> GetUsers();
        User CreateUser(User user);
        void DeleteUser(User user);
        bool SaveChanges();
    }
}
