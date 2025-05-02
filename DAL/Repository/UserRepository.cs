using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(User entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(User entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(User entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => EF.Property<string>(e, "Login") == login);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
