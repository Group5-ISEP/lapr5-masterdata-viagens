using lapr5_masterdata_viagens.Domain.DriverDuties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class MockDriverDutyRepo : IDriverDutyRepo
    {
        private List<DriverDuty> _list = new List<DriverDuty>();
        public async Task<List<DriverDuty>> GetAllAsync()
        {
            return _list;
        }

        public async Task<DriverDuty> GetByIdAsync(DriverDutyId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return _list.Find(v => v.Id.Equals(id));
        }
        public async Task<List<DriverDuty>> GetByIdsAsync(List<DriverDutyId> ids)
        {
            return _list.FindAll(v => ids.Contains(v.Id));
        }
        public async Task<DriverDuty> AddAsync(DriverDuty obj)
        {
            _list.Add(obj);
            return obj;
        }

        public void Remove(DriverDuty obj)
        {
            _list.Remove(obj);
        }

    }
}