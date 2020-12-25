using System.Globalization;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public class DriverMapper
    {
        public static DriverDTO ToDto(Driver driver)
        {
            DriverDTO dto = new DriverDTO();
            dto.Id = driver.Id.AsString();
            dto.Name = driver.Name;
            dto.BirthDate = driver.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            dto.DriverLicenseNumber = driver.DriverLicenseNumber;
            dto.DriverLicenseExpirationDate = driver.DriverLicenseExpirationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            dto.TypesIDs = driver.TypesIDs;
            return dto;
        }
    }
}