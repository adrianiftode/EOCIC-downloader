using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace InteractiveToolEu
{
    public class InteractiveToolEuClient
    {
        private readonly HttpClient _httpClient;
        public InteractiveToolEuClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://interactivetool.eu/EASME-TST/EOCIC/php/")
            };
        }

        public async Task<IReadOnlyCollection<Region>> GetRegions()
        {
            var response = await _httpClient.GetAsync("requestDataGraph.php?" +
                "Select=SELECT DISTINCT Region, Region_name FROM zone_country_region");
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IReadOnlyCollection<Region>>(content);
        }

        public async Task<IReadOnlyCollection<string>> GetIndicators()
        {
            var response = await _httpClient.GetAsync("requestDataGraph.php?" +
                "Select=SELECT DISTINCT Indicator FROM dsiyu");
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IReadOnlyCollection<Indicator>>(content).Select(c => c.Name).ToList();
        }

        public async Task<IReadOnlyCollection<EocicData>> GetEocicValuesBy(IEnumerable<string> regions, IEnumerable<string> indicators)
        {
            var response = await _httpClient.GetAsync($"requestDataGraph.php?" +
                $"Select=SELECT * FROM eocic_data&" +
                $"Region={EncodeValues(regions)}&" +
                $"Indicator={EncodeValues(indicators)}"
                );

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IReadOnlyCollection<EocicData>>(content);

            static string EncodeValues(IEnumerable<string> values) 
                => $"[{string.Join(",", values.Select(c => $"\"{HttpUtility.UrlEncode(c)}\""))}]";
        }
    }
}
