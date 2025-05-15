using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class MovementStatusRepository : IMovementStatusRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<MovementStatus> _dbSet;

        public MovementStatusRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<MovementStatus>();
        }

        public async Task<IEnumerable<MovementStatus>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<MovementStatus?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(MovementStatus entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(MovementStatus entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(MovementStatus entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<MovementStatus?> FindAsync(Expression<Func<MovementStatus, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
