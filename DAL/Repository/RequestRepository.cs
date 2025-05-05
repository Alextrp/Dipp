using Azure.Core;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request = DAL.Models.Request;

namespace DAL.Repository
{
    public class RequestRepository: IRequestRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<Request> _dbSet;

        public RequestRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Request>();
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Request?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(Request entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(Request entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Request entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<Request>> GetRequestsWithDetailsByUserIdAsync(int userId)
        {
            return await _context.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.Cargo)
                .Include(r => r.Route)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
