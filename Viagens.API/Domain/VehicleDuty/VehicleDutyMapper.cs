using lapr5_masterdata_viagens.Domain.Workblocks;

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
            dto.Workblocks = vehicleDuty.Workblocks.ConvertAll<WorkblockDto>(wb =>
                new WorkblockDto()
                {
                    Id = wb.Id.AsString(),
                    StartTime = wb.StartTime,
                    EndTime = wb.EndTime,
                    TripsIDs = wb.TripsIDs
                });
            return dto;
        }
    }
}