using lapr5_masterdata_viagens.Domain.Vehicles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class MockVehicleRepo : IVehicleRepo
    {
        private List<Vehicle> _list = new List<Vehicle>();
        public async Task<List<Vehicle>> GetAllAsync()
        {
            return _list;
        }

        public async Task<Vehicle> GetByIdAsync(VehicleId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return _list.Find(v => v.Id.Equals(id));
        }
        public async Task<List<Vehicle>> GetByIdsAsync(List<VehicleId> ids)
        {
            return _list.FindAll(v => ids.Contains(v.Id));
        }
        public async Task<Vehicle> AddAsync(Vehicle obj)
        {
            _list.Add(obj);
            return obj;
        }

        public void Remove(Vehicle obj)
        {
            _list.Remove(obj);
        }
    }
}