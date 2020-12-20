using System.Threading.Tasks;
using lapr5_masterdata_viagens.DTOs;
using lapr5_masterdata_viagens.Shared;

namespace lapr5_masterdata_viagens.Services
{
    public class VehicleService
    {
        public async Task<Result<VehicleDTO>> CreateVehicle(VehicleDTO dto)
        {
            return Result<VehicleDTO>.Ok(dto);
        }
    }
}