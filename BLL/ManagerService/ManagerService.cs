using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerService
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ManagerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<Request>> GetPendingRequestsAsync()
        {
            var allRequests = await _uow.Requests.GetAllAsync();
            var requests = allRequests.Where(r => r.Status == "Ждет добавления в расписание").ToList();
            return requests;
        }

        public async Task<bool> ConfirmRequestAsync(int requestId)
        {
            var request = await _uow.Requests.GetByIdAsync(requestId);
            if (request == null) return false;

            request.Status = "Подтверждено";
            _uow.Requests.Update(request);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> ConfirmMultipleRequestsAsync(List<int> requestIds)
        {
            var requests = (await _uow.Requests.GetAllAsync()).Where(r => requestIds.Contains(r.RequestId));
            foreach (var req in requests)
            {
                req.Status = "Подтверждено";
            }

            _uow.Requests.UpdateRange(requests);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<bool> AddToScheduleAsync(List<int> requestIds)
        {
            var stationDowntimeMap = new Dictionary<int, TimeSpan>();
            var rand = new Random();

            foreach (var requestId in requestIds)
            {
                var request = await _uow.Requests.GetByIdAsync(requestId);
                if (request == null) continue;

                var cargo = await _uow.Cargoes.GetByIdAsync(request.CargoId);
                if (cargo == null) continue;

                var cargoTypeId = cargo.CargoTypeId;
                decimal downtimeTotalCost = 0;

                var segments = await _uow.RouteSegments.GetSegmentsByRouteIdAsync(request.RouteId);
                var sortedSegments = segments.OrderBy((RouteSegment s) => s.OrderNumber).ToList();

                DateTime arrival = DateTime.Now;

                foreach (var segment in sortedSegments)
                {
                    decimal distance = segment.Segment.DistanceKm;
                    var travelTime = TimeSpan.FromHours((double)distance / 80); // Средняя скорость 80 км/ч
                    arrival += travelTime;

                    var stationId = segment.Segment.StationToId;
                    TimeSpan downtime;

                    if (segment == sortedSegments.Last())
                    {
                        downtime = TimeSpan.FromHours(1); // Конечная станция — всегда 1 час
                    }
                    else
                    {
                        if (!stationDowntimeMap.TryGetValue(stationId, out downtime))
                        {
                            int randomMinutes = rand.Next(15, 600); // от 15 минут до 10 часов
                            downtime = TimeSpan.FromMinutes(randomMinutes);
                            stationDowntimeMap[stationId] = downtime;
                        }
                    }

                    // Найти стоимость простоя на этой станции для типа груза
                    var downtimeCost = await _uow.DowntimeCosts.GetByStationAndCargoTypeAsync(stationId, cargoTypeId);
                    if (downtimeCost != null)
                    {
                        downtimeTotalCost += downtimeCost.CostPerHour * (decimal)downtime.TotalHours;
                    }

                    var entry = new ScheduleEntry
                    {
                        RequestId = requestId,
                        StationId = stationId,
                        ArrivalTime = arrival,
                        Downtime = downtime
                    };

                    await _uow.ScheduleEntries.AddAsync(entry);
                    arrival += downtime;
                }

                // Обновляем статус и стоимость
                request.Status = "В расписании";
                request.Cost += Math.Round(downtimeTotalCost, 2);

                await _uow.Requests.UpdateAsync(request);

            }

            await _uow.SaveAsync();
            return true;
        }


    }
}


