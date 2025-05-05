using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SegmentDTO
    {
        public int SegmentId { get; set; }
        public string StationFrom { get; set; } = null!;
        public string StationTo { get; set; } = null!;
        public decimal Distance { get; set; }
    }
}
