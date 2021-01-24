using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Domain.DriverDuties
{
    public interface IDriverDutyRepo : IRepository<DriverDuty, DriverDutyId>
    {
    }
}