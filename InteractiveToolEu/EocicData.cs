using System.Text.Json.Serialization;

namespace InteractiveToolEu
{
    public class EocicData
    {
        public string Indicator { get; set; }
        public string Sector { get; set; }
        public string Dimension { get; set; }
        public string Unit { get; set; }
        public string Zone { get; set; }
        public string Year { get; set; }
        public string Value { get; set; }
        public string Region { get; set; }

        [JsonPropertyName("Region_name")]
        public string RegionName { get; set; }
        public string Country { get; set; }

        [JsonPropertyName("Country_name")]
        public string CountryName { get; set; }
    }
}