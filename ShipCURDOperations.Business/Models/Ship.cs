using ShipCURDOperations.Common.Interfaces;

namespace ShipCURDOperations.Business.Models
{
    public class Ship : IShip
    {

      
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal ShipWidth { get; set; }
        public decimal ShipLength { get; set; }

        public Ship(IShip ship)
        {
            Name = ship.Name;
            Code = ship.Code;
            ShipWidth = ship.ShipWidth;
            ShipLength = ship.ShipLength;
        }
    }
}