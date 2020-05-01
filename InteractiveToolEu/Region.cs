using System.Text.Json.Serialization;

namespace InteractiveToolEu
{
    public class Region
    {
        [JsonPropertyName("Region")] 
        public string Code { get; set; }

        [JsonPropertyName("Region_name")]
        public string Name { get; set; }
    }
}