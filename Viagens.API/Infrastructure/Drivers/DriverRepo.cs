using lapr5_masterdata_viagens.Domain.Drivers;
using lapr5_masterdata_viagens.Infrastructure.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.Drivers
{
    public class DriverRepo : BaseRepository<Driver, DriverId>, IDriverRepo
    {
        public DriverRepo(ViagensDbContext context) : base(context.Drivers)
        {
        }
    }
}