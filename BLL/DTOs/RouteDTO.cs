using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class RouteDTO
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;
        public string StartStation { get; set; } = null!;
        public string EndStation { get; set; } = null!;
        public decimal? TotalDistance { get; set; }
        public int? EstimatedTime { get; set; }
    }
}
