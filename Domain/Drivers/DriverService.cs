using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public class DriverService
    {
        private readonly IDriverRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DriverService(IDriverRepo repo, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<Result<DriverDTO>> CreateDriver(DriverDTO dto)
        {
            var result = Driver.Create(dto);
            if (result.IsSuccess == false)
                return Result<DriverDTO>.Fail(result.Error);

            Driver DriverSaved = await _repo.AddAsync(result.Value);

            await _unitOfWork.CommitAsync();

            return Result<DriverDTO>.Ok(DriverMapper.ToDto(DriverSaved));
        }
    }
}