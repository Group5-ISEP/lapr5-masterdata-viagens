using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Path;
using System.Collections.Generic;

namespace Viagens.Tests
{
    public class TripCreationTest
    {
        PathDTO pathdto1;
        TripDTO tripDTO;

        [SetUp]
        public void Setup()
        {
            pathdto1 = new PathDTO()
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

            tripDTO = new TripDTO()
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
        }

        [Test]
        public void expectFailureIfStartTimeLessThanZero()
        {
            var result = Trip.Create(-1, pathdto1);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be less than zero", result.Error);
        }

        [Test]
        public void expectFailureIfPathDtoNull()
        {
            var result = Trip.Create(0, null);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Path cant be null", result.Error);
        }

        [Test]
        public void expectFailureIfTripPassingTimeDoesntFitInto0to24HourPeriod()
        {
            var result = Trip.Create(86000, pathdto1);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be more than 86400 (24 hours)", result.Error);
        }

        [Test]
        public void expectFailureIfTripGeneratedPassingTimesDontMatchGivenDto()
        {
            tripDTO.PassingTimes[1].TimeInstant = 600;
            var result = Trip.Create(0, pathdto1, tripDTO);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trip dto doesnt match the path dto and passing times generated", result.Error);
        }

        [Test]
        public void expectFailureIfTripLineIdDontMatchGivenDto()
        {
            tripDTO.LineID = "Line:2";
            var result = Trip.Create(0, pathdto1,tripDTO);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trip dto doesnt match the path dto and passing times generated", result.Error);
        }

        [Test]
        public void expectSuccessIfEveythingValid()
        {

            var result = Trip.Create(0, pathdto1, tripDTO);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Trip:1", result.Value.Id.AsString());
            Assert.AreEqual("Path:1", result.Value.PathID);
            Assert.AreEqual("Line:1", result.Value.LineID);
            Assert.AreEqual(3, result.Value.PassingTimes.Count);
        }
    }
}