using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace lapr5_masterdata_viagens.Domain.Path
{

    public class PathDTO
    {
        [JsonPropertyName("id")]
        public string PathId { get; set; }
        [JsonPropertyName("lineCode")]
        public string LineId { get; set; }
        [JsonPropertyName("direction")]
        public string Orientation { get; set; }
        [JsonPropertyName("firstNode")]
        public string FirstNodeId { get; set; }
        [JsonPropertyName("lastNode")]
        public string LastNodeId { get; set; }
        [JsonPropertyName("isEmpty")]
        public bool IsEmpty { get; set; }
        [JsonPropertyName("segmentList")]
        public List<SegmentDTO> Segments { get; set; }

    }

    public class SegmentDTO
    {
        [JsonPropertyName("startNode")]
        public string StartNodeId { get; set; }
        [JsonPropertyName("endNode")]
        public string EndNodeId { get; set; }
        [JsonPropertyName("order")]
        public int Order { get; set; }
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }
}