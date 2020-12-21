using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;

namespace lapr5_masterdata_viagens.Domain.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string CarPlateCode { get; private set; }
        public string VIN { get; private set; }
        public DateTime ServiceStartDate { get; private set; }
        public string VehicleTypeID { get; private set; }
        private Vehicle(Guid id, string carPlateCode, string vin, string vehicleTypeId, DateTime serviceStarDate)
        {
            this.Id = id;
            this.CarPlateCode = carPlateCode;
            this.VIN = vin;
            this.VehicleTypeID = vehicleTypeId;
            this.ServiceStartDate = serviceStarDate;
        }

        public static Result<Vehicle> Create(VehicleDTO dto)
        {
            string platecode = dto.CarPlateCode;
            string typeid = dto.VehicleTypeID;
            string vin = dto.VIN;
            string datestring = dto.ServiceStartDate;
            Guid id;

            if (dto.Id == null)
                id = Guid.NewGuid();
            else
                id = Guid.Parse(dto.Id);

            if (platecode == null || Regex.IsMatch(platecode, "^([A-Z]{2}-[0-9]{2}-[0-9]{2})|([0-9]{2}-[A-Z]{2}-[0-9]{2})|([0-9]{2}-[0-9]{2}-[A-Z]{2})$") == false)
                return Result<Vehicle>.Fail("Vehicle must have a plate code in format xx-xx-xx, 2 pair of numbers + 1 pair of uppercase letters");

            if (vin == null || Regex.IsMatch(vin, "^[0-9A-Z]{17}$") == false)
                return Result<Vehicle>.Fail("Vehicle must have a VIN");

            if (datestring == null || Regex.IsMatch(datestring, "^[0-9]{4}-[0-9]{2}-[0-9]{2}$") == false)
                return Result<Vehicle>.Fail("Vehicle must have a service start date in form yyyy-MM-dd");

            if (typeid == null)
                return Result<Vehicle>.Fail("Vehicle must have the type id");


            DateTime date = DateTime.ParseExact(datestring, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Vehicle vehicle = new Vehicle(id, platecode, vin, typeid, date);
            return Result<Vehicle>.Ok(vehicle);
        }
    }
}