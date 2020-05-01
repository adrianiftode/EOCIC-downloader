using System.Text.Json.Serialization;

namespace InteractiveToolEu
{
    public class Indicator
    {
        [JsonPropertyName("Indicator")] 
        public string Name { get; set; }
    }
}
