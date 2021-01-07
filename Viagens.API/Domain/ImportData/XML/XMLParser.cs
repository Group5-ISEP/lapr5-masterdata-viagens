using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.DriverDuties;

namespace lapr5_masterdata_viagens.Domain.ImportData.XML
{
    public class XMLParser : Parser
    {
        //TODO: IMPLEMENT

        private Document doc;

        public XMLParser(Stream fileStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            this.doc = (Document)serializer.Deserialize(fileStream);
        }

        public async Task<Result<List<Trip>>> GetTripsAsync()
        {
            var docTripsList = doc.World.GlDocument.GlDocumentSchedule.Schedule.Trips;

            var list = new List<Trip>();
            foreach (var doctrip in docTripsList)
            {
                var passingTimes = doctrip.PassingTimes.ConvertAll<PassingTime>(pt => new PassingTime(pt.Key, pt.Time, pt.Node));

                if (doctrip.Orientation == "Go")
                    doctrip.Orientation = "To";
                if (doctrip.Orientation == "Return")
                    doctrip.Orientation = "From";

                var result = Trip.Create(doctrip.Key, doctrip.Path, doctrip.Line, doctrip.Orientation, passingTimes);
                if (result.IsSuccess)
                    list.Add(result.Value);
            }
            return Result<List<Trip>>.Ok(list);
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