using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Vehicle;
using lapr5_masterdata_viagens.Services;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _service;

        public VehicleController(VehicleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDTO>> Create(VehicleDTO dto)
        {
            var result = await _service.CreateVehicle(dto);
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Created("api/vehicles/" + result.Value.Id, result.Value);
        }

    }
}