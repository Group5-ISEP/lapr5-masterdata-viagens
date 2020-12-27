using lapr5_masterdata_viagens.Domain.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public interface ITripRepo : IRepository<Trip, TripId>
    {
        Task<List<Trip>> GetByLine(string lineId);
    }
}