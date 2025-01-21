using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ClockHandAPI.Controllers;
using ClockHandAPI.Services;
using ClockHandAPI.DTOs;

namespace ClockHandAPI.Tests
{
    public class ClockControllerTests
    {
        private readonly Mock<IClockService> _mockClockService = new Mock<IClockService>();

        [Fact]
        public void GetHands_ValidInput_ReturnsOkResult()
        {
            // Arrange
            _mockClockService.Setup(service => service.CalculateHandAngles(12, 30)).Returns((15, 180));
            var controller = new ClockController(_mockClockService.Object);

            // Act
            var result = controller.GetHands(new ClockHandsDto { Hours = 12, Minutes = 30 });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Result property is of type ActionResult<object>
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetHands_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            _mockClockService.Setup(service => service.CalculateHandAngles(25, 70)).Throws<ArgumentException>();
            var controller = new ClockController(_mockClockService.Object);

            // Act
            var result = controller.GetHands(new ClockHandsDto { Hours = 25, Minutes = 70 });

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result); // .Result is of type ActionResult<object> which is the base class of BadRequestObjectResult
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}