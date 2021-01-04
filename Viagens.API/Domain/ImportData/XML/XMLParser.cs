using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class XMLParser : Parser
    {

        public XMLParser(Stream fileStream)
        {
        }

        public async Task<Result<List<Trip>>> GetTripsAsync()
        {
            return Result<List<Trip>>.Fail("teste");
        }

        public async Task<Result<List<VehicleDuty>>> GetVehicleDutiesAsync()
        {
            return Result<List<VehicleDuty>>.Fail("teste");
        }
    }
}