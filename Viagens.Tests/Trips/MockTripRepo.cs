using lapr5_masterdata_viagens.Domain.Trips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class MockTripRepo : ITripRepo
    {
        private List<Trip> _list = new List<Trip>();
        public async Task<List<Trip>> GetAllAsync()
        {
            return _list;
        }

        public async Task<Trip> GetByIdAsync(TripId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return _list.Find(v => v.Id.Equals(id));
        }
        public async Task<List<Trip>> GetByIdsAsync(List<TripId> ids)
        {
            return _list.FindAll(v => ids.Contains(v.Id));
        }
        public async Task<Trip> AddAsync(Trip obj)
        {
            _list.Add(obj);
            return obj;
        }

        public void Remove(Trip obj)
        {
            _list.Remove(obj);
        }

        public async Task<List<Trip>> GetByLine(string lineId)
        {
            return _list.FindAll(t => t.LineID == lineId);
        }

        public async Task<List<Trip>> GetByNode(string nodeId)
        {
            return _list.FindAll(t => t.PassingTimes.Find(pt => pt.NodeID == nodeId) != null);
        }

    }
}