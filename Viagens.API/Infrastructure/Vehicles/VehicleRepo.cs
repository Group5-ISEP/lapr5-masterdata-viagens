using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicles;
using lapr5_masterdata_viagens.Infrastructure.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.Vehicles
{
    public class VehicleRepo : BaseRepository<Vehicle, VehicleId>, IVehicleRepo
    {
        public VehicleRepo(ViagensDbContext context) : base(context.Vehicles)
        {
        }
    }
}