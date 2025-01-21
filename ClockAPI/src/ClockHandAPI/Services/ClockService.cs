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
            if(hourAngle < 0 || hourAngle > 359 || minuteAngle < 0 || minuteAngle > 359){
                throw new ArgumentException("Invalid input. Hour angle and minute angle should be 0-359");
            }
            int hours = (int) (hourAngle / 30); // 360 degrees / 12 hours = 30 degrees per hour
            int minutes = (int) ((hourAngle % 30) * 2); // 30 degrees / 60 minutes = 0.5 degrees per minute
            if(minutes != minuteAngle){
                throw new ArgumentException("Invalid input. The given angles do not correspond to a valid time.");
            }
            return (hours, minutes);
        }
    }
}
