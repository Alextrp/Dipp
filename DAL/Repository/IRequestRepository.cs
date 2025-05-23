﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRequestRepository: IRepository<Request>
    {
        Task<List<Request>> GetRequestsWithDetailsByUserIdAsync(int userId);
        Task<List<Request>> GetAllActiveRequests();
        void UpdateRange(IEnumerable<Request> entities);
        Task UpdateAsync(Request entity);


    }
}
