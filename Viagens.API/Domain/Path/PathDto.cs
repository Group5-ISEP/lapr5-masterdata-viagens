using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Path
{

    public class PathDTO
    {

        public string PathId { get; set; }
        public string LineId { get; set; }
        public string Orientation { get; set; }
        public bool IsEmpty { get; set; }
        public List<SegmentDTO> Segments { get; set; }

    }

    public class SegmentDTO
    {
        public string StartNodeId { get; set; }
        public string EndNodeId { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public int Distance { get; set; }
    }
}