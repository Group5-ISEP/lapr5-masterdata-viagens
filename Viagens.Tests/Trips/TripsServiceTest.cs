using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Trips;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Viagens.Tests
{
    public class TripServiceTests
    {
        private TripService _service = new TripService(new MockTripRepo(), new MockUnitOfWork());

        CreateTripsDTO sample = new CreateTripsDTO()
        {
            Frequency = 300,
            NumberOfTrips = 2,
            StartTime = 0,
            PathTo = new PathDTO()
            {
                LineId = "Line:1",
                PathId = "Path:1",
                IsEmpty = false,
                Orientation = "To",
                Segments = new List<SegmentDTO>(){
                        new SegmentDTO(){
                            StartNodeId = "Node:1",
                            EndNodeId="Node:2",
                            Distance =2000,
                            Duration = 500,
                            Order = 1
                        },
                        new SegmentDTO(){
                            StartNodeId = "Node:2",
                            EndNodeId="Node:3",
                            Distance =1500,
                            Duration = 500,
                            Order = 2
                        }
                    }
            },
            PathFrom = new PathDTO()
            {
                LineId = "Line:1",
                PathId = "Path:2",
                IsEmpty = false,
                Orientation = "From",
                Segments = new List<SegmentDTO>(){
                        new SegmentDTO(){
                            StartNodeId = "Node:3",
                            EndNodeId="Node:2",
                            Distance =1500,
                            Duration = 500,
                            Order = 1
                        },
                        new SegmentDTO(){
                            StartNodeId = "Node:2",
                            EndNodeId="Node:1",
                            Distance =2000,
                            Duration = 500,
                            Order = 2
                        }
                    }
            },
        };

        [Test]
        public void expectSuccessIfEveythingValid()
        {
            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Count == 4);
            Assert.IsTrue(result.Value[0].PassingTimes.Count == 3);
            Assert.IsTrue(result.Value[1].PassingTimes.Count == 3);
        }

    }

}