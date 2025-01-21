using Microsoft.AspNetCore.Mvc;
using ClockHandAPI.Models;
using ClockHandAPI.Services;
using ClockHandAPI.DTOs;

namespace ClockHandAPI.Controllers
{
    // This file contains the controller for the Clock API, it handles HTTP requests and responses, delegating business logic to services.
    [ApiController]
    [Route("api/[controller]")]
    public class ClockController : ControllerBase{
        // Dependency injection of the IClockService, injects the service and assigns it
        private readonly IClockService _clockService;
        public ClockController(IClockService clockService){
            _clockService = clockService;
        }

        [HttpGet("hands")] // GET /api/clock/hands
        // Query parameters are passed in the URL, e.g. /api/clock/hands?hours=12&minutes=30
        // The [FromQuery] attribute tells ASP.NET Core to bind the parameters from the query string to the method parameters.
        // The method returns an object with the calculated hour and minute angles.
        public ActionResult<object> GetHands([FromQuery] ClockHandsDto dto){
            try{
                var (hourAngle, minuteAngle) = _clockService.CalculateHandAngles(dto.Hours, dto.Minutes);
                return Ok(new {Hour = hourAngle, Minute = minuteAngle});
            } catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("time")] // GET /api/clock/time
        // Query parameters are passed in the URL, e.g. /api/clock/time?hourAngle=90&minuteAngle=180
        // The [FromQuery] attribute tells ASP.NET Core to bind the parameters from the query string to the method parameters.
        // The method returns an object with the calculated hours and minutes.
        public ActionResult<object> GetTimeFromAngles([FromQuery] int hourAngle, [FromQuery] int minuteAngle)
        {
            try
            {
                var (hours, minutes) = _clockService.CalculateTimeFromAngles(hourAngle, minuteAngle);
                return Ok(new { Hours = hours, Minutes = minutes });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}