using NUnit.Framework;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Node;

namespace Viagens.Tests
{
    public class GetNodeTimetable
    {

        TripService service;

        NodeTimetableDto dto;

        [SetUp]
        public void Setup()
        {
            var sample = new CreateTripsDTO()
            {
                Frequency = 300,
                NumberOfTrips = 1,
                StartTime = 0,
                Line = "Line:1",
                PathTo = "Path:1",
                PathFrom = "Path:2",
            };

            service = new TripService(new MockTripRepo(), new MockUnitOfWork(), new MockMDRHttpClient());

            service.CreateTrips(sample).Wait();

        }

        [Test]
        public void expectSuccessIfEveythingValid()
        {
            var task = service.GetNodeTimetable("Node:1");
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.schedule.Count == 2);
        }
    }
}