using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class TripDTO
    {
        public string Id { get; set; }
        public string PathID { get; set; }
        public string LineID { get; set; }

        public string Orientation { get; set; }
        public List<PassingTimeDTO> PassingTimes { get; set; }

    }
    public class PassingTimeDTO
    {
        public string Id { get; set; }
        public int TimeInstant { get; set; }
        public string NodeID { get; set; }
    }
}