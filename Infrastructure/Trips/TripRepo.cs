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
        public async Task<List<Trip>> GetByLine(string lineId)
        {
            var list = this._objs.Include(t => t.PassingTimes).Where(t => t.LineID == lineId).ToList();

            return list;
        }

        public async Task<List<Trip>> GetByNode(string nodeId)
        {
            var list = this._objs.Include(t => t.PassingTimes).ToList();
            return list.FindAll(trip => trip.PassingTimes.Find(pt => pt.NodeID == nodeId) != null);
        }
    }
}