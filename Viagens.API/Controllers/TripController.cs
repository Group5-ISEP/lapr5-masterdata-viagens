using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Trips;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripController : ControllerBase
    {
        private readonly TripService _service;

        public TripController(TripService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<List<TripDTO>>> Create(CreateTripsDTO dto)
        {
            var result = await _service.CreateTrips(dto);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Created("api/trips/", result.Value);
        }

        [HttpGet("{lineId}")]
        public async Task<ActionResult<List<TripDTO>>> GetByLine(string lineId)
        {
            var result = await _service.GetByLine(lineId);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Ok(result.Value);
        }
    }
}