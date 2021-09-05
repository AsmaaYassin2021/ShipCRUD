using ShipCURDOperations.Common.Interfaces;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ShipCURDOperations.API.Model
{
    [DataContract(Name = "ship")]
    public class Ship : IShip
    {
        [DataMember(Name = "name")]
        [Required]
        [StringLength(100, MinimumLength = 2)]

        public string Name { get; set; }
        [DataMember(Name = "code")]
        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string Code { get; set; }

        [DataMember(Name = "shipWidth")]
        [Required]
        [Range(15, 70, ErrorMessage = "The ship Width must between 15 and 70.")]

        public decimal ShipWidth { get; set; }

        [DataMember(Name = "shipLength")]
        [Required]
        [Range(75, 400, ErrorMessage = "The ship Length must between 75 and 400.")]
        public decimal ShipLength { get; set; }

        public Ship(IShip ship)
        {
            Name = ship.Name;
            Code = ship.Code;
            ShipWidth = ship.ShipWidth;
            ShipLength = ship.ShipLength;
        }
        public Ship()
        {

        }

    }
}
