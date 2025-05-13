using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerService
{
    public interface IManagerService
    {
        Task<List<RequestDTO>> GetPendingOrdersAsync();
        Task AddOrdersToScheduleAsync(List<int> orderIds);
        Task ConfirmOrderAsync(int orderId);
    }

}
