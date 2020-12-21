using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Vehicle;
using lapr5_masterdata_viagens.Shared;

namespace lapr5_masterdata_viagens.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepo _repo;

        public VehicleService(IVehicleRepo repo)
        {
            this._repo = repo;
        }

        public async Task<Result<VehicleDTO>> CreateVehicle(VehicleDTO dto)
        {
            var result = Vehicle.Create(dto);
            if (result.IsSuccess == false)
                return Result<VehicleDTO>.Fail(result.Error);

            Vehicle vehicleSaved = await _repo.Save(result.Value);

            return Result<VehicleDTO>.Ok(VehicleMapper.ToDto(vehicleSaved));
        }
    }
}