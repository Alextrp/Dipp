using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RouteSegmentRepository: IRouteSegmentRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<RouteSegment> _dbSet;

        public RouteSegmentRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<RouteSegment>();
        }

        public async Task<IEnumerable<RouteSegment>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<RouteSegment?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(RouteSegment entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(RouteSegment entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(RouteSegment entity)
        {
            _dbSet.Remove(entity);
        }


        public async Task<List<RouteSegment>> GetSegmentsByRouteIdAsync(int routeId)
        {
            return await _context.RouteSegments
                .Include(rs => rs.Segment)
                .Where(rs => rs.RouteId == routeId)
                .OrderBy(rs => rs.OrderNumber)
                .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
