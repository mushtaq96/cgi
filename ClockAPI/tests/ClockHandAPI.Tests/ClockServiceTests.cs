using Xunit;
using ClockHandAPI.Services;

namespace ClockHandAPI.Tests;

public class ClockServiceTests
{
    //Arrange
    private readonly ClockService _clockService = new ClockService();

    [Theory]
    [InlineData(0, 0, 0, 0)] //midnight
    [InlineData(12, 0, 0, 0)] //noon
    [InlineData(12, 30, 15, 180)] //half past noon
    [InlineData(23, 59, 359, 354)] // 23:59
    public void CalculateHandAngles_ValidInput_ReturnsCorrectAngles(int hours, int minutes, int expectedHourAngle, int expectedMinuteAngle)
    {
        // Act
        var (hourAngle, minuteAngle) = _clockService.CalculateHandAngles(hours, minutes);
        // Assert
        Assert.Equal(expectedHourAngle, hourAngle);
        Assert.Equal(expectedMinuteAngle, minuteAngle);
    }
    [Theory]
    [InlineData(25, 70)] // Invalid input
    [InlineData(-1, -1)] // Invalid input
    public void CalculateHandAngles_InvalidInput_ThrowsException(int hours, int minutes)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _clockService.CalculateHandAngles(hours, minutes)); // Assert that the method throws an exception
    }
}