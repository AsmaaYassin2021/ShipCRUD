namespace ShipCURDOperations.Common.Interfaces
{
    public interface IShip
    {
       
         string Name { get; set; }
         string Code { get; set; }
         decimal ShipWidth { get; set; }
         decimal ShipLength { get; set; }

    }
}