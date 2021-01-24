using NUnit.Framework;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Path;

namespace Viagens.Tests
{
    public class VehicleDutyCreationTest
    {
        List<Trip> _tripsList;

        [SetUp]
        public void Setup()
        {
            var pathdto1 = new PathDTO()
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
            };

            var tripDTO = new TripDTO()
            {
                Id = "Trip:1",
                LineID = "Line:1",
                PathID = "Path:1",
                Orientation = "To",
                PassingTimes = new List<PassingTimeDTO>()
                {
                    new PassingTimeDTO(){
                        Id="PassingTime:1",
                        NodeID="Node:1",
                        TimeInstant=0
                    },
                    new PassingTimeDTO(){
                        Id = "PassingTime:2",
                        NodeID = "Node:2",
                        TimeInstant = 500
                    },
                    new PassingTimeDTO(){
                        Id = "PassingTime:3",
                        NodeID = "Node:3",
                        TimeInstant = 1000
                    }
                }
            };

            var trip = Trip.Create(0, pathdto1, tripDTO);

            this._tripsList = new List<Trip>();
            this._tripsList.Add(trip.Value);
        }

        [Test]
        public void expectFailureIfNameNull()
        {
            var result = VehicleDuty.Create(null, this._tripsList);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty name cant be null", result.Error);
        }

        [Test]
        public void expectFailureIfNameEmpty()
        {
            var result = VehicleDuty.Create("", this._tripsList);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty name cant be empty", result.Error);
        }

        [Test]
        public void expectFailureIfTripsListNull()
        {
            var result = VehicleDuty.Create("V 12", null);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trips list cant be null", result.Error);
        }

        [Test]
        public void expectFailureIfTripsListEmpty()
        {
            _tripsList = new List<Trip>();

            var result = VehicleDuty.Create("V 12", _tripsList);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trips list cant be empty", result.Error);
        }


        [Test]
        public void expectFailureIfGivenIdIsEmpty()
        {
            var result = VehicleDuty.Create("V 12", _tripsList, "");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty id cant be empty", result.Error);
        }


        [Test]
        public void expectSuccessIfEveythingValid()
        {
            var result = VehicleDuty.Create("V 12", _tripsList, "VehicleDuty:1");

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("V 12", result.Value.Name);
            Assert.AreEqual("VehicleDuty:1", result.Value.Id.AsString());
            Assert.AreEqual(_tripsList, result.Value.Trips);

        }
    }
}