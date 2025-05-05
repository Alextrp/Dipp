using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RequestService
{
    public interface IRequestService
    {
        Task<List<RequestDTO>> GetUserRequestsAsync(string userLogin);
    }
}
