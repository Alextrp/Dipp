using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SegmentRepository: ISegmentRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<Segment> _dbSet;

        public SegmentRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Segment>();
        }

        public async Task<IEnumerable<Segment>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Segment?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(Segment entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(Segment entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Segment entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<Segment>> GetAllWithStationsAsync()
        {
            return await _context.Set<Segment>()
                .Include(s => s.StationFrom)
                .Include(s => s.StationTo)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
