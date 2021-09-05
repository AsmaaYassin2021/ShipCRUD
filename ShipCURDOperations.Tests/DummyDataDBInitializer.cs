using System.Collections.Generic;
using ShipCURDOperations.Common.Interfaces;

using ShipCURDOperations.API.Model;
namespace ShipCURDOperations.Tests

{
    public static class DummyDataDBInitializer
    {
        

        public static List<Ship> returnDummyData()
        {

            List<Ship> shipList = new List<Ship>();

            for (int i = 1; i <= 9; i++)
            {
                Ship c = new Ship();
                c.Code = "AAAA-1111-A" + i.ToString(); ;
                c.Name = "Ship" + i.ToString();
                c.ShipLength = 40;
                c.ShipWidth = 100;
               shipList.Add(c);

            }
          return shipList;

        }
    }
}