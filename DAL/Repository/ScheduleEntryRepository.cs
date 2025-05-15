using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ScheduleEntryRepository: IScheduleEntryRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<ScheduleEntry> _dbSet;

        public ScheduleEntryRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<ScheduleEntry>();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetAllAsync()
        {
            return await _dbSet.Include(s => s.Station)
                .ToListAsync();
        }

        public async Task<ScheduleEntry?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(ScheduleEntry entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(ScheduleEntry entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(ScheduleEntry entity)
        {
            _dbSet.Remove(entity);
        }



        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
