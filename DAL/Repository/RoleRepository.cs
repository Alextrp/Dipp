using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly RailwayDbContext _context;
        private readonly DbSet<Role> _dbSet;

        public RoleRepository(RailwayDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Role>();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(Role entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(Role entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Role entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int?> GetRoleIdByNameAsync(string roleName)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
            return role?.RoleId;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
