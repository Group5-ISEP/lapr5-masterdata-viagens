using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using System.Collections.Generic;
using System.IO;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class ImportDataService
    {
        private readonly ITripRepo _triprepo;
        private readonly IVehicleDutyRepo _vehicledutyrepo;
        private readonly IUnitOfWork _unitOfWork;

        public ImportDataService(IVehicleDutyRepo vehicleDutyRepo, ITripRepo tripRepo, IUnitOfWork unitOfWork)
        {
            this._triprepo = tripRepo;
            this._vehicledutyrepo = vehicleDutyRepo;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<List<string>>> ImportDataFromFile(string fileType, Stream fileStream)
        {
           // AdapterCreator.CreateParser(fileType, fileStream);
            fileStream.Close();
            var a = new List<string>() { "a" };
            return Result<List<string>>.Ok(a);
        }

    }
}