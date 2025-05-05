using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class RouteSegmentDTO
    {
        public int RouteSegmentId { get; set; }
        public string RouteName { get; set; } = null!;
        public string SegmentFrom { get; set; } = null!;
        public string SegmentTo { get; set; } = null!;
    }
}
