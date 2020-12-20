using lapr5_masterdata_viagens.Domain;
using System.Threading.Tasks;

namespace lapr5_masterdata_viagens.Repositories
{
    public interface IVehicleRepo
    {
        Task<Vehicle> Save(Vehicle v);
    }
}