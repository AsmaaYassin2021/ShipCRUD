using ShipCURDOperations.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipCURDOperations.Business.Interfaces
{
    public interface IShipService
    {
         Task<IEnumerable<IShip>> GetShips();
        Task<bool> AddShip(IShip ship);
        Task<IShip> GetShipByCode(string code);
        Task<bool> UpdateShip( IShip newShip);
        Task<bool> DeleteShip(string code);
        Task<bool> isUniqueCode(string code);
        Task<bool> isUniqueName(string name);
    }
}