using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DowntimeCostRepository: IDowntimeCostRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<DowntimeCost> _dbSet;

        public DowntimeCostRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<DowntimeCost>();
        }

        public async Task<IEnumerable<DowntimeCost>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<DowntimeCost?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(DowntimeCost entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(DowntimeCost entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(DowntimeCost entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<DowntimeCost?> GetByStationAndCargoTypeAsync(int stationId, int cargoTypeId)
        {
            return await _context.DowntimeCosts
                .FirstOrDefaultAsync(d => d.StationId == stationId && d.CargoTypeId == cargoTypeId);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
