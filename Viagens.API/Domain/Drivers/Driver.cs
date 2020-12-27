using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public class Driver : Entity<DriverId>, IAggregateRoot
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }

        public string DriverLicenseNumber { get; private set; }
        public DateTime DriverLicenseExpirationDate { get; private set; }
        public List<string> TypesIDs { get; private set; }

        private Driver()
        {
            //FOR ORM
        }
        private Driver(string name, DateTime birthDate, string licenseNumber, DateTime licenseExpirationDate, List<string> types)
        {
            this.Id = new DriverId(Guid.NewGuid().ToString());
            this.Name = name;
            this.BirthDate = birthDate;
            this.DriverLicenseNumber = licenseNumber;
            this.DriverLicenseExpirationDate = licenseExpirationDate;
            this.TypesIDs = types;
        }

        public static Result<Driver> Create(DriverDTO dto)
        {
            string Name = dto.Name;
            string BirthDateString = dto.BirthDate;
            string DriverLicenseNumber = dto.DriverLicenseNumber;
            string DriverLicenseExpirationDateString = dto.DriverLicenseExpirationDate;
            List<string> Types = dto.TypesIDs;

            if (Name == null)
                return Result<Driver>.Fail("Driver must have a name");

            if (BirthDateString == null || Regex.IsMatch(BirthDateString, "^[0-9]{4}-[0-9]{2}-[0-9]{2}$") == false)
                return Result<Driver>.Fail("Driver must have a birth start date in form yyyy-MM-dd");

            if (DriverLicenseNumber == null)
                return Result<Driver>.Fail("Driver must have a license number");

            if (DriverLicenseExpirationDateString == null || Regex.IsMatch(DriverLicenseExpirationDateString, "^[0-9]{4}-[0-9]{2}-[0-9]{2}$") == false)
                return Result<Driver>.Fail("Driver must have a license expiration date start date in form yyyy-MM-dd");

            if (Types == null || Types.Count < 1 || Types.Contains(null))
                return Result<Driver>.Fail("Driver must have a list of types with non null values with at least 1 type");


            DateTime BirthDate = DateTime.ParseExact(BirthDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime LicenseExpirationDate = DateTime.ParseExact(DriverLicenseExpirationDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Driver Driver = new Driver(Name, BirthDate, DriverLicenseNumber, LicenseExpirationDate, Types);
            return Result<Driver>.Ok(Driver);
        }
    }
}