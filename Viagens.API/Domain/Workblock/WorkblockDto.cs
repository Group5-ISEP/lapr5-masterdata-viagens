using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Workblocks
{
    public class WorkblockDto
    {
        public string Id { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<string> TripsIDs { get; set; }
    }
}