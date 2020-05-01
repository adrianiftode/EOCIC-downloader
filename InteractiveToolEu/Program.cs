using System.Threading.Tasks;

namespace InteractiveToolEu
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var downloader = new Downloader();

            await downloader.DownloadEocicByIndicators(new[] { "Employment", "Productivity", "Specialisation" },
                "employment-productivity-specialisation-2.csv");

            //or
            //await downloader.DownloadEocic();
        }
    }
}
