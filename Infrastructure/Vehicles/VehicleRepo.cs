using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicles;

namespace lapr5_masterdata_viagens.Infrastructure.Vehicles
{
    public class VehicleRepo : IVehicleRepo
    {
        private readonly DbSet<Vehicle> _objs;

        public VehicleRepo(ViagensDbContext context)
        {
            this._objs = context.Vehicles;
        }

        public async Task<Vehicle> Save(Vehicle v)
        {
            //var ret = await this._objs.AddAsync(v);
            return v;//ret.Entity;
        }
    }
}