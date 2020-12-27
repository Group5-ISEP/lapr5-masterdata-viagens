using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class CreateTripsDTO
    {

        public int Frequency { get; set; }
        public int NumberOfTrips { get; set; }
        public int StartTime { get; set; }
        public PathDTO PathTo { get; set; }
        public PathDTO PathFrom { get; set; }


    }
}