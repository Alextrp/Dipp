using BLL.DTOs;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RequestService
{
    public class RequestService: IRequestService
    {
        private readonly IUnitOfWork _uow;

        public RequestService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<RequestDTO>> GetUserRequestsAsync(string userLogin)
        {
            var user = await _uow.Users.GetByLoginAsync(userLogin);
            if (user == null)
                throw new Exception("Пользователь не найден.");

            var requests = await _uow.Requests
                .GetRequestsWithDetailsByUserIdAsync(user.UserId);

            return requests.Select(r => new RequestDTO
            {
                RequestId = r.RequestId,
                CargoName = r.Cargo.CargoName,
                RouteName = r.Route.RouteName,
                RequestDate = r.RequestDate,
                Cost = r.Cost,
                PaymentStatus = r.Status
            }).ToList();
        }
    }

}
