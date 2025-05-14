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
        ISegmentRepository Segments { get; }
        IScheduleEntryRepository ScheduleEntries { get; }
        IRepository<Station> Stations { get; }
        IRepository<Cargo> Cargoes { get; }
        IRepository<CargoType> CargoTypes { get; }
        IRequestRepository Requests { get; }
        IRepository<Route> Routes { get; }
        IRouteSegmentRepository RouteSegments { get; }
        IRepository<Payment> Payments { get; }
        IRepository<PaymentMethod> PaymentMethods { get; }
        IRepository<MovementStatus> MovementStatuses { get; }
        IDowntimeCostRepository DowntimeCosts { get; }

        Task<int> SaveAsync();
    }

}
