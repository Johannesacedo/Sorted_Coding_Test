using Microsoft.AspNetCore.Mvc;
using Sorted_Coding_Test.Interface.Services;
using Sorted_Coding_Test.Model;
using InvalidRequestException = Sorted_Coding_Test.Model.InvalidRequestException;
using NotFoundException = Sorted_Coding_Test.Model.NotFoundException;

namespace Sorted_Coding_Test.Controllers
{
    [ApiController]
    [Route("rainfall")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;

        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }


        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return</param>
        /// <returns>A list of rainfall readings successfully retrieved</returns>
        [HttpGet("stations/{id}/readings")]
        [ProducesResponseType(typeof(RainfallReadingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRainfall(int stationId,[FromQuery] int count = 10)
        {
            try
            {
                // Use the service to retrieve rainfall readings
                var response = await _rainfallService.GetRainfallReadings(stationId, count);

                // Return a 200 OK response with the rainfall readings
                return Ok(response);
            }
            catch (InvalidRequestException ex)
            {
                // Handle invalid request errors with a 400 Bad Request response
                var errorResponse = new ErrorResponse
                {
                    Message = "Invalid request",
                    Details = ex.ErrorDetails
                };
                return BadRequest(errorResponse);
            }
            catch (NotFoundException ex)
            {
                // Handle not found errors with a 404 Not Found response
                var errorResponse = new ErrorResponse
                {
                    Message = "No readings found for the specified stationId",
                    Details = ex.ErrorDetails
                };
                return NotFound(errorResponse);
            }
            catch (Exception)
            {
                // Handle other unexpected errors with a 500 Internal Server Error response
                var errorResponse = new ErrorResponse
                {
                    Message = "Internal server error"
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
