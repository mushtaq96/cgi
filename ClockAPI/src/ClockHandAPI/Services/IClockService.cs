using ClockHandAPI.Models;
namespace ClockHandAPI.Services
{
    // Contains the interface for the ClockService class.
    public interface IClockService
    {
        (int HourAngle, int MinuteAngle) CalculateHandAngles(int hours, int minutes);
        (int Hours, int Minutes) CalculateTimeFromAngles(int hourAngle, int minuteAngle);
    }
}