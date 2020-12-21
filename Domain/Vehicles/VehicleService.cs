using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Domain.Vehicles
{
    public class VehicleService
    {
        private readonly IVehicleRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IVehicleRepo repo, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<Result<VehicleDTO>> CreateVehicle(VehicleDTO dto)
        {
            var result = Vehicle.Create(dto);
            if (result.IsSuccess == false)
                return Result<VehicleDTO>.Fail(result.Error);

            Vehicle vehicleSaved = await _repo.AddAsync(result.Value);

            await _unitOfWork.CommitAsync();

            return Result<VehicleDTO>.Ok(VehicleMapper.ToDto(vehicleSaved));
        }
    }
}