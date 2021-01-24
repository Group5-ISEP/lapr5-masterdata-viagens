using lapr5_masterdata_viagens.Domain.Drivers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class MockDriverRepo : IDriverRepo
    {
        private List<Driver> _list = new List<Driver>();
        public async Task<List<Driver>> GetAllAsync()
        {
            return _list;
        }

        public async Task<Driver> GetByIdAsync(DriverId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return _list.Find(v => v.Id.Equals(id));
        }
        public async Task<List<Driver>> GetByIdsAsync(List<DriverId> ids)
        {
            return _list.FindAll(v => ids.Contains(v.Id));
        }
        public async Task<Driver> AddAsync(Driver obj)
        {
            _list.Add(obj);
            return obj;
        }

        public void Remove(Driver obj)
        {
            _list.Remove(obj);
        }
    }
}