using lapr5_masterdata_viagens.Domain.Workblocks;

namespace lapr5_masterdata_viagens.Domain.DriverDuties
{
    public class DriverDutyMapper
    {
        public static DriverDutyDTO ToDto(DriverDuty DriverDuty)
        {
            DriverDutyDTO dto = new DriverDutyDTO();
            dto.Id = DriverDuty.Id.AsString();
            dto.Name = DriverDuty.Name;
            dto.Workblocks = DriverDuty.Workblocks.ConvertAll<WorkblockDto>(wb =>
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