using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.DriverDuties;
using System.Collections.Generic;
using System.IO;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class ImportDataService
    {
        private readonly ITripRepo _triprepo;
        private readonly IVehicleDutyRepo _vehicledutyrepo;
        private readonly IDriverDutyRepo _driverdutyrepo;
        private readonly IUnitOfWork _unitOfWork;

        public ImportDataService(IVehicleDutyRepo vehicleDutyRepo, ITripRepo tripRepo, IDriverDutyRepo driverDutyRepo, IUnitOfWork unitOfWork)
        {
            this._triprepo = tripRepo;
            this._vehicledutyrepo = vehicleDutyRepo;
            this._driverdutyrepo = driverDutyRepo;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> ImportDataFromFile(string fileType, Stream fileStream)
        {
            var parser = AdapterCreator.CreateParser(fileType, fileStream);

            var tripsTask = parser.GetTripsAsync();
            var vehicleDutyTask = parser.GetVehicleDutiesAsync();
            var driverDutyTask = parser.GetDriverDutiesAsync();
            tripsTask.Start();
            vehicleDutyTask.Start();
            driverDutyTask.Start();

            var tripsResult = await tripsTask;
            var vehicledutyResult = await vehicleDutyTask;
            var driverdutyResult = await driverDutyTask;

            var result = CheckSuccess(tripsResult, vehicledutyResult);
            if (result.IsSuccess == false)
                return result;

            await AddTrips(tripsResult.Value);
            await AddVehicleDuties(vehicledutyResult.Value);
            await AddDriverDuties(driverdutyResult.Value);

            await this._unitOfWork.CommitAsync();

            return Result<string>.Ok("");
        }

        private Result<string> CheckSuccess(Result<List<Trip>> tripResult, Result<List<VehicleDuty>> vehicledutyResult)
        {
            string message = "";
            bool error = false;
            if (tripResult.IsSuccess == false)
            {
                error = true;
                message += tripResult.Error;
            }
            if (vehicledutyResult.IsSuccess == false)
            {
                error = true;
                message += vehicledutyResult.Error;
            }

            if (error)
                return Result<string>.Fail(message);

            return Result<string>.Ok(message);
        }

        private async Task<List<Trip>> AddTrips(List<Trip> trips)
        {
            foreach (var trip in trips)
            {
                await this._triprepo.AddAsync(trip);
            }
            return trips;
        }

        private async Task<List<VehicleDuty>> AddVehicleDuties(List<VehicleDuty> vehicleDuties)
        {
            foreach (var vd in vehicleDuties)
            {
                await this._vehicledutyrepo.AddAsync(vd);
            }
            return vehicleDuties;
        }

        private async Task<List<DriverDuty>> AddDriverDuties(List<DriverDuty> driverDuties)
        {
            foreach (var dd in driverDuties)
            {
                await this._driverdutyrepo.AddAsync(dd);
            }
            return driverDuties;
        }

    }
}