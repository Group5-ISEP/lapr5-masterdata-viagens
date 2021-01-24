using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Trips;
namespace Viagens.Tests
{
    public class TripServiceTests
    {
        private TripService _service;

        CreateTripsDTO sample;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            sample = new CreateTripsDTO()
            {
                Frequency = 300,
                NumberOfTrips = 2,
                StartTime = 0,
                Line = "Line:1",
                PathTo = "Path:1",
                PathFrom = "Path:2",
            };

            _service = new TripService(new MockTripRepo(), new MockUnitOfWork(), new MockMDRHttpClient());
        }

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

        [Test]
        public void expectFailurePathToNull()
        {
            sample.PathTo = null;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Path id cant be null", result.Error);
        }

        [Test]
        public void expectFailurePathFromNull()
        {
            sample.PathFrom = null;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Path id cant be null", result.Error);
        }

        [Test]
        public void expectFailureLineNull()
        {
            sample.Line = null;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Line id cant be null", result.Error);
        }

        [Test]
        public void expectFailureFrequencyLessThanOne()
        {
            sample.Frequency = 0;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Frequency cant be less than one", result.Error);
        }

        [Test]
        public void expectFailureNumberOfTripsLessThanOne()
        {
            sample.NumberOfTrips = 0;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Number of trips cant be less than one", result.Error);
        }

        [Test]
        public void expectFailureStartTimeLessThanZero()
        {
            sample.StartTime = -1;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be less than zero", result.Error);
        }

        [Test]
        public void expectFailureIfTripDoesntFitInto0to24HourPeriod()
        {
            sample.StartTime = 86400;

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be more than 86400 (24 hours)", result.Error);
        }

        [Test]
        public void expectFailureIfPathsNotFoundInFetchedPaths()
        {
            sample.Line = "Line:2";

            var task = _service.CreateTrips(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Path not found", result.Error);
        }

    }

}