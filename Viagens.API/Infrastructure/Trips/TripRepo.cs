using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Infrastructure.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace lapr5_masterdata_viagens.Infrastructure.Trips
{
    public class TripRepo : BaseRepository<Trip, TripId>, ITripRepo
    {
        public TripRepo(ViagensDbContext context) : base(context.Trips)
        {
        }

        //Not currently used
        public async Task<List<Trip>> GetAllWithPassingTimes()
        {
            var list = this._objs.Include(t => t.PassingTimes).ToList();

            // orders the passing times
            foreach (var trip in list)
            {
                trip.PassingTimes.OrderBy(p => p.TimeInstant);
            }

            return list;
        }
    }
}