using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.DriverDuties;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/driverduty/")]
    public class DriverDutyController : ControllerBase
    {
        private readonly DriverDutyService _service;

        public DriverDutyController(DriverDutyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<DriverDutyDTO>> Create(DriverDutyCreationDto dto)
        {
            var result = await _service.CreateDriverDuty(dto);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Created("api/DriverDuty/" + result.Value.Id, result.Value);
        }

        /*  [HttpGet]
         public async Task<ActionResult<IEnumerable<DriverDutyDTO>>> GetAll()
         {
             return await _service.GetAllAsync();
         } */
    }
}