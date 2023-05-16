using DatabaseFirst.Model;
using DatabaseFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {

        private readonly ITripsService _tripsService;

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {

            return Ok();
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip([FromBody] ClientRequest clientRequest, [FromRoute] int idTrip)
        {

            return Ok();
        }
    }
}