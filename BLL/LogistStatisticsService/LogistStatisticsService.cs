using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LogistStatisticsService
{
    public class LogistStatisticsService : ILogistStatisticsService
    {
        private readonly IUnitOfWork _uow;

        public LogistStatisticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Dictionary<string, TimeSpan>> GetAverageDowntimeByStationAsync()
        {
            var entries = await _uow.ScheduleEntries.GetAllAsync();
            var result = entries
                .GroupBy(e => e.Station.StationName)
                .ToDictionary(
                    g => g.Key,
                    g => TimeSpan.FromMinutes(g.Average(e => e.Downtime.TotalMinutes))
                );

            return result;
        }

        public async Task<List<(string StationName, TimeSpan TotalDowntime)>> GetTop5StationsByDowntimeAsync()
        {
            var entries = await _uow.ScheduleEntries.GetAllAsync();

            return entries
                .GroupBy(e => e.Station.StationName)
                .Select(g => (StationName: g.Key, TotalDowntime: TimeSpan.FromMinutes(g.Sum(e => e.Downtime.TotalMinutes))))
                .OrderByDescending(x => x.TotalDowntime)
                .Take(5)
                .ToList();
        }

        public async Task<decimal> GetTotalDowntimeCostAsync()
        {
            var requests = await _uow.Requests.GetAllAsync();
            return (decimal)requests.Sum(r => r.Cost); // или r.CostDowntime, если отделить основную стоимость
        }

        public async Task<List<(string CargoType, int RequestCount)>> GetTop5CargoTypesByRequestsAsync()
        {
            var requests = await _uow.Requests.GetAllAsync();

            return requests
                .GroupBy(r => r.Cargo.CargoType.TypeName)
                .Select(g => (CargoType: g.Key, RequestCount: g.Count()))
                .OrderByDescending(x => x.RequestCount)
                .Take(5)
                .ToList();
        }
    }

}
