using NUnit.Framework;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Domain.VehicleDuties;

namespace Viagens.Tests
{
    public class VehicleDutyServiceTests
    {
        VehicleDutyService _service;

        VehicleDutyDTO _dto;

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

            var trip = Trip.Create(0, pathdto1, tripDTO).Value;

            MockTripRepo mockTripRepo = new MockTripRepo();

            mockTripRepo.AddAsync(trip).Wait();

            this._service = new VehicleDutyService(new MockVehicleDutyRepo(), mockTripRepo, new MockUnitOfWork());

            _dto = new VehicleDutyDTO()
            {
                Name = "V 12",
                Trips = new List<string>() { "Trip:1" }
            };
        }

        [Test]
        public void expectFailureIfNameNull()
        {
            _dto.Name = null;

            var task = _service.CreateVehicleDuty(_dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty name cant be null", result.Error);
        }

        [Test]
        public void expectFailureIfNameEmpty()
        {
            _dto.Name = "";

            var task = _service.CreateVehicleDuty(_dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty name cant be empty", result.Error);
        }

        [Test]
        public void expectFailureIfTripsListNull()
        {
            _dto.Trips = null;

            var task = _service.CreateVehicleDuty(_dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trips list cant be null", result.Error);
        }

        [Test]
        public void expectFailureIfTripsDontExist()
        {
            _dto.Trips = new List<string>() { "Trip:2" };

            var task = _service.CreateVehicleDuty(_dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trips list cant be empty", result.Error);
        }
    }
}