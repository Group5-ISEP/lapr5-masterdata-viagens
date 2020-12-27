using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public interface ITripRepo : IRepository<Trip, TripId>
    {
    }
}