using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Trips;

namespace Viagens.Tests
{
    public class PassingTimeCreationTest
    {
        [Test]
        public void expectFailureIfTimeInstantLessZero()
        {
            var result = PassingTime.Create(-1, "Node:1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be less than zero", result.Error);
        }

        [Test]
        public void expectFailureIfTimeInstantMore24Hours()
        {
            var result = PassingTime.Create(90292, "Node:1");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Time instant cant be more than 86400 (24 hours)", result.Error);
        }

        [Test]
        public void expectFailureIfNodeIdNull()
        {
            var result = PassingTime.Create(0, null);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Passing time node id cant be null", result.Error);
        }

        [Test]
        public void expectSuccessIfEveythingValid()
        {
            var result = PassingTime.Create(0, "Node:1", "PassingTime:1");
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("PassingTime:1", result.Value.Id.AsString());
            Assert.AreEqual(0, result.Value.TimeInstant);
            Assert.AreEqual("Node:1", result.Value.NodeID);
        }
    }
}