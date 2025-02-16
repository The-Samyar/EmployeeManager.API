using EmployeeManager.API.Data.Models;

namespace EmployeeManager.API.Repositories.Interfaces
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetPositionsList();
    }
}
