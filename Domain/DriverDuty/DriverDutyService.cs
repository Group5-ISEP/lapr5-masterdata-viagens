using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.DriverDuties
{
    public class DriverDutyService
    {
        private readonly IDriverDutyRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DriverDutyService(IDriverDutyRepo repo, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<Result<DriverDutyDTO>> CreateDriverDuty(DriverDutyCreationDto dto)
        {
            var result = DriverDuty.Create(dto.Name);
            if (result.IsSuccess == false)
                return Result<DriverDutyDTO>.Fail(result.Error);

            DriverDuty DriverDutySaved = await _repo.AddAsync(result.Value);

            await _unitOfWork.CommitAsync();

            return Result<DriverDutyDTO>.Ok(DriverDutyMapper.ToDto(DriverDutySaved));
        }
    }
}