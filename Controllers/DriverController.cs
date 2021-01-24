using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Drivers;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly DriverService _service;

        public DriverController(DriverService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<DriverDTO>> Create(DriverDTO dto)
        {
            var result = await _service.CreateDriver(dto);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Created("api/Drivers/" + result.Value.Id, result.Value);
        }
    }
}