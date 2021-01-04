using lapr5_masterdata_viagens.Domain.VehicleDuties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Viagens.Tests
{
    public class MockVehicleDutyRepo : IVehicleDutyRepo
    {
        private List<VehicleDuty> _list = new List<VehicleDuty>();
        public async Task<List<VehicleDuty>> GetAllAsync()
        {
            return _list;
        }

        public async Task<VehicleDuty> GetByIdAsync(VehicleDutyId id)
        {
            //return await this._context.Categories.FindAsync(id);
            return _list.Find(v => v.Id.Equals(id));
        }
        public async Task<List<VehicleDuty>> GetByIdsAsync(List<VehicleDutyId> ids)
        {
            return _list.FindAll(v => ids.Contains(v.Id));
        }
        public async Task<VehicleDuty> AddAsync(VehicleDuty obj)
        {
            _list.Add(obj);
            return obj;
        }

        public void Remove(VehicleDuty obj)
        {
            _list.Remove(obj);
        }

    }
}