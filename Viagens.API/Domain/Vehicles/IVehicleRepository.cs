using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Domain.Vehicles
{
    public interface IVehicleRepo : IRepository<Vehicle, VehicleId>
    {
    }
}