using System.Globalization;

namespace lapr5_masterdata_viagens.Domain.VehicleDuties
{
    public class VehicleDutyMapper
    {
        public static VehicleDutyDTO ToDto(VehicleDuty vehicleDuty)
        {
            VehicleDutyDTO dto = new VehicleDutyDTO();
            dto.Id = vehicleDuty.Id.AsString();
            dto.Name = vehicleDuty.Name;
            dto.Trips = vehicleDuty.Trips.ConvertAll<string>(trip => trip.Id.AsString());
            if (dto.Workblocks != null)
                dto.Workblocks = vehicleDuty.Workblocks.ConvertAll<string>(wb => wb.Id.AsString());
            return dto;
        }
    }
}