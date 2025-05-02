using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRoleRepository: IRepository<Role>
    {
        Task<int?> GetRoleIdByNameAsync(string roleName);
    }
}
