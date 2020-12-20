using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain;

namespace lapr5_masterdata_viagens.Repositories
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
            return v;
        }
    }
}