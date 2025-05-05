using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class StationDTO
    {
        public int StationId { get; set; }
        public string StationName { get; set; } = null!;
        public string Location { get; set; } = null!;
    }
}
