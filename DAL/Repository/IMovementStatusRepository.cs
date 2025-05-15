using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IMovementStatusRepository: IRepository<MovementStatus>
    {
        Task<MovementStatus?> FindAsync(Expression<Func<MovementStatus, bool>> predicate);

    }
}
