using EmployeeManager.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.Contexts
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "adminuser", Password = "adminpassword", IsAdmin = true },
                new User { Id = 2, FirstName = "John", LastName = "Doe", Username = "johndoe", Password = "password123", IsAdmin = false },
                new User { Id = 3, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Password = "password456", IsAdmin = false },
                new User { Id = 4, FirstName = "Alice", LastName = "Johnson", Username = "alicejohnson", Password = "password789", IsAdmin = false },
                new User { Id = 5, FirstName = "Bob", LastName = "Brown", Username = "bobbrown", Password = "password101", IsAdmin = false }
            );

            // Seed data for Position
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Title = "Software Developer", RewardRate = 50.0 },
                new Position { Id = 2, Title = "Project Manager", RewardRate = 70.0 },
                new Position { Id = 3, Title = "Data Analyst", RewardRate = 60.0 },
                new Position { Id = 4, Title = "UX Designer", RewardRate = 55.0 },
                new Position { Id = 5, Title = "System Administrator", RewardRate = 65.0 }
            );

            // Seed data for Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, HiringDate = new DateTime(2020, 1, 15), UserId = 2, PositionId = 1 },
                new Employee { Id = 2, HiringDate = new DateTime(2021, 3, 20), UserId = 3, PositionId = 2 },
                new Employee { Id = 3, HiringDate = new DateTime(2022, 5, 10), UserId = 4, PositionId = 3 },
                new Employee { Id = 4, HiringDate = new DateTime(2023, 7, 25), UserId = 5, PositionId = 4 },
                // Admin user is not an employee and has no rewards
                new Employee { Id = 5, HiringDate = new DateTime(2021, 8, 30), UserId = 1, PositionId = 5 }
            );

            // Seed data for Reward
            modelBuilder.Entity<Reward>().HasData(
                new Reward { Id = 1, Count = 5, Rate = 250.0, Message = "Excellent performance", EmployeeId = 2 },
                new Reward { Id = 2, Count = 3, Rate = 210.0, Message = "Great leadership", EmployeeId = 3 },
                new Reward { Id = 3, Count = 4, Rate = 300.0, Message = "Outstanding teamwork", EmployeeId = 4 }
            // No rewards for the admin user
            );
        }
    }
}
