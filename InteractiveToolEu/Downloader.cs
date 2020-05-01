using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveToolEu
{
    public class Downloader
    {
        private readonly InteractiveToolEuClient _client;
        private readonly DownloaderOptions _options;
        public Downloader(DownloaderOptions options = null)
        {
            _client = new InteractiveToolEuClient();
            _options = options ?? new DownloaderOptions();
        }

        public async Task DownloadEocic(string csvPath = "eocic.csv")
        {
            var regions = await _client.GetRegions();
            var indicators = await _client.GetIndicators();

            await DownloadEocicValuesAsCsv(regions.Select(c => c.Code), indicators, csvPath);
        }

        public async Task DownloadEocicByIndicators(IEnumerable<string> indicators, string csvPath)
        {
            var regions = await _client.GetRegions();

            await DownloadEocicValuesAsCsv(regions.Select(c => c.Code), indicators, csvPath);
        }

        private async Task DownloadEocicValuesAsCsv(IEnumerable<string> regions, IEnumerable<string> indicators, string csvPath)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            foreach (var regionsBatch in regions.Select(c => c).Page(_options.RegionsBatchSize))
            {
                foreach (var indicatorsBatch in indicators.Page(_options.IndicatorsBatchSize))
                {
                    var values = await _client.GetEocicValuesBy(regionsBatch, indicatorsBatch);

                    csv.WriteRecords(values);
                }
            }
        }
    }
}
