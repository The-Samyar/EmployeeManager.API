using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Dtos;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;

namespace EmployeeManager.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public readonly EmployeeDbContext _context;
        public UserRepository(EmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }


        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }


        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User CreateUser(User user)
        {
            var res = _context.Users.Add(user);
            //return res.Entity;
            return user;
        }


        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) > 0;
        }
    }
}
