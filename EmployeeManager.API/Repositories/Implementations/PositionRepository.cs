using EmployeeManager.API.Contexts;
using EmployeeManager.API.Data.Models;
using EmployeeManager.API.Repositories.Interfaces;

namespace EmployeeManager.API.Repositories.Implementations
{
    public class PositionRepository : IPositionRepository
    {
        private readonly EmployeeDbContext _context;
        public PositionRepository(EmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreatePosition(Position position)
        {
            _context.Positions.Add(position);
        }

        public void DeletePosition(Position position)
        {
            _context.Positions.Remove(position);
        }

        public Position? GetPosition(int id)
        {
            return _context.Positions.FirstOrDefault(p=>p.Id.Equals(id));
        }

        public IEnumerable<Position> GetPositionsList()
        {
            return _context.Positions.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
