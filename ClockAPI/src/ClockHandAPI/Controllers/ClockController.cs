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
        public ActionResult<object> GetHands([FromQuery] ClockHandsDto dto){
            // dont need to have try-catch bcz validations in DTO
            try{
                var (hourAngle, minuteAngle) = _clockService.CalculateHandAngles(dto.Hours, dto.Minutes);
                return Ok(new {Hour = hourAngle, Minute = minuteAngle});
            } catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}