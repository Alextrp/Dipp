using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RouteOptimizerService
{
    public class OptimizedRouteResult
    {
        public List<Station> RouteStations { get; set; }
        public List<Request> RequestsInRoute { get; set; }
        public double TotalDistance { get; set; }
    }

}
