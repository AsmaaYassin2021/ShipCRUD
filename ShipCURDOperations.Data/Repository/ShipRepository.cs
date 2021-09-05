using ShipCURDOperations.Common.Interfaces;
using ShipCURDOperations.Data.Interfaces;
using ShipCURDOperations.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ShipCURDOperations.Data.Repository
{
    public class ShipRepository : IShipRepository
    {
        private readonly MemoryDBContext context;
        public ShipRepository(MemoryDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<IShip>> GetShips()
        {
            return await context.Ships.ToListAsync();
        }
        public async Task<IShip> GetShipByCode(string Code)
        {
            return await context.Ships.FirstOrDefaultAsync(i => i.Code == Code);
        }
        public async Task<bool> AddShip(IShip ship)
        {

            var isSuccessed = false;
            ship.Code.Trim();
            context.Ships.Add(new Ship(ship));
            int returnNumber = await context.SaveChangesAsync();
            if (returnNumber == 1)
                isSuccessed = true;
            return isSuccessed;
        }
        public async Task<bool> UpdateShip(IShip newShip)
        {
            var isSuccessed = false;
            var oldShip = context.Ships.SingleOrDefaultAsync(i => i.Code == newShip.Code);
            if (oldShip == null)
                return isSuccessed;

            oldShip.Result.UpdateProperties(newShip);
            int returnNumber = await context.SaveChangesAsync();

            if (returnNumber == 1)
                isSuccessed = true;

            return isSuccessed;
        }
        public async Task<bool> DeleteShip(string Code)
        {
            var isSuccessed = false;

            var deletedShip = context.Ships.SingleOrDefaultAsync(i => i.Code == Code).Result;
            if (deletedShip == null)
                return isSuccessed;

            context.Remove(deletedShip);
            int returnNumber = await context.SaveChangesAsync();

            if (returnNumber == 1)
                isSuccessed = true;
            return isSuccessed;
        }
        public async Task<bool> isUniqueCode(string code)
        {
            if (await context.Ships.FirstOrDefaultAsync(t => t.Code == code) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> isUniqueName(string name)
        {
            var ship=context.Ships.FirstOrDefaultAsync(t => t.Name == name);
            if (await context.Ships.FirstOrDefaultAsync(t => t.Name == name) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}