using ClockHandAPI.Models;

namespace ClockHandAPI.Services
{
    // Contains the implementation of the IClockService interface.
    public class ClockService : IClockService
    {
        public (int HourAngle, int MinuteAngle) CalculateHandAngles(int hours, int minutes){
            // input validation
            if(hours < 0 || hours > 23 || minutes < 0 || minutes > 59){
                throw new ArgumentException("Invalid input. Hourse should be 0-23, minutes should be 0-59");
            }
            hours %= 12; // convert to 12-hour clock
            int minuteAngle = minutes * 6; // 360 degrees / 60 minutes
            int hourAngle = (int) ((hours * 30) + (minutes * 0.5)); // 360 degrees / 12 hours = 30 degrees per hour, 30 degrees / 60 minutes = 0.5 degrees per minute
            return (hourAngle, minuteAngle);
        }

        public (int Hours, int Minutes) CalculateTimeFromAngles(int hourAngle, int minuteAngle){
            // input validation
            if (hourAngle < 0 || hourAngle >= 360 || minuteAngle < 0 || minuteAngle >= 360)
            {
                throw new ArgumentException("Invalid input. Angles must be between 0 and 359.");
            }

            int minutes = minuteAngle / 6; // 360 degrees / 60 minutes = 6 degrees per minute
            double hours = (hourAngle - (minutes * 0.5)) / 30.0; // Reverse of hour angle calculation
            hours = hours % 12; // Convert to 12-hour format
            if (hours < 0) hours += 12; // Handle negative values

            return ((int)Math.Round(hours), minutes);
        }
    }
}
