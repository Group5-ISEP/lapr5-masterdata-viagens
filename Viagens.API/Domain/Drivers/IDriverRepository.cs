using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public interface IDriverRepo : IRepository<Driver, DriverId>
    {
    }
}