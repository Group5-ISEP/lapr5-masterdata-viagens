using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.DriverDuties;
using lapr5_masterdata_viagens.Domain.Workblocks;

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

        public Task<Result<List<Trip>>> GetTripsAsync()
        {
            return new Task<Result<List<Trip>>>(() =>
            {
                var docTripsList = doc.World.GlDocument.GlDocumentSchedule.Schedule.Trips;

                var list = new List<Trip>();
                foreach (var doctrip in docTripsList)
                {
                    var trip = ToTrip(doctrip);
                    list.Add(trip);
                }
                return Result<List<Trip>>.Ok(list);
            });

        }

        public Task<Result<List<VehicleDuty>>> GetVehicleDutiesAsync()
        {
            return new Task<Result<List<VehicleDuty>>>(() =>
            {
                var docVehicleDuties = doc.World.GlDocument.GlDocumentSchedule.Schedule.VehicleDuties;
                var docWorkBlocks = doc.World.GlDocument.GlDocumentSchedule.Schedule.WorkBlocks;

                var vehicleDutyList = docVehicleDuties.ConvertAll<VehicleDuty>(docVd =>
                {
                    List<Workblock> workblocks = docVd.WorkBlocks.ConvertAll<Workblock>(wbRef =>
                    {
                        DocWorkBlock docWorkBlock = docWorkBlocks.Find(doc => doc.Key == wbRef.Key);
                        return ToWorkBlock(docWorkBlock);
                    });

                    var vehicleDutyTrips = new List<Trip>();
                    foreach (var wb in workblocks)
                    {
                        foreach (var trip in wb.Trips)
                        {
                            if (vehicleDutyTrips.Contains(trip) == false)
                                vehicleDutyTrips.Add(trip);
                        }
                    }

                    VehicleDuty vehicleDuty = VehicleDuty.Create(docVd.Name, vehicleDutyTrips, docVd.Key).Value;
                    vehicleDuty.AddWorkBlocks(workblocks);
                    return vehicleDuty;
                });

                return Result<List<VehicleDuty>>.Ok(vehicleDutyList);
            });
        }

        public Task<Result<List<DriverDuty>>> GetDriverDutiesAsync()
        {
            return new Task<Result<List<DriverDuty>>>(() =>
            {
                var docDriverDuties = doc.World.GlDocument.GlDocumentSchedule.Schedule.DriverDuties;
                var docWorkBlocks = doc.World.GlDocument.GlDocumentSchedule.Schedule.WorkBlocks;

                var driverDuties = docDriverDuties.ConvertAll<DriverDuty>(docDd =>
                {
                    List<Workblock> workblocks = docDd.WorkBlocks.ConvertAll<Workblock>(wbRef =>
                    {
                        DocWorkBlock docWorkBlock = docWorkBlocks.Find(doc => doc.Key == wbRef.Key);
                        return ToWorkBlock(docWorkBlock);
                    });

                    return DriverDuty.Create(docDd.Name, workblocks, docDd.Key).Value;
                });

                return Result<List<DriverDuty>>.Ok(driverDuties);
            });
        }

        private Workblock ToWorkBlock(DocWorkBlock docWorkBlock)
        {
            var docTrips = doc.World.GlDocument.GlDocumentSchedule.Schedule.Trips;

            var id = docWorkBlock.Key;
            var startTime = docWorkBlock.StartTime;
            var endTime = docWorkBlock.EndTime;

            var workblockTrips = docWorkBlock.Trips.ConvertAll<Trip>(tripRef =>
                {
                    var docTrip = docTrips.Find(t => t.Key == tripRef.Key);
                    return ToTrip(docTrip);
                });
            return Workblock.Create(startTime, endTime, workblockTrips, id).Value;
        }

        private Trip ToTrip(DocTrip doctrip)
        {
            var passingTimes = doctrip.PassingTimes.ConvertAll<PassingTime>(pt => new PassingTime(pt.Key, pt.Time, pt.Node));

            if (doctrip.Orientation == "Go")
                doctrip.Orientation = "To";
            if (doctrip.Orientation == "Return")
                doctrip.Orientation = "From";

            return Trip.Create(doctrip.Key, doctrip.Path, doctrip.Line, doctrip.Orientation, passingTimes).Value;
        }
    }
}