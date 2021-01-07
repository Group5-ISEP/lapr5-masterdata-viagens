using lapr5_masterdata_viagens.Domain.DriverDuties;
using lapr5_masterdata_viagens.Infrastructure.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.DriverDuties
{
    public class DriverDutyRepo : BaseRepository<DriverDuty, DriverDutyId>, IDriverDutyRepo
    {
        public DriverDutyRepo(ViagensDbContext context) : base(context.DriverDuties)
        {
        }
    }
}