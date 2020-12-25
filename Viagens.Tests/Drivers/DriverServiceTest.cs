using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Drivers;
using System.Collections.Generic;

namespace Viagens.Tests
{
    public class DriverServiceTest
    {
        private DriverService _service = new DriverService(new MockDriverRepo(), new MockUnitOfWork());

        [Test]
        public void expectSuccessIfEveythingValid()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfNameNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = null,
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfBirthDateNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = null,
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfBirthDateInvalidFormat()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "14/5/1980",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfLicenseNumberNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = null,
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfLicenseExpirationDateNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = null,
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfLicenseExpirationDateInvalidFormat()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "9/6/2025",
                TypesIDs = new List<string>() { "typeid1", "typeid2" }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfTypesListNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = null
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfTypesListEmpty()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>()
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfTypesListContainsNull()
        {
            DriverDTO sample = new DriverDTO()
            {
                Name = "Jose Coutinho",
                BirthDate = "1980-05-14",
                DriverLicenseNumber = "163828728",
                DriverLicenseExpirationDate = "2025-06-09",
                TypesIDs = new List<string>() { "typeid1", null }
            };

            var task = _service.CreateDriver(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }
    }
}