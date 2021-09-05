using ShipCURDOperations.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShipCURDOperations.Data.Models
{
    public class Ship : IShip
    {
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Key]
        [Column(TypeName = "varchar(12)")]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "decimal(3, 2)")]

        public decimal ShipWidth { get; set; }
        [Required]
        [Column(TypeName = "decimal(3, 2)")]

        public decimal ShipLength { get; set; }

        public Ship(IShip ship)
        {
            Name = ship.Name;
            Code = ship.Code;
            ShipWidth = ship.ShipWidth;
            ShipLength = ship.ShipLength;
        }
        public void UpdateProperties(IShip currentShip)
        {

            Name = currentShip.Name;
            ShipLength = currentShip.ShipLength;
            ShipWidth = currentShip.ShipWidth;

        }
        public Ship()
        {

        }

    }
}