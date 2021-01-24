using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Node
{
    public class NodeTimetableDto
    {
        public List<BusPassingDto> schedule { get; set; }
    }

    public class BusPassingDto
    {
        public string Line { get; set; }
        public string DestinationName { get; set; }
        public int TimeInstant { get; set; }
    }
}