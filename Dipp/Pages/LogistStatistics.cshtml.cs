using BLL.LogistStatisticsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages
{
    public class LogistStatisticsModel : PageModel
    {
        private readonly ILogistStatisticsService _statisticsService;

        public LogistStatisticsModel(ILogistStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public Dictionary<string, TimeSpan> AverageDowntimeByStation { get; set; } = new();
        public List<(string StationName, TimeSpan TotalDowntime)> Top5StationsByDowntime { get; set; } = new();
        public decimal TotalDowntimeCost { get; set; }
        public List<(string CargoType, int RequestCount)> Top5CargoTypesByRequests { get; set; } = new();

        public async Task OnGetAsync()
        {
            AverageDowntimeByStation = await _statisticsService.GetAverageDowntimeByStationAsync();
            Top5StationsByDowntime = await _statisticsService.GetTop5StationsByDowntimeAsync();
            TotalDowntimeCost = await _statisticsService.GetTotalDowntimeCostAsync();
            Top5CargoTypesByRequests = await _statisticsService.GetTop5CargoTypesByRequestsAsync();
        }
    }
}
