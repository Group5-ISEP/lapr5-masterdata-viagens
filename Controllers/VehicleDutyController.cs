using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/vehicleduty/")]
    public class VehicleDutyController : ControllerBase
    {
        private readonly VehicleDutyService _service;

        public VehicleDutyController(VehicleDutyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDutyDTO>> Create(VehicleDutyDTO dto)
        {
            var result = await _service.CreateVehicleDuty(dto);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Created("api/vehicleduty/" + result.Value.Id, result.Value);
        }

        [HttpPost("workblocks")]
        public async Task<ActionResult<VehicleDutyDTO>> AddWorkblocks(AddWorkblocksDto dto)
        {
            var result = await _service.AddWorkblocks(dto);
            if (result.IsSuccess == false)
                return Conflict(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDutyDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }
    }
}