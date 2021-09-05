using ShipCURDOperations.Business.Interfaces;
using ShipCURDOperations.Business.Models;
using ShipCURDOperations.Data.Interfaces;
using ShipCURDOperations.Data.Repository;
using ShipCURDOperations.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipCURDOperations.Business
{
    public class ShipService : IShipService
    {
        private readonly IShipRepository shipRepository;
        public ShipService(IShipRepository shipRepository)
        {
            this.shipRepository = shipRepository;
        }

        public async Task< IEnumerable<IShip>> GetShips()
        {
            return await shipRepository.GetShips();
        }
        public  async Task<bool> AddShip(IShip ship)
        {
            return await shipRepository.AddShip(ship);
        }
        public  async Task<bool> UpdateShip( IShip newShip)
        {
            return await shipRepository.UpdateShip( newShip);
        }
        public  async Task<bool> DeleteShip(string code)
        {
            return await shipRepository.DeleteShip(code);
        }
         public  async Task<IShip> GetShipByCode(string code)
        {
             return await shipRepository.GetShipByCode(code);
        }

       public async Task<bool> isUniqueCode(string code)
       {
           return await shipRepository.isUniqueCode(code);
       }
      public  async Task<bool> isUniqueName(string name)
      {
           return await shipRepository.isUniqueName(name);
      }
    }
}