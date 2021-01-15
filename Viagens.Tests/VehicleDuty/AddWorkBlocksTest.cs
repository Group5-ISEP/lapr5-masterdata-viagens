using NUnit.Framework;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.VehicleDuties;

namespace Viagens.Tests
{
    public class AddWorkBlocksTest
    {
        VehicleDuty vehicleDuty;

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

            this.vehicleDuty = VehicleDuty.Create("V 12", tripsList).Value;
        }

        [Test]
        public void expectFailureIfDurationOfBlockIsLessThanOne()
        {
            var result = this.vehicleDuty.AddWorkBlocks(0, 3);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Duration of workblock cant be less than 1", result.Error);
        }

        [Test]
        public void expectFailureIfDurationOfBlockIsMoreThanFourHours()
        {
            var result = this.vehicleDuty.AddWorkBlocks(14500, 3);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Duration of workblock cant be greater than 4 hours (14500 seconds)", result.Error);
        }

        [Test]
        public void expectFailureIfNumberOfBlocksLessThanOne()
        {
            var result = this.vehicleDuty.AddWorkBlocks(15, 0);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Number of workblocks cant be less than 1", result.Error);
        }

        [Test]
        public void expectFailureIfNoMoreRoomForWorkblocks()
        {
            var result = this.vehicleDuty.AddWorkBlocks(30, 2);
            Assert.IsTrue(result.IsSuccess);

            var result2 = this.vehicleDuty.AddWorkBlocks(15, 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No room for more workblocks", result.Error);
        }

        [Test]
        public void expectLessBlocksToBeAddedWhenAddingBlocksThatNotAllFit()
        {
            var result = this.vehicleDuty.AddWorkBlocks(30, 3);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Workblocks.Count == 2);
        }

        [Test]
        public void expectLastBlockToBeShorterIfTimeRemainingLessThanDuration()
        {
            var result = this.vehicleDuty.AddWorkBlocks(25, 3);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Workblocks.Count == 3);

            var workblocks = result.Value.Workblocks;
            var lastWb = workblocks[workblocks.Count - 1];
            Assert.IsTrue((lastWb.EndTime - lastWb.StartTime) == 10);
        }
    }
}