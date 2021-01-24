using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Path;
using System.Collections.Generic;

namespace Viagens.Tests
{
    public class AddWorkblocksService
    {
        VehicleDutyService service;

        AddWorkblocksDto dto;

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
                            Duration = 20,
                            Order = 1
                        },
                        new SegmentDTO(){
                            StartNodeId = "Node:2",
                            EndNodeId="Node:3",
                            Distance =1500,
                            Duration = 10,
                            Order = 2
                        }
                    }
            };

            var trip = Trip.Create(0, pathdto1).Value;
            var trip2 = Trip.Create(30, pathdto1).Value;

            var tripsList = new List<Trip>();
            tripsList.Add(trip);
            tripsList.Add(trip2);

            var vehicleDuty = VehicleDuty.Create("V 12", tripsList, "VehicleDuty:1").Value;

            MockVehicleDutyRepo mockVehicleDutyRepo = new MockVehicleDutyRepo();
            mockVehicleDutyRepo.AddAsync(vehicleDuty).Wait();

            this.service = new VehicleDutyService(mockVehicleDutyRepo, new MockTripRepo(), new MockUnitOfWork());

            this.dto = new AddWorkblocksDto()
            {
                VehicleDutyId = "VehicleDuty:1",
                Duration = 30,
                NumberOfWorkblocks = 2
            };
        }

        [Test]
        public void expectFailureIfDurationOfBlockIsLessThanOne()
        {
            this.dto.Duration = 0;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Duration of workblock cant be less than 1", result.Error);
        }

        [Test]
        public void expectFailureIfDurationOfBlockIsMoreThanFourHours()
        {
            this.dto.Duration = 14500;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Duration of workblock cant be greater than 4 hours (14400 seconds)", result.Error);
        }

        [Test]
        public void expectFailureIfNumberOfBlocksLessThanOne()
        {
            this.dto.NumberOfWorkblocks = 0;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Number of workblocks cant be less than 1", result.Error);
        }

        [Test]
        public void expectFailureIfVehicleDutyNotFound()
        {
            this.dto.VehicleDutyId = "Random";
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Vehicle duty not found", result.Error);
        }

        [Test]
        public void expectFailureIfNoMoreRoomForWorkblocks()
        {
            this.service.AddWorkblocks(this.dto).Wait();

            this.dto.Duration = 15;
            this.dto.NumberOfWorkblocks = 1;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No room for more workblocks", result.Error);
        }

        [Test]
        public void expectLessBlocksToBeAddedWhenAddingBlocksThatNotAllFit()
        {
            this.dto.NumberOfWorkblocks = 3;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Workblocks.Count == 2);
        }

        [Test]
        public void expectLastBlockToBeShorterIfTimeRemainingLessThanDuration()
        {
            this.dto.Duration = 25;
            this.dto.NumberOfWorkblocks = 3;
            var task = this.service.AddWorkblocks(this.dto);
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Workblocks.Count == 3);

            var workblocks = result.Value.Workblocks;
            var lastWb = workblocks[workblocks.Count - 1];
            Assert.IsTrue((lastWb.EndTime - lastWb.StartTime) == 10);
        }
    }
}