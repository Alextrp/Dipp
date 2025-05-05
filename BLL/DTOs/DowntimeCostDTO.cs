using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DowntimeCostDTO
    {
        public int DowntimeCostId { get; set; }
        public decimal CostPerHour { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string CargoType { get; set; } = null!;
        public string Station { get; set; } = null!;
    }
}
