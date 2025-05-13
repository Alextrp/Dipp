using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RouteOptimizerService
{
    public interface IRouteOptimizerService
    {
        Task<OptimizedRouteResult> CalculateOptimalRoute();
    }
}
