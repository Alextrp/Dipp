using DAL.Models;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DispatcherService
{
    public class DispatcherService : IDispatcherService
    {
        private readonly IUnitOfWork _uow;

        public DispatcherService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task UpdateMovementStatusesAsync()
        {
            var allSchedules = await _uow.ScheduleEntries.GetAllAsync();
            var allStatuses = await _uow.MovementStatuses.GetAllAsync();

            var now = DateTime.Now;
            var requests = allSchedules
                .GroupBy(s => s.RequestId)
                .ToList();

            foreach (var group in requests)
            {
                var requestId = group.Key;
                var sortedEntries = group.OrderBy(e => e.ArrivalTime).ToList();

                // Найти станцию, на которой груз сейчас находится
                ScheduleEntry? currentEntry = null;
                foreach (var entry in sortedEntries)
                {
                    var leaveTime = entry.ArrivalTime + entry.Downtime;
                    if (now < leaveTime)
                    {
                        currentEntry = entry;
                        break;
                    }
                }

                if (currentEntry != null)
                {
                    var existingStatus = allStatuses.FirstOrDefault(ms => ms.RequestId == requestId);
                    if (existingStatus != null)
                    {
                        existingStatus.CurrentStationId = currentEntry.StationId;
                        existingStatus.StatusDate = now;
                        existingStatus.SattusDescription = $"На станции {currentEntry.Station.StationName}";
                        _uow.MovementStatuses.Update(existingStatus);
                    }
                    else
                    {
                        var newStatus = new MovementStatus
                        {
                            RequestId = requestId,
                            CurrentStationId = currentEntry.StationId,
                            StatusDate = now,
                            SattusDescription = $"На станции {currentEntry.Station.StationName}"
                        };
                        await _uow.MovementStatuses.AddAsync(newStatus);
                    }
                }
            }

            await _uow.SaveAsync();
        }

        public async Task<List<MovementStatus>> GetCurrentStatusesAsync()
        {
            return (await _uow.MovementStatuses.GetAllAsync()).ToList();
        }
    }
}
