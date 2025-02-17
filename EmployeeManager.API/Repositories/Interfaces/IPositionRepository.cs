using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetPositionsList();
        Position? GetPosition(int id);
        void CreatePosition(Position position);
        void DeletePosition(Position position);
        bool SaveChanges();
    }
}
