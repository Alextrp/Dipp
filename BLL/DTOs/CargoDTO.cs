using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CargoDTO
    {
        public int CargoId { get; set; } = 0;
        public string CargoName { get; set; } = null!;
        public double? WeightKg { get; set; }
        public string? AdditionalInfo { get; set; } = null!;
        public string CargoType { get; set; } = null!;
    }
}
