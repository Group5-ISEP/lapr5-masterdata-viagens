using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.DriverDuties;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class XMLParser : Parser
    {
        //TODO: IMPLEMENT
        public XMLParser(Stream fileStream)
        {
        }

        public async Task<Result<List<Trip>>> GetTripsAsync()
        {
            List<PassingTime> pt = new List<PassingTime>() { new PassingTime(10, "Node:1"), new PassingTime(20, "Node:2") };
            var res = Trip.Create("Trip:1", "Path:1", "Line:1", "To", pt);
            System.Console.WriteLine(res.Value);
            return Result<List<Trip>>.Ok(new List<Trip>() { res.Value });
        }

        public async Task<Result<List<VehicleDuty>>> GetVehicleDutiesAsync()
        {
            return Result<List<VehicleDuty>>.Ok(new List<VehicleDuty>());
        }

        public async Task<Result<List<DriverDuty>>> GetDriverDutiesAsync()
        {
            return Result<List<DriverDuty>>.Ok(new List<DriverDuty>());
        }
    }
}