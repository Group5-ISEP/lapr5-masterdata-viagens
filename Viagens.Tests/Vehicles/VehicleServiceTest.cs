using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.Vehicles;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class VehicleServiceTests
    {
        private VehicleService _service = new VehicleService(new MockVehicleRepo(), new MockUnitOfWork());

        [Test]
        public void expectSuccessIfEveythingValid()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = "12345678901234567",
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfPlateCodeNull()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = null,
                VIN = "12345678901234567",
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }


        [Test]
        public void expectFailureIfPlateCodeInvalidFormat()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-BB",
                VIN = "12345678901234567",
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfVINNull()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = null,
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfVINNotSeventeenChars()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = "1234567890",
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfStartDateNull()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = "12345678901234567",
                ServiceStartDate = null,
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfStartDateInvalidFormat()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = "12345678901234567",
                ServiceStartDate = "10/10/2000",
                VehicleTypeID = "RandomId"
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void expectFailureIfTypeIdNull()
        {
            VehicleDTO sample = new VehicleDTO()
            {
                CarPlateCode = "22-AA-45",
                VIN = "12345678901234567",
                ServiceStartDate = "2000-10-10",
                VehicleTypeID = null
            };

            var task = _service.CreateVehicle(sample);
            task.Wait();
            var result = task.Result;

            Assert.IsFalse(result.IsSuccess);
        }

    }

}