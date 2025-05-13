using BLL.DTOs;
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
        //private readonly IStationService _stationService;

        public ManagerService(IUnitOfWork uow)
        {
            _uow = uow;
            
        }

        public async Task<List<RequestDTO>> GetPendingOrdersAsync()
        {
            var allorders = await _uow.Requests.GetAllAsync();
            var orders = allorders.Where(r => r.Status == "Ждет добавления в расписание").ToList();
            return orders.Select(o => new RequestDTO
            {
                RequestId = o.RequestId,                
                RequestStatus = o.Status
            }).ToList();
        }

        public async Task AddOrdersToScheduleAsync(List<int> orderIds)
        {
            //foreach (var orderId in orderIds)
            //{
            //    var order = await _uow.Requests.GetByIdAsync(orderId);
            //    if (order == null) continue;

            //    var distance = await _stationService.GetDistanceAsync(order.FromStationId, order.ToStationId);
            //    var travelTimeHours = distance / 80.0;
            //    var arrival = DateTime.Now.AddHours(travelTimeHours);
            //    var unloadComplete = arrival.AddHours(1);

            //    order.ScheduleDeparture = DateTime.Now;
            //    order.ScheduleArrival = arrival;
            //    order.UnloadComplete = unloadComplete;
            //    order.Status = "Запланировано";

            //    await _uow.Orders.UpdateAsync(order);
            //}

            //await _uow.SaveAsync();
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            //var order = await _uow.Orders.GetByIdAsync(orderId);
            //if (order != null)
            //{
            //    order.Status = "Подтвержден";
            //    await _uow.Orders.UpdateAsync(order);
            //    await _uow.SaveAsync();
            //}
        }
    }

}
