using System;
using Xunit;
using Moq;
using ShipCURDOperations.API.Controllers;
using ShipCURDOperations.Common.Interfaces;
using ShipCURDOperations.Business.Interfaces;
using ShipCURDOperations.API.Model;
using ShipCURDOperations.Data.Repository;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


using System.Threading.Tasks;

namespace ShipCURDOperations.Tests
{
    public class ShipControllerTest
    {
        #region Property  
        public Mock<IShipService> _mockShipService;
        public Mock<ShipRepository> mockShipRepository;

        private ShipController _shipController;
        private Mock<ILogger<ShipController>> _logger;

        #endregion

        public ShipControllerTest()
        {
            _logger = new Mock<ILogger<ShipController>>();

            _mockShipService = new Mock<IShipService>();
            _shipController = new ShipController(_logger.Object, _mockShipService.Object);

        }

        #region Get By Code  

        [Fact]
        public async void Task_GetShipByCode_Return_ResponseIShip()
        {
            //Arrange  
            _mockShipService.Setup(p => p.GetShipByCode("AAAA-1111-A1")).ReturnsAsync(DummyDataDBInitializer.returnDummyData()[0]);
            //Act
            IActionResult actionResult = await _shipController.GetShipByCode("AAAA-1111-A1");
            // Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<IShip> responseShip = Assert.IsType<Response<IShip>>(okObjectResult.Value);
        }
        [Fact]
        public async void Task_GetShipByCode_Return_BadRequestResult()
        {
            //Arrange  
            string code = "2222";

            //Act  
            IActionResult actionResult = await _shipController.GetShipByCode(code);

            // Assert  
            BadRequestObjectResult okObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(okObjectResult.Value);
        }

        #endregion


        #region Get All
        [Fact]
        public async void Task_GetShip_Returns_ResponseIEnumerableIShip()
        {
            //Arrange
            _mockShipService.Setup(p => p.GetShips()).ReturnsAsync(DummyDataDBInitializer.returnDummyData());
            //Act
            IActionResult actionResult = await _shipController.GetAllShips();
            //Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<IEnumerable<IShip>> responseShip = Assert.IsType<Response<IEnumerable<IShip>>>(okObjectResult.Value);


        }
        [Fact]
        public async void Task_GetShip_MatchResult()
        {
            //Arrange
            _mockShipService.Setup(p => p.GetShips()).ReturnsAsync(DummyDataDBInitializer.returnDummyData());
            //Act
            IActionResult actionResult = await _shipController.GetAllShips();
            //Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<IEnumerable<IShip>> responseShip = Assert.IsType<Response<IEnumerable<IShip>>>(okObjectResult.Value);
            List<IShip> shipList = responseShip.Data.ToList();
            Assert.Equal("AAAA-1111-A1", shipList[0].Code);
            Assert.Equal("Ship1", shipList[0].Name);
        }


        #endregion
        #region Add Ship
        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange
            var newShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 100, ShipWidth = 50 };
            _mockShipService.Setup(repo => repo.isUniqueName(newShip.Name)).ReturnsAsync(true);
            _mockShipService.Setup(repo => repo.isUniqueCode(newShip.Code)).ReturnsAsync(true);
            _mockShipService.Setup(repo => repo.AddShip(newShip)).ReturnsAsync(true);

            //Act
            IActionResult actionResult = await _shipController.CreateShip(newShip);

            // Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(okObjectResult.Value);
        }
        [Fact]
        public async void Task_Add_InvalidData_Return_BadRequest()
        {
            //Arrange
            var newShip = new Ship() { Code = "BAAA-1111-", Name = "Test Ship B", ShipLength = 100, ShipWidth = 50 };

            //Act
            IActionResult actionResult = await _shipController.CreateShip(newShip);

            // Assert  
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(badRequestObjectResult.Value);
        }

        #endregion
        #region Update Ship
        [Fact]
        public async void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange
            var newShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 100, ShipWidth = 50 };
            _mockShipService.Setup(repo => repo.AddShip(newShip)).ReturnsAsync(true);
            _mockShipService.Setup(repo => repo.GetShipByCode(newShip.Code)).ReturnsAsync(newShip);


            _mockShipService.Setup(repo => repo.isUniqueName(newShip.Name)).ReturnsAsync(true);

            var updatedShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 110, ShipWidth = 50 };
            _mockShipService.Setup(repo => repo.UpdateShip(updatedShip)).ReturnsAsync(true);


            //Act
            IActionResult actionResult = await _shipController.UpdateShip(updatedShip);

            // Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(okObjectResult.Value);
        }
        [Fact]
        public async void Task_Update_InvalidData_Return_BadRequest()
        {
            //Arrange
            var newShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 100, ShipWidth = 50 };
            _mockShipService.Setup(repo => repo.AddShip(newShip)).ReturnsAsync(true);
            _mockShipService.Setup(repo => repo.GetShipByCode(newShip.Code)).ReturnsAsync(newShip);
            _mockShipService.Setup(repo => repo.isUniqueName(newShip.Name)).ReturnsAsync(true);

            var updatedShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 0, ShipWidth = 50 };

            //Act
            IActionResult actionResult = await _shipController.UpdateShip(updatedShip);

            // Assert  
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(badRequestObjectResult.Value);
        }
        [Fact]
        public async void Task_Update_ShipNotExist_Return_BadRequest()
        {
            //Arrange
            var updatedShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 110, ShipWidth = 50 };
            _mockShipService.Setup(repo => repo.GetShipByCode(updatedShip.Code)).ReturnsAsync(updatedShip);

            _mockShipService.Setup(repo => repo.UpdateShip(updatedShip)).ReturnsAsync(false);

            //Act
            IActionResult actionResult = await _shipController.UpdateShip(updatedShip);

            // Assert  
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(badRequestObjectResult.Value);
        }

        #endregion

        #region Delete Ship
        [Fact]
        public async void Task_Delete_Return_OkResult()
        {
            //Arrange
            var newShip = new Ship() { Code = "BAAA-1111-A2", Name = "Test Ship B", ShipLength = 100, ShipWidth = 50 };
            var code = "BAAA-1111-A2";
            _mockShipService.Setup(repo => repo.AddShip(newShip)).ReturnsAsync(true);
            _mockShipService.Setup(repo => repo.DeleteShip(code)).ReturnsAsync(true);


            //Act
            IActionResult actionResult = await _shipController.DeleteShip(code);

            // Assert  
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(okObjectResult.Value);
        }
        [Fact]
        public async void Task_Delete_EmptyCode_Return_BadRequest()
        {
            //Arrange
            var code = "";
            //Act
            IActionResult actionResult = await _shipController.DeleteShip(code);

            // Assert  
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(badRequestObjectResult.Value);
        }
        [Fact]
        public async void Task_Delete_ShipNotExist_Return_BadRequest()
        {
            //Arrange
            var code = "BAAA";
            _mockShipService.Setup(repo => repo.DeleteShip(code)).ReturnsAsync(false);

            //Act
            IActionResult actionResult = await _shipController.DeleteShip(code);

            // Assert  
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Response<string> responseShip = Assert.IsType<Response<string>>(badRequestObjectResult.Value);
        }
        #endregion
    }
}
