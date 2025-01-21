using Xunit;
using ClockHandAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ClockHandAPI.Tests
{
    public class ClockHandsDtoTests
    {
        [Theory]
        [InlineData(24, 30, "Hours must be between 0 and 23")]
        [InlineData(12, 60, "Minutes must be between 0 and 59")]
        [InlineData(-1, 30, "Hours must be between 0 and 23")]
        [InlineData(12, -1, "Minutes must be between 0 and 59")]
        public void ClockHandsDto_Validation_ShouldFail(int hours, int minutes, string expectedErrorMessage)
        {
            // Arrange
            var dto = new ClockHandsDto { Hours = hours, Minutes = minutes };
            var context = new ValidationContext(dto, null, null); // object, serviceProvider, items
            var results = new List<ValidationResult>(); // to store validation results

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true); // It validates the object and returns true if the object is valid, otherwise false.
            
            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == expectedErrorMessage);
        }

        [Theory]
        [InlineData(12, 30)]
        [InlineData(0, 0)]
        [InlineData(23, 59)]
        public void ClockHandsDto_Validation_ShouldPass(int hours, int minutes)
        {
            // Arrange
            var dto = new ClockHandsDto { Hours = hours, Minutes = minutes };
            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            Assert.True(isValid);
        }
    }
}