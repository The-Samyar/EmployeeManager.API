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
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Position
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Title = "Software Developer", RewardRate = 50.0 },
                new Position { Id = 2, Title = "Project Manager", RewardRate = 70.0 },
                new Position { Id = 3, Title = "Data Analyst", RewardRate = 60.0 },
                new Position { Id = 4, Title = "UX Designer", RewardRate = 55.0 },
                new Position { Id = 5, Title = "System Administrator", RewardRate = 65.0 }
            );

            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", IsAdmin = true, PositionId = 5 }, // Assuming admin is a System Administrator
                new User { Id = 2, FirstName = "John", LastName = "Doe", Username = "johndoe", Password = "password", IsAdmin = false, PositionId = 1 }, // Software Developer
                new User { Id = 3, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Password = "password", IsAdmin = false, PositionId = 2 }, // Project Manager
                new User { Id = 4, FirstName = "Alice", LastName = "Johnson", Username = "alicejohnson", Password = "password", IsAdmin = false, PositionId = 3 }, // Data Analyst
                new User { Id = 5, FirstName = "Bob", LastName = "Brown", Username = "bobbrown", Password = "password", IsAdmin = false, PositionId = 4 } // UX Designer
            );

            // Seed data for Reward
            modelBuilder.Entity<Reward>().HasData(
                new Reward { Id = 1, Count = 5, Rate = 250.0, Message = "Excellent performance", UserId = 2 },
                new Reward { Id = 2, Count = 3, Rate = 210.0, Message = "Great leadership", UserId = 3 },
                new Reward { Id = 3, Count = 4, Rate = 300.0, Message = "Outstanding teamwork", UserId = 4 }
            );
        }
    }
}
