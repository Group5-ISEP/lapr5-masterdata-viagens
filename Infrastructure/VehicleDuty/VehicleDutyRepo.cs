using lapr5_masterdata_viagens.Domain.VehicleDuties;
using lapr5_masterdata_viagens.Infrastructure.Shared;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lapr5_masterdata_viagens.Infrastructure.VehicleDuties
{
    public class VehicleDutyRepo : BaseRepository<VehicleDuty, VehicleDutyId>, IVehicleDutyRepo
    {
        public VehicleDutyRepo(ViagensDbContext context) : base(context.VehicleDuties)
        {
        }

        public new async Task<VehicleDuty> GetByIdAsync(VehicleDutyId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return await this._objs
                .Include(vd => vd.Trips)
                .ThenInclude(t => t.PassingTimes)
                .Include(vd => vd.Workblocks)
                .Where(x => id.Equals(x.Id)).FirstOrDefaultAsync();
        }
    }
}