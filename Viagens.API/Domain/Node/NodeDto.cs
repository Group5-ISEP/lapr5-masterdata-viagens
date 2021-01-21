using System.Text.Json.Serialization;


namespace lapr5_masterdata_viagens.Domain.Node
{
    public class NodeDto
    {
        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}