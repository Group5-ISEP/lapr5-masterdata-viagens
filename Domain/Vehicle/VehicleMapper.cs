using System.Globalization;

namespace lapr5_masterdata_viagens.Domain.Vehicle
{
    public class VehicleMapper
    {
        public static VehicleDTO ToDto(Vehicle v)
        {
            VehicleDTO dto = new VehicleDTO();
            dto.Id = v.Id.ToString();
            dto.CarPlateCode = v.CarPlateCode;
            dto.VIN = v.VIN;
            dto.VehicleTypeID = v.VehicleTypeID;
            dto.ServiceStartDate = v.ServiceStartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            return dto;
        }
    }
}