using ShipCURDOperations.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ShipCURDOperations.Data.Interfaces
{
    public interface IShipRepository
    {
        Task<IEnumerable<IShip>> GetShips();
        Task<IShip> GetShipByCode(string code);
        Task<bool> AddShip(IShip ship);
        Task<bool> UpdateShip( IShip newShip);
        Task<bool> DeleteShip(string code);
        Task<bool> isUniqueCode(string code);
        Task<bool> isUniqueName(string name);

    }
}