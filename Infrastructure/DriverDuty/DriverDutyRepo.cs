using lapr5_masterdata_viagens.Domain.DriverDuties;
using lapr5_masterdata_viagens.Infrastructure.Shared;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lapr5_masterdata_viagens.Infrastructure.DriverDuties
{
    public class DriverDutyRepo : BaseRepository<DriverDuty, DriverDutyId>, IDriverDutyRepo
    {
        public DriverDutyRepo(ViagensDbContext context) : base(context.DriverDuties)
        {
        }

         public new async Task<List<DriverDuty>> GetAllAsync()
        {
            return this._objs
                .Include(vd => vd.Workblocks)
                .ToList();
        }
    }
}