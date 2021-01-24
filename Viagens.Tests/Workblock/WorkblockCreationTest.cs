using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Workblocks;
using lapr5_masterdata_viagens.Domain.Trips;
using System.Collections.Generic;

namespace Viagens.Tests
{
    public class WorkblockCreationTest
    {
        List<string> tripIds;

        [SetUp]
        public void Setup()
        {
            tripIds = new List<string>();
            var tripid = "Trip:1";
            tripIds.Add(tripid);
        }

        [Test]
        public void expectFailureIfStartTimeLessThanZero()
        {
            var result = Workblock.Create(-1, 1000, tripIds);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Workblock start time cant be less than zero", result.Error);
        }

        [Test]
        public void expectFailureIfEndTimeLessThanStartTime()
        {
            var result = Workblock.Create(100, 50, tripIds);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Workblock end time cant be less than start time", result.Error);
        }

        [Test]
        public void expectFailureIfEndTimeMoreThan24Hours()
        {
            var result = Workblock.Create(100, 89000, tripIds);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Workblock end time cant be more than 86400 (24 hours)", result.Error);
        }

        [Test]
        public void expectFailureIfTripsListNull()
        {
            var result = Workblock.Create(100, 500, null);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Trips list cant be null", result.Error);
        }

        [Test]
        public void expectSuccessIfEvrythingValid()
        {
            var result = Workblock.Create(100, 500, tripIds, "WorkBlock:1");

            Assert.IsTrue(result.IsSuccess);
        }

    }
}