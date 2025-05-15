using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LogistStatisticsService
{
    public interface ILogistStatisticsService
    {
        Task<Dictionary<string, TimeSpan>> GetAverageDowntimeByStationAsync();
        Task<List<(string StationName, TimeSpan TotalDowntime)>> GetTop5StationsByDowntimeAsync();
        Task<decimal> GetTotalDowntimeCostAsync();
        Task<List<(string CargoType, int RequestCount)>> GetTop5CargoTypesByRequestsAsync();
    }

}
