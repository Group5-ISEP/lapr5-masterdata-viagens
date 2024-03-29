using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Vehicles;
using System.Collections.Generic;


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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
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