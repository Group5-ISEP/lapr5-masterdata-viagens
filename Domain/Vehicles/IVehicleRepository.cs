using System.Threading.Tasks;

namespace lapr5_masterdata_viagens.Domain.Vehicles
{
    public interface IVehicleRepo
    {
        Task<Vehicle> Save(Vehicle v);
    }
}