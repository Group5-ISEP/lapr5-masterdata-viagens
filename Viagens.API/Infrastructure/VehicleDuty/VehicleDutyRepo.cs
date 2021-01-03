using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Infrastructure.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.VehicleDuties
{
    public class VehicleDutyRepo : BaseRepository<VehicleDuty, VehicleDutyId>, IVehicleDutyRepo
    {
        public VehicleDutyRepo(ViagensDbContext context) : base(context.VehicleDuties)
        {
        }
    }
}