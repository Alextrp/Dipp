using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerService
{
    public interface IManagerService
    {
        Task<List<Request>> GetPendingRequestsAsync();
        Task<bool> ConfirmRequestAsync(int requestId);
        Task<bool> ConfirmMultipleRequestsAsync(List<int> requestIds);
        Task<bool> AddToScheduleAsync(List<int> requestIds);
    }

}
