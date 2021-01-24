using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;

namespace lapr5_masterdata_viagens.Domain.VehicleDuties
{
    public class VehicleDutyService
    {
        private readonly IVehicleDutyRepo _repo;
        private readonly ITripRepo _tripsrepo;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleDutyService(IVehicleDutyRepo repo, ITripRepo tripsrepo, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._tripsrepo = tripsrepo;
        }

        public async Task<Result<VehicleDutyDTO>> CreateVehicleDuty(VehicleDutyDTO dto)
        {
            if (dto.Trips == null)
                return Result<VehicleDutyDTO>.Fail("Trips list cant be null");
            List<TripId> ids = dto.Trips.ConvertAll<TripId>(id => new TripId(id));
            List<Trip> trips = await _tripsrepo.GetByIdsAsync(ids);
            var result = VehicleDuty.Create(dto.Name, trips);
            if (result.IsSuccess == false)
                return Result<VehicleDutyDTO>.Fail(result.Error);

            VehicleDuty VehicleDutySaved = await _repo.AddAsync(result.Value);

            await _unitOfWork.CommitAsync();

            return Result<VehicleDutyDTO>.Ok(VehicleDutyMapper.ToDto(VehicleDutySaved));
        }

        public async Task<Result<VehicleDutyDTO>> AddWorkblocks(AddWorkblocksDto dto)
        {
            VehicleDuty vehicleDuty = await this._repo.GetByIdAsync(new VehicleDutyId(dto.VehicleDutyId));
            if (vehicleDuty == null)
                return Result<VehicleDutyDTO>.Fail("Vehicle duty not found");

            var result = vehicleDuty.AddWorkBlocks(dto.Duration, dto.NumberOfWorkblocks);
            if (result.IsSuccess == false)
                return Result<VehicleDutyDTO>.Fail(result.Error);

            await _unitOfWork.CommitAsync();

            return Result<VehicleDutyDTO>.Ok(VehicleDutyMapper.ToDto(vehicleDuty));
        }
    }
}