using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IRepository<Station> Stations { get; }
        IRepository<Segment> Segments { get; }
        IRepository<Cargo> Cargoes { get; }
        IRepository<CargoType> CargoTypes { get; }
        IRepository<Request> Requests { get; }
        IRepository<Route> Routes { get; }
        IRepository<RouteSegment> RouteSegments { get; }
        IRepository<Payment> Payments { get; }
        IRepository<PaymentMethod> PaymentMethods { get; }
        IRepository<MovementStatus> MovementStatuses { get; }
        IRepository<DowntimeCost> DowntimeCosts { get; }

        Task<int> SaveAsync();
    }

}
