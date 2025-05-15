using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RailwayDbContext _context;

        public UnitOfWork(RailwayDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
            Stations = new Repository<Station>(_context);
            Segments = new SegmentRepository(_context);
            Cargoes = new Repository<Cargo>(_context);
            CargoTypes = new Repository<CargoType>(_context);
            Requests = new RequestRepository(_context);
            Routes = new Repository<Route>(_context);
            RouteSegments = new RouteSegmentRepository(_context);
            Payments = new Repository<Payment>(_context);
            PaymentMethods = new Repository<PaymentMethod>(_context);
            MovementStatuses = new MovementStatusRepository(_context);
            DowntimeCosts = new DowntimeCostRepository(_context);
            ScheduleEntries = new ScheduleEntryRepository(_context);
        }

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IScheduleEntryRepository ScheduleEntries { get; }
        public IRepository<Station> Stations { get; }
        public ISegmentRepository Segments { get; }
        public IRepository<Cargo> Cargoes { get; }
        public IRepository<CargoType> CargoTypes { get; }
        public IRequestRepository Requests { get; }
        public IRepository<Route> Routes { get; }
        public IRouteSegmentRepository RouteSegments { get; }
        public IRepository<Payment> Payments { get; }
        public IRepository<PaymentMethod> PaymentMethods { get; }
        public IMovementStatusRepository MovementStatuses { get; }
        public IDowntimeCostRepository DowntimeCosts { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
