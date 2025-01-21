using System.ComponentModel.DataAnnotations;
// this file contains the DTO for the ClockHands object, which is used to validate and bind the query parameters in the ClockController.
namespace ClockHandAPI.DTOs
{ 
    public class ClockHandsDto{
        [Range(0, 23, ErrorMessage = "Hours must be between 0 and 23")]
        public int Hours { get; set; }
        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59")]
        public int Minutes { get; set; }
    }
}