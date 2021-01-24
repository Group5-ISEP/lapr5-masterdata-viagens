using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public class DriverDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string DriverLicenseExpirationDate { get; set; }
        public List<string> TypesIDs { get; set; }

    }
}