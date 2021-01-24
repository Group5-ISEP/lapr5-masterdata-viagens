using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.DriverDuties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public interface Parser
    {
        Task<Result<List<Trip>>> GetTripsAsync();
        Task<Result<List<VehicleDuty>>> GetVehicleDutiesAsync();
        Task<Result<List<DriverDuty>>> GetDriverDutiesAsync();
    }
}