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
        public IEnumerable<Position> GetPositionsList()
        {
            return _context.Positions.ToList();
        }
    }
}
