using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShipCURDOperations.Business.Interfaces;
using ShipCURDOperations.API.Model;
using ShipCURDOperations.Common.Interfaces;

namespace ShipCURDOperations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("api/ships")]
    public class ShipController : ControllerBase
    {

        private readonly ILogger<ShipController> _logger;
        private readonly IShipService _shipService;

        public ShipController(ILogger<ShipController> logger, IShipService shipService)
        {
            _logger = logger;
            _shipService = shipService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllShips()

        {
            try
            {
                _logger.LogInformation($"Returned all ships from the memory database.");
                IEnumerable<IShip> shipList = await _shipService.GetShips();
                return Ok(Response<IEnumerable<IShip>>.Success(shipList.Select(s => new Ship(s))));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the ReadAll action: {ex}");
                return StatusCode(500, Response<string>.Fail("Error retrieving data from the memory database"));

            }
        }
        [HttpGet("getShipByCode/{code}")]
        public async Task<IActionResult> GetShipByCode(string code)

        {
            try
            {
                _logger.LogInformation($"Returned the ship by code  from the memory database:{code}");
                IShip ship = await _shipService.GetShipByCode(code);
                if (ship != null)
                {
                    return Ok(Response<IShip>.Success(ship));
                }
                else
                {
                    return BadRequest(Response<string>.Fail("The ship is not exist"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the ReadById action: {ex}");
                return StatusCode(500, Response<string>.Fail("Error retrieving data from the memory database"));

            }
        }
        [HttpPost("create")]
        [HttpPost]
        public async Task<IActionResult> CreateShip([FromBody] Ship ship)
        {

            try
            {

                _logger.LogInformation($"Created ship:{ship.Code + "," + ship.Name}");

                if (ship == null)
                {
                    return BadRequest(Response<string>.Fail("The ship is empty "));
                }
                ship.Code.Trim();
                if (!await _shipService.isUniqueCode(ship.Code) || !await _shipService.isUniqueName(ship.Name))
                {
                    return BadRequest(Response<string>.Fail("The ship name or code is repeated "));

                }
                bool isInserted = await _shipService.AddShip(ship);
                if (isInserted)
                {
                    return Ok(Response<string>.Success("The ship is created."));
                }
                else
                {
                    return BadRequest(Response<string>.Fail("Create operation failed "));

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateShip action: {ex}");

                return StatusCode(500, Response<string>.Fail("Error retrieving data from the memory database"));

            }


        }
        [HttpPut("update")]

        public async Task<IActionResult> UpdateShip([FromBody] Ship ship)
        {
            try
            {
                _logger.LogInformation($"Updated ship:{ship.Code + "," + ship.Name}");

                if (ship == null)
                {
                    return BadRequest(Response<string>.Fail("The ship is empty "));
                }
                IShip  oldShip= _shipService.GetShipByCode(ship.Code).Result;
                if (!await _shipService.isUniqueName(ship.Name)&&ship.Name!=oldShip.Name)
                {
                    return BadRequest(Response<string>.Fail("The ship name is repeated "));

                }

                bool isUpdated = await _shipService.UpdateShip(ship);
                if (isUpdated)
                {
                    return Ok(Response<string>.Success("The ship is updated"));
                }
                else
                {
                    return BadRequest(Response<string>.Fail("Update operation failed "));

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateShip action: {ex}");

                return StatusCode(500, Response<string>.Fail("Error retrieving data from the memory database"));

            }
        }

        [HttpDelete("delete/{code}")]
        public async Task<IActionResult> DeleteShip([FromRoute] string code)
        {
            try
            {
                _logger.LogInformation($"Deleted ship by code is :{code}");

                if (string.IsNullOrEmpty(code))
                {

                    return BadRequest(Response<string>.Fail("The ship code is empty "));
                }
                bool isDeleted = await _shipService.DeleteShip(code);
                if (isDeleted)
                {
                    return Ok(Response<string>.Success("The ship is deleted"));
                }
                else
                {
                    return BadRequest(Response<string>.Fail("Delete operation failed "));

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the DeleteShip action: {ex}");

                return StatusCode(500, Response<string>.Fail("Error Deleteing Ship data from the memory database"));


            }
        }
        [HttpGet("uniquecode/{code}")]
        public async Task<IActionResult> isUniqueCode([FromRoute] string code)
        {
            try
            {
                _logger.LogInformation($"Check the ship code is used before or not  :{code}");

                bool isUniqueCode = await _shipService.isUniqueCode(code);
                return Ok(Response<bool>.Success(isUniqueCode));

            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the isUniqueCode action: {ex}");

                return StatusCode(500, Response<string>.Fail("Error"));

            }
        }
        [HttpGet("uniquename/{name}")]

        public async Task<IActionResult> isUniqueName([FromRoute] string name)
        {
            try
            {
                _logger.LogInformation($"Check the ship name is used before or not  :{name}");

                bool isUniqueName = await _shipService.isUniqueName(name);
                return Ok(Response<bool>.Success(isUniqueName));
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the isUniqueName action: {ex}");
                return StatusCode(500, Response<string>.Fail("Error"));


            }
        }

        [Route("error/{code}")]
        public IActionResult Error(int code)
        {
            return new ObjectResult(Response<string>.Fail("Error"));
        }
    }
}
